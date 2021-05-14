using System.Numerics;
using SceneKit;
using UIKit;
using XamAR.Platform.iOS.SceneKit.Extensions;

namespace XamAR.Platform.iOS.SceneKit.Drawables
{
    public class DrawableScnNode : XamAR.Core.Models.Drawable
    {
        public DrawableScnNode(SCNNode node)
        {
            Node = node;
            ExtractInitialData();
        }

        public override object Object => Node;

        public SCNNode Node { get; }

        protected override void SetVisibleInternal(bool visible) => Node.Hidden = !visible;

        public override void RefreshDisplayedObject()
        {
            // Because no AR world is used for display, 
            // coordinate center is in middle of phone, and direction
            // is normal to phone.
            //

            Node.Position = Offset.Convert();
            Node.Orientation = UserRotation.ToAR(); //) new SCNVector4(q.X, q.Y, q.Z, q.W);
        }

        public override bool ContainsObject(object obj, bool includeChildren = false)
        {
            if (!(obj is SCNNode target))
            {
                return false;
            }

            return CheckSubtree(Node, target);
        }

        private void ExtractInitialData()
        {
            Offset = Node.Position.ToNumerics();
            Quaternion rot = Node.Orientation.ToQuaternion();
            UserRotation = Quaternion.Normalize(rot);
        }

        private static bool CheckSubtree(SCNNode parent, IUIFocusEnvironment target)
        {
            if (ReferenceEquals(parent, target))
            {
                return true;
            }

            foreach (SCNNode n in parent.ChildNodes)
            {
                if (ReferenceEquals(n, target))
                {
                    return true;
                }

                if (CheckSubtree(n, target))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
