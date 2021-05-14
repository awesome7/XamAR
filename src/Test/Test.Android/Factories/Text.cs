using System.Numerics;
using Android.App;
using Android.Widget;
using Google.AR.Sceneform;
using Google.AR.Sceneform.Rendering;
using XamAR.Core.Models;
using XamAR.Factory;
using XamAR.Platform.Android.Sceneform;
using XamAR.Platform.Android.Sceneform.Extensions;

namespace Test.Droid.Factories
{
    public class Text : ModelFactory<string>
    {
        public override ARModel CreateModel(string tag)
        {
            // Create text.
            var nodeText = new Node()// fragment.TransformationSystem);
            {
                LocalPosition = new Vector3(0, 0.0f, 0.0f).ToAR()
            };

            var text = new TextView(Application.Context) {Text = tag};
            text.SetTextColor(Android.Graphics.Color.AliceBlue);
            //text.SetWidth(100);
            //text.SetHeight(60);
            ViewRenderable.InvokeBuilder()
                .SetView(Application.Context, text)
                .Build()
                .ThenAccept(
                    new DelegateConsumer<Renderable>((renderable) =>
                        {
                            nodeText.Renderable = renderable;
                        }
                    ));

            return nodeText.AsARModel();
        }
    }
}
