using System.Numerics;

namespace XamAR.Core.Models.Distance
{
    /// <summary>
    /// Doesn't make any changes to distance.
    /// </summary>
    public class UnchangedDistance : IDistanceOverride
    {
        public float GetDistance(Vector3 position)
        {
            return position.Length();
        }
    }
}
