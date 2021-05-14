using System;
using System.Numerics;

namespace XamAR.Core.Geometry
{
    public static class VectorExtensions
    {
        /// <summary>
        ///     Angle between vectors around Y axis, in right-handed system.
        ///     <para>right-handed: rotate v1 to v2 in counterclockwise direction.</para>
        /// </summary>
        /// <returns>Angle in radians (-Pi,Pi] .</returns>
        public static float GetAngleYRad(this Vector3 v1, Vector3 v2)
        {
            //throw new NotImplementedException();
            Vector2 p1 = new Vector2(v1.X, v1.Z); // v1.Get2d();
            Vector2 p2 = new Vector2(v2.X, v2.Z); // v2.Get2d();
            double length1 = p1.Length();
            double length2 = p2.Length();

            if (length1 < 1e-4 || length2 < 1e-4)
            {
                throw new NotSupportedException("Angle around Y axis can't be determined!");
            }

            // Not sure for now, it seems formula must be implemented.
            // ArcCos returns [0-Pi], and we need [0-2Pi)
            // https://www.omnicalculator.com/math/angle-between-two-vectors
            Vector3 p3 = Vector3.Cross(v1, v2);
            // p1 * p2 = |p1| * |p2| * cos(alpha)
            double cosA = Vector2.Dot(p1, p2) / (p1.Length() * p2.Length());
            double alpha = Math.Acos(cosA);
            // -180 degree is returned as 180 degree.
            if (p3.Y < 0 && alpha < Math.PI)
            {
                alpha = -alpha;
            }

            //if (alpha < 0)
            //alpha += Math.PI * 2;
            return (float)alpha;
        }

        /// <summary>
        ///     Angle between vectors around Y axis, in right-handed system.
        ///     <para>right-handed: rotate v1 to v2 in counterclockwise direction.</para>
        /// </summary>
        /// <returns>Angle in radians (-180,180] .</returns>
        public static float GetAngleYDeg(this Vector3 v1, Vector3 v2)
        {
            return v1.GetAngleYRad(v2).RadToDeg();
        }

        /// <summary>
        ///     Angle between vectors around Z axis, in right-handed system.
        ///     <para>right-handed: rotate v1 to v2 in counterclockwise direction.</para>
        /// </summary>
        /// <returns>Angle in radians (-Pi,Pi] .</returns>
        public static float GetAngleZRad(this Vector3 v1, Vector3 v2)
        {
            //throw new NotImplementedException();
            Vector2 p1 = new Vector2(v1.X, v1.Y); // v1.Get2d();
            Vector2 p2 = new Vector2(v2.X, v2.Y); // v2.Get2d();
            double length1 = p1.Length();
            double length2 = p2.Length();

            if (length1 < 1e-4 || length2 < 1e-4)
            {
                throw new NotSupportedException("Angle around Y axis can't be determined!");
            }

            // Not sure for now, it seems formula must be implemented.
            // ArcCos returns [0-Pi], and we need [0-2Pi)
            // https://www.omnicalculator.com/math/angle-between-two-vectors
            Vector3 p3 = Vector3.Cross(v1, v2);
            // p1 * p2 = |p1| * |p2| * cos(alpha)
            double cosA = Vector2.Dot(p1, p2) / (p1.Length() * p2.Length());
            double alpha = Math.Acos(cosA);
            // -180 degree is returned as 180 degree.
            if (p3.Z < 0 && alpha < Math.PI)
            {
                alpha = -alpha;
            }

            if (alpha < 0)
            {
                alpha += Math.PI * 2;
            }

            return (float)alpha;
        }

        /// <summary>
        ///     Angle between vectors around Z axis, in right-handed system.
        ///     <para>right-handed: rotate v1 to v2 in counterclockwise direction.</para>
        /// </summary>
        /// <returns>Angle in radians (-180,180] .</returns>
        public static float GetAngleZDeg(this Vector3 v1, Vector3 v2)
        {
            return v1.GetAngleZRad(v2).RadToDeg();
        }


        /// <summary>
        ///     Returns vector on new Z coordinate.
        /// </summary>
        public static Vector3 GetOnZ(this Vector3 v, float z = 0)
        {
            return new Vector3(v.X, v.Y, z);
        }

        /// <summary>
        ///     Returns vector on new Z coordinate.
        /// </summary>
        public static Vector3 GetOnY(this Vector3 v, float y = 0)
        {
            return new Vector3(v.X, y, v.Z);
        }

        /// <summary>
        ///     Returns 2d vector (Z coordinate is removed).
        /// </summary>
        public static Vector2 Get2d(this Vector3 v)
        {
            return new Vector2(v.X, v.Y);
        }

        /// <summary>
        ///     Returns 3d vector with provided Z coordinate.
        /// </summary>
        public static Vector3 Get3d(this Vector2 v, float z = 0)
        {
            return new Vector3(v.X, v.Y, z);
        }

        /// <summary>
        ///     Returns vector with same direction but length of 1.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 GetUnit(this Vector3 v)
        {
            return Vector3.Normalize(v);
        }

        public static Vector3 Add(this Vector3 v1, Vector3 v2)
        {
            return Vector3.Add(v1, v2);
        }

        public static Vector3 Negate(this Vector3 v)
        {
            return Vector3.Negate(v);
        }

        public static string VectorString(this Vector3 v)
        {
            return $"X:{v.X:0.00} Y:{v.Y:0.00} Z:{v.Z:0.00}";
        }
    }
}
