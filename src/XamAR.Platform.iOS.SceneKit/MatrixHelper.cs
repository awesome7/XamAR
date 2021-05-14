using System.Numerics;
using SceneKit;

namespace XamAR.Platform.iOS.SceneKit
{
    public static class MatrixHelper
    {
        //NOTE Changing directly field of SCNMatrix4 is ignored by ARKit. Therefore 
        //NOTE new matrix is created.

        public static Vector3 GetXAxis(this SCNMatrix4 m) => new Vector3(m.M11, m.M12, m.M13);
        public static Vector3 GetYAxis(this SCNMatrix4 m) => new Vector3(m.M21, m.M22, m.M23);
        public static Vector3 GetZAxis(this SCNMatrix4 m) => new Vector3(m.M31, m.M32, m.M33);
        public static Vector3 GetTranslation(this SCNMatrix4 m) => new Vector3(m.M41, m.M42, m.M43);

        public static SCNMatrix4 SetXAxis(this SCNMatrix4 m, SCNVector3 v)
        {
            return new SCNMatrix4(
                v.X, v.Y, v.Z, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
                );
        }
        public static SCNMatrix4 SetYAxis(this SCNMatrix4 m, SCNVector3 v)
        {
            return new SCNMatrix4(
                m.M11, m.M12, m.M13, m.M14,
                v.X, v.Y, v.Z, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
                );
        }
        public static SCNMatrix4 SetZAxis(this SCNMatrix4 m, SCNVector3 v)
        {
            return new SCNMatrix4(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                v.X, v.Y, v.Z, m.M24,
                m.M41, m.M42, m.M43, m.M44
                );
        }
        public static SCNMatrix4 SetTranslation(this SCNMatrix4 m, SCNVector3 v)
        {
            return new SCNMatrix4(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                v.X, v.Y, v.Z, m.M44
                );
        }

        public static SCNMatrix4 ToAR(this Matrix4x4 m)
        {
            return new SCNMatrix4(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
                );
        }
        public static SCNMatrix4 ToAR(this OpenTK.NMatrix4 m)
        {
            return new SCNMatrix4(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
                );
        }
        public static OpenTK.Matrix4 ToTk(this SCNMatrix4 m)
        {
            return new OpenTK.Matrix4(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
                );
        }
        public static Matrix4x4 FromAR(this SCNMatrix4 m)
        {
            return new Matrix4x4(
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
                );
        }
    }
}