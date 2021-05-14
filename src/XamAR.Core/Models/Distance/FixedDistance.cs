using System.Numerics;

namespace XamAR.Core.Models.Distance
{
    /// <summary>
    /// Returns fixed distance provided to constructor.
    /// Real distance is ignored.
    /// </summary>
    public class FixedDistance : IDistanceOverride
    {
        public float Distance { get; }

        public FixedDistance(float distance)
        {
            Distance = distance;
        }

        public float GetDistance(Vector3 realDistance)
        {
            return Distance;
        }
    }
}
