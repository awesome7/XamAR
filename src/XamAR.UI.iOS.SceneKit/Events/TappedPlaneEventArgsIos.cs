using ARKit;
using XamAR.Core.Events;
using XamAR.Core.Models.Direction;
using XamAR.Core.Models.Position;
using XamAR.UI.iOS.SceneKit.Direction;
using XamAR.UI.iOS.SceneKit.Position;

namespace XamAR.UI.iOS.SceneKit.Events
{
    internal class TappedPlaneEventArgsIos : TappedPlaneEventArgs
    {
        public ARPlaneAnchor Anchor { get; }

        public TappedPlaneEventArgsIos(ARPlaneAnchor anchor)
        {
            Anchor = anchor;
        }

        public override IDirectionSource GetDirection()
        {
            return new AnchorDirectionIos(Anchor);
        }

        public override IPositionSource GetPosition()
        {
            return new AnchorPositionIos(Anchor);
        }
    }
}
