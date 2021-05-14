using System.Numerics;
using XamAR.Core.Geometry;

namespace XamAR.Core.Models
{
    /// <summary>
    /// Used for conversion of Vector between worlds.
    /// Encapsulates calculations from EntityUpdateService.
    /// </summary>
    /// <remarks>
    /// Conversion is: Real World - Camera World - AR world.
    /// </remarks>
    public class WorldConverter
    {
        /// <summary>
        /// WCS is real world, UCS is camera world.
        /// </summary>
        public WorldTransformation RealCameraWorld { get; }

        /// <summary>
        /// WCS is AR world, UCS is camera world.
        /// </summary>
        public WorldTransformation ARCameraWorld { get; }

        /// <summary>
        /// Convert real to AR.
        /// </summary>
        public WorldTransformation RealAR { get; }

        public WorldConverter(WorldTransformation realWorldMatrix, WorldTransformation aRWorld)
        {
            RealCameraWorld = realWorldMatrix;
            ARCameraWorld = aRWorld;

            //TODO Something is wrong here, needs more testing.
            Matrix4x4 m = ARCameraWorld.WorldMatrix.MultiplyLeft(RealCameraWorld.WorldMatrixInverted);
            RealAR = new WorldTransformation(m);
        }

        public Vector3 RealToCameraWorld(Vector3 realWorldVector)
        {
            return RealCameraWorld.ConvertToUcs(realWorldVector);
        }

        public Vector3 CameraToARWorld(Vector3 cameraWorldVector)
        {
            return ARCameraWorld.ConvertToWcs(cameraWorldVector);
        }

        public Vector3 RealToARWorld(Vector3 realWorldVector)
        {
            Vector3 p = RealToCameraWorld(realWorldVector);
            Vector3 q = CameraToARWorld(p);

            //TODO Some problems exist, needs more testing.
            Vector3 v = RealAR.ConvertToWcs(realWorldVector);
            return q;
        }

        public Vector3 ArToRealWorld(Vector3 arWorldVector)
        {
            Vector3 p = ARCameraWorld.ConvertToUcs(arWorldVector);
            Vector3 q = RealCameraWorld.ConvertToWcs(p);
            Vector3 v = RealAR.ConvertToUcs(arWorldVector);

            return q;
        }
    }
}
