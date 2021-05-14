using System.Numerics;

namespace XamAR.Core.Models.Position
{
    public abstract class AnchorPosition : PositionSourceBase
    {
        public new abstract Vector3 GetPositionInARWorld(WorldConverter converter);

        // protected abstract Vector3 getRealWorld();
    }

    /*   
    public class FixedLocation : IPositionSource
    {
        public float Height { get; set; } 
        private Location target { get; }

        public FixedLocation(Location location)
        {
            this.target = location;
        }


        public Vector3 RefreshPositionRealWorld()
        {
            var current = LocationMonitor.LastLocation;
            Vector3 targetVector = GeolocationHelpers.GetVectorToTargetRelativeToNorth(current, target);
            targetVector = targetVector.GetOnZ(Height);
            return targetVector;
        }

        public Vector3 GetPositionInARWorld(WorldConverter converter)
        {
            var realV = RefreshPositionRealWorld();
            var arV = converter.RealToARWorld(realV);
            return arV;
        }
    }
     */
}
