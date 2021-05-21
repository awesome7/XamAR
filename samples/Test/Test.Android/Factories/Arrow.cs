using System;
using System.Numerics;
using Google.AR.Sceneform;
using Google.AR.Sceneform.Rendering;
using XamAR.Core.Models;
using XamAR.Factory;
using XamAR.Platform.Android.Sceneform;
using XamAR.Platform.Android.Sceneform.Extensions;

namespace Test.Droid.Factories
{
    public class LeftArrow : ModelFactory
    {
        public override ARModel CreateModel()
        {
            var nodeCube = new Node()// fragment.TransformationSystem);
            {
                LocalPosition = new Vector3().ToAR(),
                LocalRotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, (float)(-15 * Math.PI / 180))
                    .ToAR()
            };

            ModelRenderable model;

            MaterialFactory
                .MakeOpaqueWithColor(Android.App.Application.Context,
                    new Color(200, 150, 40))
                .ThenAccept(
                    new DelegateConsumer<Material>((m) =>
                    {
                        model = ShapeFactory.MakeSphere(0.1f, new Vector3(0, 0, 0).ToAR(), m);
                        model = ShapeFactory.MakeCube(
                            new Google.AR.Sceneform.Math.Vector3(0.05f, 0.05f, 0.5f),
                            new Google.AR.Sceneform.Math.Vector3(-0.1f, 0, -0.2f),
                            m);
                        nodeCube.Renderable = model;
                    })
                );

            return nodeCube.AsARModel();
        }
    }

    public class RightArrow : ModelFactory
    {
        public override ARModel CreateModel()
        {
            var nodeCube = new Node()// fragment.TransformationSystem);
            {
                LocalPosition = new Vector3().ToAR(),
                LocalRotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, (float)(15 * Math.PI / 180))
                    .ToAR()
            };

            ModelRenderable model;

            MaterialFactory
                .MakeOpaqueWithColor(Android.App.Application.Context,
                    new Color(100, 150, 200))
                .ThenAccept(
                    new DelegateConsumer<Material>((m) =>
                    {
                        model = ShapeFactory.MakeSphere(0.1f, new Vector3(0, 0, 0).ToAR(), m);
                        model = ShapeFactory.MakeCube(
                            new Google.AR.Sceneform.Math.Vector3(0.05f, 0.05f, 0.5f),
                            new Google.AR.Sceneform.Math.Vector3(+0.1f, 0, -0.2f),
                            m);
                        nodeCube.Renderable = model;
                    })
                );

            return nodeCube.AsARModel();
        }
    }
}
