using System;

namespace XamAR.Core.Geometry
{
    public static class AngleConverter
    {
        public const float RadMultiplier = (float)(Math.PI / 180);

        public const float DegMultiplier = (float)(180 / Math.PI);

        /// <summary>
        /// Converts angle in degrees to radians.
        /// </summary>
        public static float DegToRad(this float degree)
        {
            return degree * RadMultiplier;
        }

        /// <summary>
        /// Converts angle in radians to degrees.
        /// </summary>
        public static float RadToDeg(this float radians)
        {
            return radians * DegMultiplier;
        }

        /// <summary>
        /// Converts angle in degrees to radians.
        /// </summary>
        public static double DegToRad(this double degree)
        {
            return degree * RadMultiplier;
        }

        /// <summary>
        /// Converts angle in radians to degrees.
        /// </summary>
        public static double RadToDeg(this double radians)
        {
            return radians * DegMultiplier;
        }
    }
}
