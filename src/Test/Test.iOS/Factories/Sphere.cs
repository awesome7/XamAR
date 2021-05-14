using System;
using System.Numerics;
using SceneKit;
using UIKit;
using XamAR.Core.Models;
using XamAR.Factory;
using XamAR.Platform.iOS.SceneKit.Extensions;
using XamAR.Platform.iOS.SceneKit.Geometry;

namespace Test.iOS.Factories
{
    public class Sphere : ModelFactory
    {
        public override ARModel CreateModel()
        {
            SCNVector3 position = new SCNVector3();
            SphereConfig config = new SphereConfig()
            {
                Center = position,
                Radius = 0.1f,
                Color = UIColor.LightGray
            };

            SCNNode node = SCNGeometryHelpers.CreateSphere(config);
            node.Position = new SCNVector3();
            node.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, (float)(-15 * Math.PI / 180)).ToVector();

            return node.AsModelWrapper();
        }
    }
}
