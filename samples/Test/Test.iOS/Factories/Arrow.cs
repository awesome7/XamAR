using System;
using System.Numerics;
using XamAR.Core.Models;
using XamAR.Factory;
using XamAR.Platform.iOS.SceneKit.Extensions;
using XamAR.Platform.iOS.SceneKit.Geometry;

namespace Test.iOS.Factories
{
    public class LeftArrow : ModelFactory
    {
        public override ARModel CreateModel()
        {
            var config = new BoxConfig()
            {
                Length = 0.05f,
                Depth = 0.5f,
                Height = 0.05f
            };

            var node = SCNGeometryHelpers.CreateBox(config);
            node.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, (float)(-15 * Math.PI / 180)).ToVector();
            node.Position = new SceneKit.SCNVector3(-0.1f, 0, -0.2f);

            return node.AsModelWrapper();
        }
    }

    public class RightArrow : ModelFactory
    {
        public override ARModel CreateModel()
        {
            var config = new BoxConfig()
            {
                Length = 0.05f,
                Depth = 0.5f,
                Height = 0.05f
            };

            var node = SCNGeometryHelpers.CreateBox(config);
            node.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, (float)(15 * Math.PI / 180)).ToVector();
            node.Position = new SceneKit.SCNVector3(0.1f, 0, -0.2f);

            return node.AsModelWrapper();
        }
    }
}
