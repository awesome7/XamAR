using System;
using System.Numerics;
using XamAR.Core.Geometry;
using XamAR.Core.Sensors;

namespace XamAR.Core.Models.Position
{
    /// <summary>
    ///     Using device's world, except z axis is parallel to ground
    ///     (ignores device's pitch - as if device is vertical, and it's Y
    ///     is aligned to real-world's Z).
    /// </summary>
    /// <remarks>
    ///     This is same as RelativeToDeviceDirection, when phone's Y axis is
    ///     collinear (aligned) with world's Z axis.
    /// </remarks>
    public class RelativeToDeviceStabilized : PositionSourceBase
    {
        public new Vector3 GetPositionInARWorld(WorldConverter converter) => throw
            //Vector3 realV = RefreshPositionRealWorld(converter);
            //Vector3 arV = converter.RealToARWorld(realV);
            //return arV;
            new NotImplementedException();

        /// <summary>
        ///     Position is relative to phone's world (-z is in front of phone).
        /// </summary>
        public RelativeToDeviceStabilized(Vector3 localPosition)
        {
            LocalPosition = new Vector3(
                localPosition.X,
                -localPosition.Z,
                localPosition.Y);
        }

        /// <summary>
        ///     Local position in real world.
        /// </summary>
        private Vector3 LocalPosition { get; }

        protected override Vector3 GetRealWorld(WorldConverter converter)
        {
            float north = OrientationMonitor.MagneticNorthDeg;
            float northRad = north.DegToRad();
            Matrix4x4 rotation = Matrix4x4.CreateRotationZ(-northRad);

            return Vector3.Transform(LocalPosition, rotation);
        }
    }
}
