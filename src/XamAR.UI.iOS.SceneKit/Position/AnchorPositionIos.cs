using System.Numerics;
using ARKit;
using XamAR.Core.Models;
using XamAR.Core.Models.Position;
using XamAR.Platform.iOS.SceneKit.Extensions;

namespace XamAR.UI.iOS.SceneKit.Position
{
    internal class AnchorPositionIos : AnchorPosition
    {
        public AnchorPositionIos(ARAnchor anchor)
        {
            Anchor = anchor;
        }

        public ARAnchor Anchor { get; }

        public override Vector3 GetPositionInARWorld(WorldConverter converter)
        {
            return Anchor.Transform.ToAR().GetTranslation();
        }

        protected override Vector3 GetRealWorld(WorldConverter converter)
        {
            return converter.ArToRealWorld(Anchor.Transform.ToAR().GetTranslation()); ;
        }
    }
}
