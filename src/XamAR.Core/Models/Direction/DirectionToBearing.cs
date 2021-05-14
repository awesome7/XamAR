using System;
using System.Numerics;
using XamAR.Core.Geometry;

namespace XamAR.Core.Models.Direction
{
    /// <summary>
    /// Heading is to bearing angle [0-360) relative to North.
    /// (clockwise)
    /// </summary>
    public class DirectionToBearing : DirectionSourceBase
    {
        /// <summary>
        /// 0 is North. Clockwise direction.
        /// </summary>
        public float BearingAngleDeg { get; private set;}

        public float BearingAngleRad { get; private set; }

        /// <summary>
        /// Bearing angle relative to North (clockwise).
        /// </summary>
        public DirectionToBearing(float bearingAngleDeg)
        {
            UpdateBearing(bearingAngleDeg);
        }

        public void UpdateBearing(float bearingDeg)
        {
            BearingAngleDeg = bearingDeg;
            BearingAngleRad = (float)(bearingDeg * Math.PI / 180);
        }

        protected override DirectionParameters GetDirection(WorldConverter converter, Vector3 realWorldPosition)
        {
            //var direction = new Vector3(0, 1, 0);// North.
            //var rotate = Matrix4x4.CreateRotationZ(-BearingAngleRad);
            //var vectorRealWorld = Vector3.Transform(direction, rotate);
            //var vectorCamera = OrientationMonitor.World.ConvertToUCS(vectorRealWorld);

            // Not sure if needed.
            //var north = OrientationMonitor.MagneticNorthDeg;
            //var angleDeg = -north + BearingAngleDeg;
            //var angleRad = (float)(angleDeg * Math.PI / 180);

            Vector2 vectorRealWorld = GeolocationHelpers.CreateFromHeading(BearingAngleRad);
            return new DirectionParameters(vectorRealWorld.Get3d());
        }
    }
}
