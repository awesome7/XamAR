using System;
using System.Numerics;
using XamAR.Core.Display;
using XamAR.Core.Geometry;
using XamAR.Core.Models;
using XamAR.Core.Models.Direction;
using XamAR.Core.Sensors;

namespace XamAR.Core
{
    /// <summary>
    ///     Service which has its own loop in which
    ///     all Entities are updated.
    /// </summary>
    public class EntityUpdateService
    {
        public EntityUpdateService(ObjectManagerService objectManager, IDisplayService displayService)
        {
            ObjectManager = objectManager;
            DisplayService = displayService;
        }

        private ObjectManagerService ObjectManager { get; }

        private IDisplayService DisplayService { get; }

        /// <summary>
        ///     Start listening for RefreshFrame.
        ///     Recalculate all displayed entities on each frame.
        /// </summary>
        public void Run()
        {
            LocationMonitor.StartTracking();
            OrientationMonitor.StartTracking();

            DisplayService.RefreshFrame += Update;
        }

        protected virtual void Update()
        {
            Vector3 cameraPosition = DisplayService.CameraPosition;
            Vector3 cameraDirection = DisplayService.CameraDirection;
            Vector3 cameraUp = DisplayService.CameraUp;

            WorldTransformation realWorld = OrientationMonitor.World;
            WorldTransformation arWorld =
                WorldTransformation.CreateForZ(cameraDirection.Negate(), cameraUp, cameraPosition);
            WorldConverter worldConverter = new WorldConverter(realWorld, arWorld);

            float arToCamera = new Vector3(0, 0, -1).GetAngleYDeg(cameraDirection);
            //var cameraToNorth = OrientationMonitor.MagneticNorthDeg;
            //var arToNorth = -arToCamera + cameraToNorth;
            //arToNorth %= 360;

            foreach (Entity e in ObjectManager.AllObjects)
            {
                e.Position.RefreshPositionRealWorld(worldConverter);
            }

            //Parallel.ForEach(objectManager.AllObjects, (e) =>
            foreach (Entity e in ObjectManager.AllObjects)
            {
                if (!e.Visible)
                {
                    continue;
                }

                //return;
                // No need to run this block on UI thread.
                // It can be ran in parallel.

                // Calculate point relative to real world, 
                // then express those coordinates in AR world.
                Vector3 realWorldPosition = e.Position.RealWorldPosition;
                // e.Position.RefreshPositionRealWorld(worldConverter);

                // This is needed to correctly calculate direction.
                Vector3 adjustedRealWorldPosition = realWorldPosition;
                float distance = e.DistanceOverride.GetDistance(adjustedRealWorldPosition);
                adjustedRealWorldPosition = Vector3.Normalize(adjustedRealWorldPosition) * distance;

                Vector3 arVector = worldConverter.RealToARWorld(adjustedRealWorldPosition);
                // = GeolocationHelpers.GetVectorToTargetRelativeToNorth(current, e.Location);
                // Convert vector in real world, to vector in phone's world.
                //targetVector = realWorld.ConvertToUCS(realWorldPosition);
                // Convert vector in phone's world, to vector in AR world.
                //targetVector = arWorld.ConvertToWCS(targetVector);

                //Vector3 relative = cameraPosition.Negate().Add(arVector);
                //float distance = e.DistanceOverride.GetDistance(relative);
                //relative  = Vector3.Normalize(relative) * distance;
                //arVector = cameraPosition.Add(relative);

                // For root drawable, local offset is same as world offset.
                e.Drawable.Offset = arVector.Add(e.Offset);

                // Calculate direction for root drawable.
                e.Direction.RefreshDirection(worldConverter, realWorldPosition);
                DirectionParameters direction = e.Direction.Current;

                if (direction.ShouldApply)
                {
                    Vector3 dir = worldConverter.RealToCameraWorld(direction.Direction);
                    float angleDeg = new Vector3(0, 0, -1).GetAngleYDeg(dir);

                    //  Problem is when Camera contains offset in AR world. Direction is expressed
                    //  relative to device, and result is wrong. 2 solutions:
                    //  1)  Remove offset from ar world (only for calculating direction)
                    //  2)  This approach: convert from real to camera world, and then add angle
                    //      offset AR_to_Camera.
                    angleDeg += arToCamera;

                    if (angleDeg < 0)
                    {
                        angleDeg += 360;
                    }

                    e.DirectionOffset = angleDeg;
                    e.NorthHeading = angleDeg + OrientationMonitor.MagneticNorthDeg;
                    if (e.NorthHeading > 360)
                    {
                        e.NorthHeading -= 360;
                    }

                    // No need to include UserRotation, because for root Drawable it is 0.
                    e.Rotation =
                        Quaternion.CreateFromAxisAngle(
                            new Vector3(0, 1, 0),
                            (float)(angleDeg * Math.PI / 180));
                }

                foreach (Drawable d in e.Drawable.GetAllChildren())
                {
                    if (!d.Visible)
                    {
                        continue;
                    }

                    if (d.Direction == null)
                    {
                        continue;
                    }

                    d.Direction.RefreshDirection(worldConverter, realWorldPosition);
                    direction = d.Direction.Current;

                    if (!direction.ShouldApply)
                    {
                        continue;
                    }

                    //var dir = realWorld.ConvertToUCS(direction.Direction);
                    //dir = arWorld.ConvertToWCS(dir);
                    Vector3 dir = worldConverter.RealToARWorld(direction.Direction);
                    float angleRad = new Vector3(0, 0, -1).GetAngleYRad(dir);
                    Quaternion rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 1, 0), angleRad);

                    if (d.UserRotation.IsIdentity)
                    {
                        d.Rotation = rotation;
                    }
                    else
                    {
                        d.Rotation = Quaternion.Multiply(d.Rotation, rotation);
                    }
                }
            }
            //);

            // Must be on UI thread (i think so).
            foreach (Entity v in ObjectManager.AllObjects)
            {
                if (v.Visible)
                {
                    Refresh(v);
                }
            }
        }

        public void Refresh(Entity entity)
        {
            entity.Drawable.RefreshDisplayedObject();
            foreach (Drawable drawable in entity.Drawable.GetAllChildren())
            {
                if (!drawable.Visible)
                {
                    continue;
                }

                drawable.RefreshDisplayedObject();
            }
        }

        public void Stop()
        {
            DisplayService.RefreshFrame -= Update;
        }
    }
}
