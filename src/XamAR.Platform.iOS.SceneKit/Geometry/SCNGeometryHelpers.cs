using CoreGraphics;
using SceneKit;
using SpriteKit;
using UIKit;

namespace XamAR.Platform.iOS.SceneKit.Geometry
{
    public class SCNGeometryHelpers
    {
        public static SCNNode CreatePlane(PlaneConfig config)
        {
            SCNPlane plane = new SCNPlane
            {
                Width = config.Width,
                Height = config.Height
            };

            SCNMaterial planeMaterial = new SCNMaterial();
            planeMaterial.Diffuse.Contents = config.Color;

            SCNNode planeNode = new SCNNode
            {
                Geometry = plane, Name = config.Name,
                Position = config.Center
            };

            return planeNode;
        }

        public static SCNNode CreateBox(BoxConfig config)
        {
            SCNBox box = SCNBox.Create(
                config.Length,
                config.Height,
                config.Depth,
                config.ChamferRadius);

            SCNMaterial material = new SCNMaterial();
            material.Diffuse.Contents = config.Color;
            box.FirstMaterial = material;

            SCNNode node = new SCNNode
            {
                Name = config.Name,
                Geometry = box,
                Position = config.Center
            };

            return node;
        }

        public static SCNNode CreateSphere(SphereConfig config)
        {
            SCNSphere sphere = SCNSphere.Create(config.Radius);
            SCNMaterial material = new SCNMaterial();
            material.Diffuse.Contents = config.Color;
            sphere.FirstMaterial = material;

            SCNNode node = new SCNNode
            {
                Name = config.Name,
                Geometry = sphere,
                Position = config.Center
            };

            return node;
        }

        public static SCNNode CreateLabel(LabelConfig config)
        {
            // Label size tells size on SKView.
            // SKView size is scaled to SCNNode.
            // SCNBox size is size in 3d world.
            UILabel label = new UILabel(
                new CGRect(0, 0, 100, 100))
                {
                    BackgroundColor = config.BackgroundColor,
                    Text = config.Text,
                    Lines = 30
                };

            label.Font = config.Font ?? label.Font.WithSize(config.FontSize);
            SKView view = new SKView(new CGRect(0, 0, 100, 100));
            //view.BackgroundColor = config.BackgroundColor;
            view.AddSubview(label);

            SCNBox box = SCNBox.Create(config.Width, config.Height, 0.1f, 0.3f);
            SCNMaterial material = new SCNMaterial();
            material.Diffuse.Contents = view;
            box.FirstMaterial = material;

            SCNNode node = new SCNNode
            {
                Geometry = box,
                Name = config.Name,
                Position = config.Position
            };

            return node;
        }

        public static SCNNode CreateButton(LabelConfig config)
        {
            UIButton button =
                new UIButton(
                    new CGRect(0, 0, config.Width, config.Height))
                    {
                        BackgroundColor = config.BackgroundColor
                    };

            button.SetTitle(config.Text, UIControlState.Normal);
            button.Font = config.Font ?? button.Font.WithSize(config.FontSize);

            SKView view =
                new SKView(
                    new CGRect(0, 0, config.Width, config.Height))
                    {
                        BackgroundColor = config.BackgroundColor
                    };

            view.AddSubview(button);

            SCNBox box = SCNBox.Create(config.Width, config.Height, 1, 0.3f);
            SCNMaterial material = new SCNMaterial();
            material.Diffuse.Contents = view;
            box.FirstMaterial = material;

            SCNNode node = new SCNNode
            {
                Geometry = box,
                Name = config.Name,
                Position = config.Position
            };

            return node;
        }

        public static SCNNode CreateLine(LineConfig config)
        {
            float length = config.Length;
            SCNMaterial material = new SCNMaterial();
            material.Diffuse.Contents = config.Color;
            SCNBox line = SCNBox.Create(length, config.Width, config.Width, 0.1f);
            line.FirstMaterial = material;

            SCNNode node = new SCNNode
            {
                Geometry = line,
                Position = config.Start
            };

            return node;
        }
    }
}
