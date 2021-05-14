using System.Numerics;

namespace XamAR.Core.Models.Direction
{
    public interface IDirectionSource
    {
        /// <summary>
        /// Last calculated direction in real world coordinate system
        /// (this is excluding device orientation).
        /// (Y is North, X is East).
        /// </summary>
        DirectionParameters Current { get; }

        /// <summary>
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="realWorldPosition">
        /// Real-world vector of position for 
        /// which direction is calculated. Relative to device center.</param>
        void RefreshDirection(WorldConverter converter, Vector3 realWorldPosition);
    }
}
