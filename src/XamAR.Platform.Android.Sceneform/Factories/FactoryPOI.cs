using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Widget;
using Google.AR.Sceneform;
using Google.AR.Sceneform.Rendering;
using XamAR.Core.Factories;
using XamAR.Core.Models;
using XamAR.Platform.Android.Sceneform.Drawables;

namespace XamAR.Platform.Android.Sceneform.Factories
{
    public class FactoryPOI : IFactoryPOI
    {
        public IEnumerable<Drawable> Create(string label = null)
        {
            List<Drawable> list = new List<Drawable>();
            Context context = Application.Context;

            // Create POI object (sphere for now).
            Node nodeSphere = new Node // fragment.TransformationSystem);
            {
                LocalPosition = new Google.AR.Sceneform.Math.Vector3()
            };

            MaterialFactory
                .MakeOpaqueWithColor(context, new Color(100, 150, 40))
                .ThenAccept(
                    new DelegateConsumer<Material>(m =>
                    {
                        ModelRenderable model = ShapeFactory.MakeSphere(0.1f, new Google.AR.Sceneform.Math.Vector3(0, 0, 0), m);
                        //nodeSphere.LocalPosition = new Google.AR.Sceneform.Math.Vector3(0, 0, -1);
                        nodeSphere.Renderable = model;
                    })
                );

            list.Add(new DrawableNode(nodeSphere));

            // Create text.
            Node nodeText = new Node // fragment.TransformationSystem);
            {
                LocalPosition = new Google.AR.Sceneform.Math.Vector3(0, 0.1f, 0.1f)
            };

            TextView text = new TextView(context)
            {
                Text = label ?? "..."
            };

            text.SetTextColor(global::Android.Graphics.Color.AliceBlue);
            text.SetWidth(100);
            text.SetHeight(60);

            DrawableNode drawableNode = new DrawableNode(nodeText);
            ViewRenderable.InvokeBuilder()
                .SetView(context, text)
                .Build()
                .ThenAccept(
                    new DelegateConsumer<Renderable>(renderable => { nodeText.Renderable = renderable; }
                    ));

            list.Add(drawableNode /* { IsFacingCamera = true }*/);

            // This is when
            //var parentNode = new TransformableNode(fragment.TransformationSystem);
            //nodeSphere.SetParent(parentNode);
            //nodeText.SetParent(parentNode);
            //parentNode.LocalPosition = new Google.AR.Sceneform.Math.Vector3(0, 0.5f, -0.1f);
            //list.Add(new WrapperNode(parentNode));

            return list;
        }
    }
}
