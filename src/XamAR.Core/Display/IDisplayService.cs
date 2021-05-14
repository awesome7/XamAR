using System;
using System.Numerics;
using XamAR.Core.Events;
using XamAR.Core.Models;

namespace XamAR.Core.Display
{
    /// <summary>
    /// Service responsible for interaction
    /// with a platform (displays object on the screen).
    /// </summary>
    public interface IDisplayService
    {
        /// <summary>
        /// Raised when new AR prepared frame to
        /// be displayed on screen.
        /// <para>(camera position is refreshed, and model's geometries
        /// can be recalculated)</para>
        /// </summary>
        event Action RefreshFrame;

        /// <summary>
        /// Raised when object is pressed.
        /// </summary>
        event Action<object> Pressed;

        /// <summary>
        /// Raised when plane is recognized and tapped.
        /// </summary>
        event Action<TappedPlaneEventArgs> PlaneTapped;

        /// <summary>
        /// Position of camera relative to AR world center.
        /// </summary>
        Vector3 CameraPosition { get; }

        /// <summary>
        /// Direction of camera in to AR world (towards coordinate center).
        /// </summary>
        Vector3 CameraDirection { get; }

        /// <summary>
        /// Up vector of camera (while tracking AR, it is Y axis).
        /// However, we need to support any orientation.
        /// </summary>
        Vector3 CameraUp { get; }

        /// <summary>
        /// Adds drawable object do display scene.
        /// Returns drawable which is root for all provided drawables.
        /// </summary>
        Drawable Add(params Drawable[] drawables);

        /// <summary>
        /// Removes drawable object from display scene.
        /// </summary>
        /// <param name="drawables"></param>
        void Remove(params Drawable[] drawables);

        void DrawCoordinateSystem();
    }
}
