using System;
using System.Numerics;
using DryIoc;
using XamAR.Core.Geometry;
using XamAR.Core.Models.Orientation;

namespace XamAR.Core.Sensors
{
    /// <summary>
    ///     Tracks orientation of device.
    ///     Needs some update and refactoring.
    /// </summary>
    public static class OrientationMonitor
    {
        /// <summary>
        /// Transformation matrix which transforms point from CCS to WCS 
        /// (coordinates in CCS displays as coordinates in WCS).
        /// </summary>
        //public static Matrix4x4 OrientationMatrix { get; private set; }
        /// <summary>
        /// Convert point from WCS to CCS.
        /// </summary>
        //public static Matrix4x4 OrientationMatrixInverted { get; private set; }

        /// <summary>
        ///     <para>WCS - y is North, x is East, z is height.</para>
        ///     <para>
        ///         UCS - relative to phone: z points from phone towards user (-z is from back camera)
        ///         x is phone's right, y is phone's up.
        ///     </para>
        /// </summary>
        public static WorldTransformation World { get; private set; } = new WorldTransformation();

        /// <summary>
        ///     Tilt angle relative to Portrait mode. Positive is clockwise roll.
        ///     <para>Also works if phone is Pitched.</para>
        /// </summary>
        /// <remarks>
        ///     Tilt angle is calculated as angle between X axis of phone and XY plane of world
        ///     (plane is parallel to world, Z is up in world).
        ///     Angle 0 means that z coordinate (in world) of x axis is 0.
        /// </remarks>
        public static float BankAngleDeg { get; private set; }

        public static float BankAngleRad { get; private set; }

        /// <summary>
        ///     Angle from MagneticNorth from -z axis of the phone (rotating phone with
        ///     negated angle aligns phone with magnetic north).
        ///     <para>Result belongs to [0,360) range, going clockwise.</para>
        /// </summary>
        /// <remarks>Difference to compass could be from reducing noise.</remarks>
        public static float MagneticNorthDeg { get; private set; }

        /// <summary>
        ///     Starts tracking compass.
        ///     <para>Wait 50ms before fetching value.</para>
        /// </summary>
        /// <returns></returns>
        public static void StartTracking()
        {
            IOrientationService orientationService = DI.Container.Resolve<IOrientationService>();

            orientationService.StartListening();
            orientationService.ReadingChanged += OrientationReadingChanged;
        }

        public static void StopTracking()
        {
            IOrientationService orientationService = DI.Container.Resolve<IOrientationService>();

            orientationService.StopListening();
        }

        private static void OrientationReadingChanged(OrientationChanged obj)
        {
            // Orientation is coordinate system of openGL in World coordinate system (WCS).
            // World coordinate system is: x - to East, y - to North, z - up;
            // Example: 
            //      Pointing phone to North (in Portrait mode), with Z axis parallel to ground,
            //      coordinate system of OpenGL will have following coordinates:
            //      (Matrix is expressed in WCS - it shows OpenGL coordinate system in WCS)
            //      x: 1,  0,  0
            //      y: 0,  0,  1 (WCS z axis is Up, OpenGL that is y axis)
            //      z: 0, -1,  0 (WCS y axis is parallel to ground, OpenGL that is z axis)

            Quaternion q = obj.Orientation;
            Matrix4x4 m = Matrix4x4.CreateFromQuaternion(q);

            // Check angle of X axis.
            // If height coordinate is 0 (z), no tilt.
            if (Math.Abs(m.M13) < 0.01f)
            {
                BankAngleDeg = 0;
            }
            else
            {
                Vector3 v = m.GetVectorX();
                // Angle between this vector and Up axis.

                // Transforming to 2d space (to calculate angle between vectors).
                // Component of vector in XY plane.
                double l = Math.Sqrt(v.X * v.X + v.Y * v.Y);

                double d = v.Length();
                double alphaRad = Math.Acos(l / d);
                alphaRad *= -Math.Sign(v.Z);
                float alphaDeg = (float)alphaRad.RadToDeg(); // (float)(radFactor * alphaRad);
                BankAngleDeg = alphaDeg;
                BankAngleRad = (float)alphaRad;
            }

            World = new WorldTransformation(m);
            //OrientationMatrix = m;
            //Matrix4x4 inverted;
            //Matrix4x4.Invert(m, out inverted);
            //OrientationMatrixInverted = inverted;
            UpdateNorth();
        }

        private static void UpdateNorth()
        {
            // Angle between -z axis and magnetic north.
            Vector3 phone = new Vector3(0, 0, -1);
            Vector3 phoneInWcs = World.ConvertToWcs(phone); // Vector3.Transform(phone, OrientationMatrix);

            // Angle to magnetic north
            double angleDeg = Math.Atan2(phoneInWcs.Y, phoneInWcs.X) * 180 / Math.PI;
            // Our 0 is y in WCS, and trigonometric angle 0 is along x axis.
            angleDeg -= 90;
            if (angleDeg < 0)
            {
                angleDeg += 360;
            }

            MagneticNorthDeg = (float)(360 - angleDeg);
        }
    }
}
