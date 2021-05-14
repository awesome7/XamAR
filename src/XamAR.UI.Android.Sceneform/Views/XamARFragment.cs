using System;
using Android.OS;
using DryIoc;
using Google.AR.Sceneform.UX;
using XamAR.Core;
using XamAR.Core.Display;
using XamAR.Core.Events;
using XamAR.Platform.Android.Sceneform;
using XamAR.Platform.Android.Sceneform.Events;
using XamAR.UI.Android.Sceneform.Display;
using XamAR.UI.Android.Sceneform.Events;

namespace XamAR.UI.Android.Sceneform.Views
{
    public class XamARFragment : ArFragment
    {
        public event Action<TappedPlaneEventArgs> PlaneTapped;

        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            ((DisplayAndroid)DI.Container.Resolve<IDisplayService>()).Attach(this);
            
            // This overrides TapArPlane.
            SetOnTapArPlaneListener(new TapListener());

            // This must be set after SetOnTapArPlaneListener.
            TapArPlane += MyArFragment_TapArPlane;
        }

        private void MyArFragment_TapArPlane(object sender, TapArPlaneEventArgs e)
        {
            PlaneTapped?.Invoke(new TappedPlaneEventArgsAndroid(e));
            //var anchor = e.HitResult.CreateAnchor();
            //var node = new AnchorNode(anchor);
            /*var anchor = e.HitResult.CreateAnchor();
            var anchorNode = new AnchorNode(anchor);
            anchorNode.SetParent(ArSceneView.Scene);
            ModelRenderable model;
            MaterialFactory
                .MakeOpaqueWithColor(Context, new Color(100, 150, 40))
                .ThenAccept(
                    new Consumer<Material>((m) =>
                    {
                        model = ShapeFactory.MakeSphere(0.1f, new Google.AR.Sceneform.Math.Vector3(0, 0, 0), m);
                        var tn = new TransformableNode(TransformationSystem);
                        tn.Tap += (o, ee) =>
                        {
                            eventSource.InvokePressed(tn);
                        };
                        tn.SetParent(anchorNode);
                        tn.Renderable = model;
                    })
            );
            var text = new TextView(Context);
            text.Text = "Test";
            text.SetTextColor(Android.Graphics.Color.AliceBlue);
            text.SetWidth(100);
            text.SetHeight(60);
            Renderable textRenderable = null;
            ViewRenderable.InvokeBuilder()
                .SetView(Context, text)
                .Build()
                .ThenAccept(
                    new Consumer<Renderable>((r) =>
                    {
                        textRenderable = r;
                        var tn = new TransformableNode(TransformationSystem);
                        tn.LocalPosition = new Google.AR.Sceneform.Math.Vector3(0, 0.1f, 0);
                        tn.SetParent(anchorNode);
                        tn.Renderable = textRenderable;
                        anchorNode.AddChild(tn);
                    }
                ));
            ArSceneView.Scene.AddChild(anchorNode);*/
        }
    }
}
