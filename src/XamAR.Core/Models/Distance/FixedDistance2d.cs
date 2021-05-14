using System;
using System.Numerics;
using XamAR.Core.Geometry;

namespace XamAR.Core.Models.Distance
{
    /// <summary>
    /// Returns fixed distance of 2d (XY) coordinates.
    /// </summary>
    public class FixedDistance2d : IDistanceOverride
    {
        public float Distance2d { get; }

        public FixedDistance2d(float distance2d)
        {
            Distance2d = distance2d;
        }

        public float GetDistance(Vector3 position)
        {
            Vector3 xz = new Vector3(position.X, 0, position.Z);
            xz = xz.GetUnit() * Distance2d;

            return (float)Math.Sqrt(xz.LengthSquared() + position.Z * position.Z);
        }
    }
}
