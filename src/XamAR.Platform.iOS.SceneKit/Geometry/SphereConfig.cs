using SceneKit;
using UIKit;

namespace XamAR.Platform.iOS.SceneKit.Geometry
{
    public class SphereConfig
    {
        public string Name { get; set; } = string.Empty;

        public float Radius { get; set; } = 1;

        public UIColor Color { get; set; } = UIColor.Red;

        public SCNVector3 Center { get; set; } = new SCNVector3();
    }
}
