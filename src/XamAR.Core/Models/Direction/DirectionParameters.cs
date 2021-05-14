using System;
using System.Numerics;
using XamAR.Core.Geometry;

namespace XamAR.Core.Models.Direction
{
    public readonly struct DirectionParameters
    {
        /// <summary>
        ///     If false, don't change default direction
        ///     (don't apply this).
        /// </summary>
        public bool ShouldApply { get; }

        /// <summary>
        ///     Direction vector in real world.
        /// </summary>
        public Vector3 Direction { get; }

        /// <summary>
        ///     Heading relative to North.
        /// </summary>
        public float HeadingNorthDeg { get; }

        /// <summary>
        ///     Direction is in real-world.
        ///     (0,1,0) is North (current orientation of device is irrelevant).
        /// </summary>
        public DirectionParameters(Vector3 direction, bool shouldApply = true)
        {
            Direction = direction;
            ShouldApply = shouldApply;
            HeadingNorthDeg = 0;

            if (!shouldApply)
            {
                return;
            }

            float angleDeg = new Vector3(0, 1, 0).GetAngleZDeg(direction);
            angleDeg = -angleDeg;
            if (angleDeg < 0)
            {
                angleDeg += 360;
            }
            //angleDeg = 360 - angleDeg;

            HeadingNorthDeg = angleDeg;
        }

        /// <summary>
        ///     Heading is relative to North (irrelevant of device's current orientation).
        /// </summary>
        public DirectionParameters(float headingDeg, bool shouldApply = true)
        {
            ShouldApply = shouldApply;
            HeadingNorthDeg = headingDeg;
            if (!shouldApply)
            {
                Direction = new Vector3(0, 1, 0);
                return;
            }

            float headingRad = (float)(headingDeg * Math.PI / 180);
            Matrix4x4 rotate = Matrix4x4.CreateRotationZ(-headingRad);
            Direction = Vector3.Transform(new Vector3(0, 1, 0), rotate);
        }
    }
}
