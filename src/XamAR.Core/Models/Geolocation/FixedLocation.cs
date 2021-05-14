using System.Numerics;
using XamAR.Core.Geometry;
using XamAR.Core.Models.Position;
using XamAR.Core.Sensors;

namespace XamAR.Core.Models.Geolocation
{
    /// <summary>
    ///     Fixed GPS location, set by user.
    /// </summary>
    public class FixedLocation : PositionSourceBase
    {
        public FixedLocation(Location location)
        {
            Target = location;
        }

        public float Height { get; set; }

        private Location Target { get; }

        protected override Vector3 GetRealWorld(WorldConverter converter)
        {
            Location current = LocationMonitor.LastLocation;
            Vector3 targetVector = GeolocationHelpers.GetVectorToTargetRelativeToNorth(current, Target);

            return targetVector.GetOnZ(Height);
        }

        public new Vector3 GetPositionInARWorld(WorldConverter converter)
        {
            return converter.RealToARWorld(RealWorldPosition);
        }
    }
}
