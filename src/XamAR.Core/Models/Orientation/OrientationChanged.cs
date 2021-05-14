using System.Numerics;

namespace XamAR.Core.Models.Orientation
{
    public class OrientationChanged
    {
        /// <summary>
        /// Current orientation, as Quaternion.
        /// <para></para>
        /// </summary>
        /// <remarks>
        /// Orientation is coordinate system of openGL in World coordinate system (WCS).
        /// World coordinate system is: x - to East, y - to North, z - up;
        /// Example: 
        ///      Pointing phone to North (in Portrait mode), with Z axis parallel to ground,
        ///      coordinate system of OpenGL will have following coordinates:
        ///      (Matrix is expressed in WCS - it shows OpenGL coordinate system in WCS)
        ///      x: 1,  0,  0
        ///      y: 0,  0,  1 (WCS z axis is Up, OpenGL that is y axis)
        ///      z: 0, -1,  0 (WCS y axis is parallel to ground, OpenGL that is z axis)
        /// </remarks>
        public Quaternion Orientation { get; set; }
    }
}
