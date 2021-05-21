using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.AR.Sceneform;
using Google.AR.Sceneform.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using XamAR.Core.Models;
using XamAR.Factory;
using XamAR.Platform.Android.Sceneform;
using XamAR.Platform.Android.Sceneform.Extensions;

namespace Create3dModel.Droid.Factories
{
    public class Text : ModelFactory<string>
    {
        public override ARModel CreateModel(string tag)
        {
            // Create text.
            var nodeText = new Node()
            {
                LocalPosition = new Vector3(0, 0.2f, 0.0f).ToAR()
            };

            var text = new TextView(Application.Context) { Text = tag };
            text.SetTextColor(Android.Graphics.Color.AliceBlue);
            text.SetBackgroundColor(Android.Graphics.Color.Argb(120, 0, 0, 0));
            int padding = 10;
            text.SetPadding(padding, padding, padding, padding);
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