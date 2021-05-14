using SceneKit;
using UIKit;

namespace XamAR.Platform.iOS.SceneKit.Geometry
{
    public class BoxConfig
    {
        public string Name { get; set; } = "";

        /// <summary>
        ///     Along X axis.
        /// </summary>
        public float Length { get; set; } = 1;

        /// <summary>
        ///     Along Y axis.
        /// </summary>
        public float Height { get; set; } = 1;

        /// <summary>
        ///     Along Z axis.
        /// </summary>
        public float Depth { get; set; } = 1f;

        public float ChamferRadius { get; set; } = 0.1f;

        public UIColor Color { get; set; } = UIColor.Red;

        public SCNVector3 Center { get; set; } = new SCNVector3();
    }
}
