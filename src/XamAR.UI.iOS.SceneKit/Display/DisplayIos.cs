using System;
using System.Collections.Generic;
using System.Numerics;
using ARKit;
using SceneKit;
using UIKit;
using XamAR.Core.Display;
using XamAR.Core.Events;
using XamAR.Core.Geometry;
using XamAR.Core.Models;
using XamAR.Platform.iOS.SceneKit.Drawables;
using XamAR.Platform.iOS.SceneKit.Extensions;
using XamAR.UI.iOS.SceneKit.Events;

namespace XamAR.UI.iOS.SceneKit.Display
{
    public class DisplayIos : IDisplayService
    {
        public ARSCNView View { get; private set; }

        /// <summary>
        ///     Handles events from ARSCNView.
        /// </summary>
        public AREventDelegate ViewDelegate { get; private set; }

        public event Action RefreshFrame;
        public event Action<object> Pressed;
        public event Action<TappedPlaneEventArgs> PlaneTapped;

        public Vector3 CameraPosition { get; private set; }

        public Vector3 CameraDirection { get; private set; } = new Vector3(0, 0, -1);

        public Vector3 CameraUp { get; private set; } = Vector3.UnitY;

        public Drawable Add(params Drawable[] drawables)
        {
            SCNNode rootNode = new SCNNode();
            View.Scene.RootNode.AddChildNode(rootNode);
            DrawableScnNode dn = new DrawableScnNode(rootNode);

            foreach (Drawable d in drawables)
            {
                DrawableScnNode wrapper = (DrawableScnNode)d;
                rootNode.AddChildNode(wrapper.Node);
                dn.AddChild(d);
            }

            return dn;
        }

        public void Remove(params Drawable[] drawables)
        {
            foreach (Drawable d in drawables)
            {
                DrawableScnNode node = (DrawableScnNode)d;
                node.Node.RemoveFromParentNode();
            }
        }

        public void DrawCoordinateSystem()
        {
        }

        public void Attach(ARSCNView view, AREventDelegate del)
        {
            View = view;
            ViewDelegate = del;
            ViewDelegate.FrameUpdate += FrameReady;

            del.Tapped += DelegateTouched;
            del.TappedArPlane += DelegateTappedArPlane;
        }

        private void DelegateTappedArPlane(ARPlaneAnchor obj)
        {
            PlaneTapped?.Invoke(new TappedPlaneEventArgsIos(obj));
        }

        private void DelegateTouched(IEnumerable<SCNNode> obj)
        {
            foreach (SCNNode o in obj)
            {
                Pressed?.Invoke(o);
            }
        }

        private void FrameReady(ARFrame frame)
        {
            SCNMatrix4 m = frame.Camera.Transform.ToAR();
            // Camera matrix transform is different.

            CameraPosition = new Vector3(m.M14, m.M24, m.M34); // m.GetTranslation();
            Vector3 cameraZ = new Vector3(m.M13, m.M23, m.M33);

            CameraDirection = cameraZ.Negate(); // m.GetZAxis().Negate();
            // On iOS, AR keeps coordinate system rotation (even without tracking),
            // with sensors, so we can assume this is Up vector.
            // Otherwise, we would need to get up vector with orientation, and then
            // transform it to AR world.

            Vector3 x = Vector3.Cross(new Vector3(0, 1, 0), CameraDirection);
            CameraUp = Vector3.Cross(CameraDirection, x); // m.GetYAxis();

            AdjustCamera(m);

            RefreshFrame?.Invoke();
        }

        /// <summary>
        ///     Camera's up depends on
        /// </summary>
        private static SCNMatrix4 AdjustCamera(SCNMatrix4 matrix)
        {
            // X axis (documentation):
            //      the x-axis always points along the long axis of the device,
            //      from the front-facing camera toward the Home button
            // This means that in Portrait mode, X axis points down, and
            // Y axis to the right.
            switch (UIDevice.CurrentDevice.Orientation)
            {
                case UIDeviceOrientation.Portrait:
                    // Rotate x and y 90 deg 
                    break;
                case UIDeviceOrientation.PortraitUpsideDown:
                    break;
                case UIDeviceOrientation.LandscapeLeft:
                    break;
                case UIDeviceOrientation.LandscapeRight:
                    break;
            }

            return matrix;
        }
    }
}
