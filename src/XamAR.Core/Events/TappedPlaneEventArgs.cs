using XamAR.Core.Models.Direction;
using XamAR.Core.Models.Position;

namespace XamAR.Core.Events
{
    public abstract class TappedPlaneEventArgs
    {
        // Platform specific data needed for creating anchor.

        /// <summary>
        /// Returns position related to tapped plane.
        /// </summary>
        public abstract IPositionSource GetPosition();

        /// <summary>
        /// Returns direction related to tapped plane.
        /// </summary>
        public abstract IDirectionSource GetDirection();
    }
}
