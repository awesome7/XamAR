using System.Numerics;
using Google.AR.Sceneform;
using Java.Interop;
using XamAR.Core.Exceptions;
using XamAR.Platform.Android.Sceneform.Extensions;

namespace XamAR.Platform.Android.Sceneform.Drawables
{
    public class DrawableNode : XamAR.Core.Models.Drawable
    {
        private NodeParent _parent;

        public DrawableNode(Node node)
        {
            Node = node;
            ExtractInitialData();
        }

        public override object Object
        {
            get { return Node; }
        }

        public Node Node { get; }

        protected override void SetVisibleInternal(bool visible)
        {
            if (visible)
            {
                if (_parent == null)
                {
                    throw new ModelVisibilityException("Model should already be visible!");
                }

                Node.SetParent(_parent);
                _parent = null;
            }
            else
            {
                _parent = Node.Parent ?? (NodeParent) Node.Scene;

                Node.SetParent(null);
            }
        }

        public override void RefreshDisplayedObject()
        {
            // Update geometry in Node (position, rotation, transformation...).
            Node.LocalPosition = Offset.ToAR();
            Node.LocalRotation = UserRotation.ToAR();
            //float angle = Vector3.AngleBetweenVectors(DefaultDirection.ToAR(), Direction.ToAR());

            //Node.LocalRotation = Quaternion.AxisAngle(Vector3.Up(), angle); // Quaternion.RotationBetweenVectors(DefaultDirection.ToAR(), Direction.ToAR());
        }

        public override bool ContainsObject(object obj, bool includeChildren = false)
        {
            if (!(obj is Node target))
            {
                return false;
            }

            return CheckSubtree(Node, target);
        }

        private void ExtractInitialData()
        {
            Offset = Node.LocalPosition.ToNumerics();
            Quaternion rot = Node.LocalRotation.ToNumerics();
            UserRotation = Quaternion.Normalize(rot);
        }

        private static bool CheckSubtree(NodeParent parent, IJavaPeerable target)
        {
            if (ReferenceEquals(parent, target))
            {
                return true;
            }

            foreach (Node n in parent.Children)
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
