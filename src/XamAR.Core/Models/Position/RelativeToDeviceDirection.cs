using System.Numerics;
using XamAR.Core.Sensors;

namespace XamAR.Core.Models.Position
{
    /// <summary>
    ///     Center of coordinate system is in center of the device
    ///     (tracks device direction).
    /// </summary>
    /// <remarks>
    ///     (0,0,-1) is 1 meter in front of device's camera, no matter
    ///     of orientation of the device.
    /// </remarks>
    public class RelativeToDeviceDirection : PositionSourceBase
    {
        private Vector3 LocalPosition { get; }

        public RelativeToDeviceDirection(Vector3 localPosition)
        {
            LocalPosition = localPosition;
        }

        public new Vector3 GetPositionInARWorld(WorldConverter converter)
        {
            //Vector3 realV = RefreshPositionRealWorld();
            return converter.CameraToARWorld(LocalPosition);
        }

        protected override Vector3 GetRealWorld(WorldConverter converter)
        {
            return OrientationMonitor.World.ConvertToWcs(LocalPosition);
        }
    }
}
