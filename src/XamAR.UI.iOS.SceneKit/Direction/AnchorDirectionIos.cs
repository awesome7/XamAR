using System.Numerics;
using ARKit;
using XamAR.Core.Models;
using XamAR.Core.Models.Direction;
using XamAR.Platform.iOS.SceneKit.Extensions;

namespace XamAR.UI.iOS.SceneKit.Direction
{
    internal class AnchorDirectionIos : AnchorDirection
    {
        public AnchorDirectionIos(ARAnchor anchor)
        {
            Anchor = anchor;
        }

        public ARAnchor Anchor { get; }

        protected override Vector3 GetDirectionInternal(WorldConverter converter, Vector3 realWorldPosition)
        {
            return converter.ArToRealWorld(Anchor.Transform.ToAR().GetZAxis());
        }
    }
}
