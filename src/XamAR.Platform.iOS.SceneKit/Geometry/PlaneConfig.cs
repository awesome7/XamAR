using SceneKit;
using UIKit;

namespace XamAR.Platform.iOS.SceneKit.Geometry
{
    public class PlaneConfig
    {
        public string Name { get; set; } = string.Empty;

        public float Width { get; set; } = 1;

        public float Height { get; set; } = 1;

        public UIColor Color { get; set; } = UIColor.Blue;

        public SCNVector3 Center { get; set; } = new SCNVector3(0, 0, 0);
    }
}
