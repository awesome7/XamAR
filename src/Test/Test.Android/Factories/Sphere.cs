using System.Numerics;
using Google.AR.Sceneform;
using Google.AR.Sceneform.Rendering;
using XamAR.Core.Models;
using XamAR.Factory;
using XamAR.Platform.Android.Sceneform;
using XamAR.Platform.Android.Sceneform.Extensions;

namespace Test.Droid.Factories
{
    public class Sphere : ModelFactory
    {
        public override ARModel CreateModel()
        {
            var nodeSphere = new Node() // fragment.TransformationSystem);
            {
                LocalPosition = new Vector3().ToAR()
            };

            if (true)
            {

                ModelRenderable model;
                MaterialFactory
                    .MakeOpaqueWithColor(Android.App.Application.Context,
                        new Google.AR.Sceneform.Rendering.Color(100, 150, 40))
                    .ThenAccept(
                        new DelegateConsumer<Material>((m) =>
                        {
                            model = ShapeFactory.MakeSphere(0.1f, new Vector3(0, 0, 0).ToAR(), m);
                            nodeSphere.Renderable = model;
                        })
                    );
            }

            return nodeSphere.AsARModel();
        }
    }
}
