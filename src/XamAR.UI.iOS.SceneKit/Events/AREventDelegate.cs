using System;
using System.Collections.Generic;
using System.Linq;
using ARKit;
using CoreGraphics;
using SceneKit;
using UIKit;

namespace XamAR.UI.iOS.SceneKit.Events
{
    /// <summary>
    ///     Delegate to handle events from ARSession.
    /// </summary>
    public class AREventDelegate : ARSessionDelegate
    {
        public AREventDelegate(ARSCNView v)
        {
            View = v;

            UITapGestureRecognizer tapGesture = new UITapGestureRecognizer(HandleTap);
            List<UIGestureRecognizer> recognizers = new List<UIGestureRecognizer>
            {
                tapGesture
            };

            recognizers.AddRange(
                View.GestureRecognizers ??
                throw new InvalidOperationException($"{nameof(View.GestureRecognizers)} cannot be null."));

            View.GestureRecognizers = recognizers.ToArray();
        }

        private ARSCNView View { get; }

        /// <summary>
        ///     AR has prepared new Frame.
        /// </summary>
        public event Action<ARFrame> FrameUpdate;

        /// <summary>
        ///     Provided nodes have been tapped.
        /// </summary>
        public event Action<IEnumerable<SCNNode>> Tapped;

        public event Action<ARPlaneAnchor> TappedArPlane;

        private void HandleTap(UIGestureRecognizer gestureRecognize)
        {
            CGPoint p = gestureRecognize.LocationInView(View);
            SCNHitTestResult[] hitResults = View.HitTest(p, new SCNHitTestOptions());

            List<SCNNode> list = new List<SCNNode>();
            if (hitResults.Length > 0)
            {
                SCNHitTestResult result = hitResults[0];

                list.Add(result.Node);
            }

            if (list.Any())
            {
                Tapped?.Invoke(list);
            }
        }

        public override void DidUpdateFrame(ARSession session, ARFrame frame)
        {
            FrameUpdate?.Invoke(frame);
            frame.Dispose();
        }

        public override void DidAddAnchors(ARSession session, ARAnchor[] anchors)
        {
            foreach (ARAnchor a in anchors)
            {
                if (!(a is ARPlaneAnchor planeAnchor))
                {
                    continue;
                }

                TappedArPlane?.Invoke(planeAnchor);
            }
        }
    }
}
