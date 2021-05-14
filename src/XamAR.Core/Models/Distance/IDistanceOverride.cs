using System.Numerics;

namespace XamAR.Core.Models.Distance
{
    /// <summary>
    /// Allows distance to be overriden.
    /// </summary>
    public interface IDistanceOverride
    {
        /// <summary>
        /// Get distance based on position.
        /// </summary>
        /// <remarks>Position is in AR world.</remarks>
        float GetDistance(Vector3 position);
    }
}
