using System.Numerics;
using XamAR.Core.Geometry;
using XamAR.Core.Sensors;

namespace XamAR.Core.Models.Direction
{
    /// <summary>
    ///     Heading is relative to current device's direction.
    ///     0 is -Z of device (0 is always in front of device).
    /// </summary>
    public class DirectionRelativeToDevice : DirectionSourceBase
    {
        public DirectionRelativeToDevice(float headingDeg)
        {
            HeadingDeg = headingDeg;
            HeadingRad = headingDeg.DegToRad();
        }

        public float HeadingDeg { get; }

        public float HeadingRad { get; }

        protected override DirectionParameters GetDirection(WorldConverter converter, Vector3 realWorldPosition)
        {
            float north = OrientationMonitor.MagneticNorthDeg;
            float headingDeg = north + HeadingDeg;
            float headingRad = headingDeg.DegToRad();

            Vector2 v = GeolocationHelpers.CreateFromHeading(headingRad);
            return new DirectionParameters(v.Get3d());
        }
    }
}
