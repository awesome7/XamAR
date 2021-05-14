using System.Numerics;

namespace XamAR.Core.Geometry
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// <list type="bullet">
    /// <item>
    /// WCS (World Coordinate System) - referent coordinate system: 
    ///      <para>(1,0,0)</para>
    ///      <para>(0,1,0)</para>
    ///      <para>(0,0,1)</para>
    /// </item>
    /// <item>
    /// UCS (User Coordinate System) system which is located inside WCS 
    ///      (and can have any transformation). It's axes are described using WCS.
    /// </item>     
    /// </list>
    /// </remarks>
    /// <example>
    ///      Example: UCS which is rotated -90 deg around Z axis 
    ///               (in right-handed system it is in clockwise direction)
    ///               (0, -1, 0)
    ///               (1,  0, 0)
    ///               (0,  0, 1)
    /// </example>
    public class WorldTransformation
    {
        /// <summary>
        /// Use this matrix to convert Vector3d in UCS system to WCS.
        /// </summary>
        /// <example>
        /// UCS center is in (1, 2, 3). Vector3d (0,0,0) in UCS will be (1,2,3) in WCS.
        /// </example>
        public Matrix4x4 WorldMatrix { get; private set; } 

        /// <summary>
        /// Use this matrix to convert Vector3d in WCS system to UCS.
        /// </summary>
        /// <example>
        /// UCS center is in (1, 2, 3). Vector3d (1,2,3) in WCS will be (0,0,0) in UCS.
        /// </example>
        public Matrix4x4 WorldMatrixInverted { get; private set; }

        /// <summary>
        /// Creates Identity world.
        /// </summary>
        public WorldTransformation()
        {
            UpdateWorldMatrix(Matrix4x4.Identity);
        }

        /// <summary>
        /// Creates world from given world matrix.
        /// </summary>
        /// <param name="world"></param>
        public WorldTransformation(Matrix4x4 world)
        {
            UpdateWorldMatrix(world);
        }

        /// <summary>
        /// Creates world for given X axis, and optional Y.
        /// If up is not provided, Y axis is used.
        /// </summary>
        public static WorldTransformation CreateForX(Vector3 xAxis, Vector3? up = null, Vector3? translation = null)
        {
            xAxis = Vector3.Normalize(xAxis);

            Vector3 t = translation ?? new Vector3();
            Vector3 yAxis = up.HasValue ? Vector3.Normalize(up.Value) : Vector3.UnitY;
            Vector3 zAxis = Vector3.Cross(xAxis, yAxis);
            Vector3 yAxisNew = Vector3.Cross(zAxis, xAxis);
            Matrix4x4 w = MatrixExtensions.Create(xAxis, yAxisNew, zAxis, t);

            return new WorldTransformation(w);
        }

        /// <summary>
        /// Creates world for given Z axis, and optional Y.
        /// If up is not provided, Y axis is used.
        /// </summary>
        public static WorldTransformation CreateForZ(Vector3 zAxis, Vector3? up = null, Vector3? translation = null)
        {
            zAxis = Vector3.Normalize(zAxis);

            Vector3 t = translation ?? new Vector3();
            Vector3 yAxis = up.HasValue ? Vector3.Normalize(up.Value) : Vector3.UnitY;
            Vector3 xAxis = Vector3.Cross(yAxis, zAxis);
            Vector3 yAxisNew = Vector3.Cross(zAxis, xAxis);
            Matrix4x4 w = MatrixExtensions.Create(xAxis, yAxisNew, zAxis, t);

            return new WorldTransformation(w);
        }

        private void UpdateWorldMatrix(Matrix4x4 matrix)
        {
            Matrix4x4.Invert(matrix, out Matrix4x4 inverted);

            WorldMatrix = matrix;
            WorldMatrixInverted = inverted;
        }

        /// <summary>
        /// Vector3d in UCS expresses in WCS.
        /// </summary>
        public Vector3 ConvertToWcs(Vector3 ucsVector)
        {
            return Vector3.Transform(ucsVector, WorldMatrix);
        }

        /// <summary>
        /// Vector in WCS expresses in UCS.
        /// </summary>
        /// <param name="wcsVector"></param>
        /// <returns></returns>
        public Vector3 ConvertToUcs(Vector3 wcsVector)
        {
            return Vector3.Transform(wcsVector, WorldMatrixInverted);
        }
    }
}
