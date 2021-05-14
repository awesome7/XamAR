using SceneKit;
using UIKit;

namespace XamAR.Platform.iOS.SceneKit.Geometry
{
    public class LineConfig
    {
        public string Name { get; set; } = string.Empty;

        public SCNVector3 Start { get; set; } = new SCNVector3();

        public float Width { get; set; } = 0.1f;

        /// <summary>
        ///     Length along X axis;
        /// </summary>
        public float Length { get; set; } = 1;

        public UIColor Color { get; set; } = UIColor.Black;
    }
}
