using System.Numerics;

namespace XamAR.Core.Geometry
{
    public static class MatrixExtensions
    {
        /// <summary>
        ///     Extracts X axis from matrix.
        /// </summary>
        public static Vector3 GetVectorX(this Matrix4x4 m)
        {
            return new Vector3(m.M11, m.M12, m.M13);
        }

        /// <summary>
        ///     Extracts Y axis from matrix.
        /// </summary>
        public static Vector3 GetVectorY(this Matrix4x4 m)
        {
            return new Vector3(m.M21, m.M22, m.M23);
        }

        /// <summary>
        ///     Extracts Z axis from matrix.
        /// </summary>
        public static Vector3 GetVectorZ(this Matrix4x4 m)
        {
            return new Vector3(m.M31, m.M32, m.M33);
        }

        /// <summary>
        ///     Extracts Translation from matrix.
        /// </summary>
        public static Vector3 GetVectorTranslation(this Matrix4x4 m)
        {
            return new Vector3(m.M41, m.M42, m.M43);
        }

        public static void SetAxisX(this Matrix4x4 m, Vector3 x)
        {
            m.M11 = x.X;
            m.M12 = x.Y;
            m.M13 = x.Z;
        }

        public static void SetAxisY(this Matrix4x4 m, Vector3 y)
        {
            m.M21 = y.X;
            m.M22 = y.Y;
            m.M23 = y.Z;
        }

        public static void SetAxisZ(this Matrix4x4 m, Vector3 z)
        {
            m.M31 = z.X;
            m.M32 = z.Y;
            m.M33 = z.Z;
        }

        public static void SetTranslation(this Matrix4x4 m, Vector3 t)
        {
            m.M41 = t.X;
            m.M42 = t.Y;
            m.M43 = t.Z;
        }

        /// <summary>
        ///     Creates matrix from given axes and translation.
        /// </summary>
        public static Matrix4x4 Create(Vector3 x, Vector3 y, Vector3 z, Vector3? translation = null)
        {
            Vector3 t = translation ?? new Vector3();

            return new Matrix4x4(
                x.X, x.Y, x.Z, 0,
                y.X, y.Y, y.Z, 0,
                z.X, z.Y, z.Z, 0,
                t.X, t.Y, t.Z, 1
            );
        }

        /// <summary>
        ///     Multiplies current matrix (right) with provided left matrix (left * right).
        ///     (in terms of creating transform matrix, first right transformation is
        ///     applied, and then left).
        /// </summary>
        /// <remarks>
        ///     IMPORTANT: System numerics has reversed order of multiplication
        ///     (left * right) applies first left and then right transformation. OpenGL
        ///     is working by math rules.
        /// </remarks>
        /// <param name="right"></param>
        /// <param name="left"></param>
        /// <returns></returns>
        public static Matrix4x4 MultiplyLeft(this Matrix4x4 right, Matrix4x4 left)
        {
            return Matrix4x4.Multiply(right, left);
        }

        /// <summary>
        ///     Check MultiplyLeft method for description.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="right"></param>
        /// <returns></returns>
        public static Matrix4x4 MultiplyRight(this Matrix4x4 left, Matrix4x4 right)
        {
            return Matrix4x4.Multiply(right, left);
        }

        /// <summary>
        ///     Convert array (of 16 elements) to Matrix4x4.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static Matrix4x4 ToMatrix(this float[] matrix)
        {
            float[] m = matrix;
            return new Matrix4x4(
                m[0], m[1], m[2], m[3],
                m[4], m[5], m[6], m[7],
                m[8], m[9], m[10], m[11],
                m[12], m[13], m[14], m[15]
            );
        }

        /// <summary>
        ///     Converts Matrix4x4 to array of 16.
        /// </summary>
        public static float[] ToArray(this Matrix4x4 m)
        {
            return new[]
            {
                m.M11, m.M12, m.M13, m.M14,
                m.M21, m.M22, m.M23, m.M24,
                m.M31, m.M32, m.M33, m.M34,
                m.M41, m.M42, m.M43, m.M44
            };
        }

        public static string MatrixString(Matrix4x4 m)
        {
            return m.ToArray().MatrixString();
        }

        public static string MatrixString(this float[] matrix)
        {
            string Fun(int j) => $"{matrix[j],5:0.00}";

            string[] s = new string[4];

            s[0] = $"{Fun(0)}, {Fun(1)}, {Fun(2)}, {Fun(3)} ";

            int i = 4;
            s[1] = $"{Fun(i + 0)}, {Fun(i + 1)}, {Fun(i + 2)}, {Fun(i + 3)} ";

            i = 8;
            s[2] = $"{Fun(i + 0)}, {Fun(i + 1)}, {Fun(i + 2)}, {Fun(i + 3)} ";

            i = 12;
            s[3] = $"{Fun(i + 0)}, {Fun(i + 1)}, {Fun(i + 2)}, {Fun(i + 3)} ";

            string text = string.Join("\n", s);

            return text;
        }
    }
}
