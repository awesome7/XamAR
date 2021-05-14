using System;

namespace XamAR.Core.Models.Orientation
{
    public interface IOrientationService
    {
        event Action<OrientationChanged> ReadingChanged;

        /// <summary>
        /// Starts listening orientation sensor.
        /// Invokes ReadingChanged event on change.
        /// </summary>
        void StartListening();

        /// <summary>
        /// Stops listening for orientation sensor.
        /// Not invoking ReadingChanged event anymore.
        /// </summary>
        void StopListening();
    }
}
