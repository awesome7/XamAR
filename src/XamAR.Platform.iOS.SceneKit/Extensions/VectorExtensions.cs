using System.Numerics;
using SceneKit;

namespace XamAR.Platform.iOS.SceneKit.Extensions
{
    public static class VectorExtensions
    {
        public static SCNVector3 Convert(this Vector3 v)
        {
            return new SCNVector3(v.X, v.Y, v.Z);
        }

        public static Vector3 ToNumerics(this SCNVector3 v)
        {
            return new Vector3(v.X, v.Y, v.Z);
        }

        public static OpenTK.Vector3 ConvertTo(this SCNVector3 v)
        {
            return new OpenTK.Vector3(v.X, v.Y, v.Z);
        }

        public static Quaternion ToQuaternion(this SCNVector4 v)
        {
            return new Quaternion(v.X, v.Y, v.Z, v.W);
        }

        public static SCNVector4 ToVector(this Quaternion q)
        {
            return new SCNVector4(q.X, q.Y, q.Z, q.W);
        }

        public static SCNQuaternion ToAR(this Quaternion q)
        {
            return new SCNQuaternion(q.X, q.Y, q.Z, q.W);
        }

        public static Quaternion ToQuaternion(this SCNQuaternion q)
        {
            return new Quaternion(q.X, q.Y, q.Z, q.W);
        }
    }
}
