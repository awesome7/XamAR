using System;
using System.Collections.Generic;
using System.Numerics;
using Google.AR.Sceneform;
using Google.AR.Sceneform.Rendering;
using Google.AR.Sceneform.UX;
using XamAR.Core.Display;
using XamAR.Core.Events;
using XamAR.Core.Models;
using XamAR.Platform.Android.Sceneform;
using XamAR.Platform.Android.Sceneform.Drawables;
using XamAR.Platform.Android.Sceneform.Extensions;
using XamAR.UI.Android.Sceneform.Views;

namespace XamAR.UI.Android.Sceneform.Display
{
    public class DisplayAndroid : IDisplayService
    {
        private ArFragment Fragment { get; set; }
        public event Action RefreshFrame;
        public event Action<object> Pressed;
        public event Action<TappedPlaneEventArgs> PlaneTapped;

        public Vector3 CameraPosition { get; private set; }
        // => Fragment.ArSceneView.Scene.Camera.LocalPosition.ToNumerics();

        public Vector3 CameraDirection { get; private set; } = new Vector3(0, 0, -1);
        // => Fragment.ArSceneView.Scene.Camera.Forward.ToNumerics();

        public Vector3 CameraUp { get; private set; } = Vector3.UnitY;
        // => Fragment.ArSceneView.Scene.Camera.Up.ToNumerics();

        public Drawable Add(params Drawable[] drawables)
        {
            DrawableNode root = CreateRootDrawable(drawables);
            root.Node.SetParent(Fragment.ArSceneView.Scene);
            root.Node.Tap += (_, __) => Pressed?.Invoke(root.Node);

            // It seems it is enough to have only top TransformableNode, 
            // to handle Tap event.
            // Good thing about this solution is it doesn't depend on user's Nodes type.
            /*foreach (var d in drawables)
            {
                var wrapper = (WrapperNode)d;
                //// Add object to Scene.
                //wrapper.Node.SetParent(Fragment.ArSceneView.Scene);
                //Fragment.ArSceneView.Scene.AddChild(wrapper.Node);
                if (wrapper.Node is TransformableNode tn)
                {
                    //tn.Tap += (_, __) => Pressed?.Invoke(wrapper.Node);
                }
                else
                {
                    Log.Info("ARDisplay", "Drawable is not TransformableNode - " +
                        "touch and motion events won't work.");
                }
            }*/

            return root;
        }

        public void Remove(params Drawable[] drawables)
        {
            foreach (Drawable d in drawables)
            {
                Fragment.ArSceneView.Scene.RemoveChild(((DrawableNode)d).Node);
            }
        }

        public void DrawCoordinateSystem()
        {
            const float thickness = 0.01f;
            const float length = 0.3f;

            Node CreateNode(Color c, Google.AR.Sceneform.Math.Vector3 v)
            {
                Node node = new Node();
                node.SetParent(Fragment.ArSceneView.Scene);
                MaterialFactory.MakeOpaqueWithColor(Fragment.Context, c)
                    .ThenAccept(new DelegateConsumer<Material>(m =>
                    {
                        ModelRenderable model = ShapeFactory.MakeCube(v, v.Scaled(0.5f), m);
                        node.Renderable = model;
                    }));
                return node;
            }

            // ReSharper disable once UnusedVariable
            Node xAxis = CreateNode(
                new Color(255, 0, 0),
                new Google.AR.Sceneform.Math.Vector3(length, thickness, thickness));

            // ReSharper disable once UnusedVariable
            Node yAxis = CreateNode(
                new Color(0, 255, 0),
                new Google.AR.Sceneform.Math.Vector3(thickness, length, thickness));

            // ReSharper disable once UnusedVariable
            Node zAxis = CreateNode(
                new Color(0, 0, 255),
                new Google.AR.Sceneform.Math.Vector3(thickness, thickness, length));
        }

        public void Attach(XamARFragment fragment)
        {
            Fragment = fragment;

            fragment.SessionInitialization += FragmentSessionInitialization; //.ArSceneView.Scene.Update += frameReady;
            fragment.PlaneTapped += FragmentPlaneTapped;
        }

        private void FragmentPlaneTapped(TappedPlaneEventArgs obj) => PlaneTapped?.Invoke(obj);

        /// <summary>
        ///     Because it could be null when attached, we wait until this event.
        /// </summary>
        private void FragmentSessionInitialization(object sender, BaseArFragment.SessionInitializationEventArgs e)
        {
            Fragment.ArSceneView.Scene.Update += FrameReady;
            Fragment.SessionInitialization -= FragmentSessionInitialization;
        }

        private DrawableNode CreateRootDrawable(IEnumerable<Drawable> drawables)
        {
            TransformableNode node = new TransformableNode(Fragment.TransformationSystem);
            DrawableNode root = new DrawableNode(node);

            foreach (var drawable in drawables)
            {
                ((DrawableNode)drawable).Node.SetParent(node);
                root.AddChild((DrawableNode)drawable);
            }

            return root;
        }

        /// <summary>
        ///     Event handler when AR has Frame prepared.
        /// </summary>
        private void FrameReady(object sender, Scene.UpdateEventArgs e)
        {
            Camera p = Fragment.ArSceneView.Scene.Camera;
            CameraPosition = p.LocalPosition.ToNumerics();
            CameraDirection = p.Forward.ToNumerics();
            CameraUp = p.Up.ToNumerics();
            RefreshFrame?.Invoke();
        }
    }
}
