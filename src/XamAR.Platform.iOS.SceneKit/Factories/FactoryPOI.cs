using System.Collections.Generic;
using SceneKit;
using UIKit;
using XamAR.Core.Factories;
using XamAR.Core.Models;
using XamAR.Platform.iOS.SceneKit.Drawables;
using XamAR.Platform.iOS.SceneKit.Geometry;

namespace XamAR.Platform.iOS.SceneKit.Factories
{
    public class FactoryPOI : IFactoryPOI
    {
        public IEnumerable<Drawable> Create(string text = null)
        {
            List<Drawable> list = new List<Drawable>();

            SCNNode sphere = SCNGeometryHelpers.CreateSphere(
                new SphereConfig
                {
                    Center = new SCNVector3(0, 0, 0),
                    Radius = 0.2f,
                    Color = UIColor.Blue
                });

            list.Add(new DrawableScnNode(sphere));

            SCNNode label = SCNGeometryHelpers.CreateLabel(
                new LabelConfig
                {
                    BackgroundColor = UIColor.Red,
                    TextColor = UIColor.Cyan,
                    Width = 1,
                    Height = 1,
                    Text = text,
                    Name = "Text_1",
                    Position = new SCNVector3(0, 0.6f, 0)
                });

            list.Add(new DrawableScnNode(label));

            return list;
        }
    }
}
