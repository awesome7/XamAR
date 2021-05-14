using SceneKit;
using UIKit;

namespace XamAR.Platform.iOS.SceneKit.Geometry
{
    public class LabelConfig
    {
        public string Name { get; set; } = "";

        /// <summary>
        ///     Displayed text.
        /// </summary>
        public string Text { get; set; } = "";

        public float Width { get; set; } = 1;

        public float Height { get; set; } = 1;

        public UIFont Font { get; set; }

        public float FontSize { get; set; } = 5;

        public UIColor TextColor { get; set; } = UIColor.Red;

        public UIColor BackgroundColor { get; set; } = UIColor.Yellow;

        public SCNVector3 Position { get; set; } = new SCNVector3();
    }
}
