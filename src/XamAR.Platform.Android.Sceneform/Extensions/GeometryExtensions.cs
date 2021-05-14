namespace XamAR.Platform.Android.Sceneform.Extensions
{
    public static class GeometryExtensions
    {
        public static Google.AR.Sceneform.Math.Vector3 ToAR(this System.Numerics.Vector3 v)
        {
            return new Google.AR.Sceneform.Math.Vector3(v.X, v.Y, v.Z);
        }

        public static System.Numerics.Vector3 ToNumerics(this Google.AR.Sceneform.Math.Vector3 v)
        {
            return new System.Numerics.Vector3(v.X, v.Y, v.Z);
        }

        public static Google.AR.Sceneform.Math.Quaternion ToAR(this System.Numerics.Quaternion q)
        {
            return new Google.AR.Sceneform.Math.Quaternion(q.X, q.Y, q.Z, q.W);
        }

        public static System.Numerics.Quaternion ToNumerics(this Google.AR.Sceneform.Math.Quaternion q)
        {
            return new System.Numerics.Quaternion(q.X, q.Y, q.Z, q.W);
        }

        public static System.Numerics.Vector3 ToVector(this float[] xyz)
        {
            return new System.Numerics.Vector3(xyz[0], xyz[1], xyz[2]);
        }

        public static float[] ToArray(this System.Numerics.Vector3 v)
        {
            return new[] {v.X, v.Y, v.Z};
        }
    }
}
