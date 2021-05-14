using System.Numerics;
using SceneKit;
using UIKit;
using XamAR.Core.Models;
using XamAR.Factory;
using XamAR.Platform.iOS.SceneKit.Extensions;
using XamAR.Platform.iOS.SceneKit.Geometry;

namespace Test.iOS.Factories
{
    public class Text : ModelFactory<string>
    {
        public override ARModel CreateModel(string tag)
        {
            LabelConfig config = new LabelConfig()
            {
                Text = tag,
                Height = 0.3f,
                FontSize = 15,
                TextColor = UIColor.Blue,
                BackgroundColor = UIColor.FromRGBA(255, 255, 255, 100)
            };

            var node = SCNGeometryHelpers.CreateLabel(config);
            node.Position = new SCNVector3();
            node.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, 0).ToVector();

            return node.AsModelWrapper();
        }
    }
}
