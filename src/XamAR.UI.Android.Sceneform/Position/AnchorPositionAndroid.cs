using System.Numerics;
using Google.AR.Core;
using XamAR.Core.Models;
using XamAR.Core.Models.Position;
using XamAR.Platform.Android.Sceneform.Extensions;

namespace XamAR.UI.Android.Sceneform.Position
{
    /// <summary>
    ///     Need as separate class, since position can be changed.
    /// </summary>
    internal class AnchorPositionAndroid : AnchorPosition
    {
        public AnchorPositionAndroid(Anchor anchor)
        {
            Anchor = anchor;
        }

        public Anchor Anchor { get; }

        private Vector3 GetPosition()
        {
            return Anchor.Pose.GetTranslation().ToVector();
        }

        protected override Vector3 GetRealWorld(WorldConverter converter)
        {
            return converter.ArToRealWorld(GetPosition());
        }

        public override Vector3 GetPositionInARWorld(WorldConverter converter)
        {
            return GetPosition();
        }
    }
}
