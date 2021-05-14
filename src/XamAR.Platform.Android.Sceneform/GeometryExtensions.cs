using ar = Google.AR.Sceneform.Math;
using num = System.Numerics;

namespace XamAR.Platform.Android.Sceneform
{
    public static class GeometryExtensions
    {
        public static ar.Vector3 ToAR(this num.Vector3 v)
            => new ar.Vector3(v.X, v.Y, v.Z);
        public static num.Vector3 ToNumerics(this ar.Vector3 v)
            => new num.Vector3(v.X, v.Y, v.Z);

        public static ar.Quaternion ToAR(this num.Quaternion q)
            => new ar.Quaternion(q.X, q.Y, q.Z, q.W);
        public static num.Quaternion ToNumerics(this ar.Quaternion q)
            => new num.Quaternion(q.X, q.Y, q.Z, q.W);

        public static num.Vector3 ToVector(this float[] xyz)
            => new num.Vector3(xyz[0], xyz[1], xyz[2]);

        public static float[] ToArray(this num.Vector3 v)
            => new float[] { v.X, v.Y, v.Z };
    }
}
