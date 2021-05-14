using System.Numerics;
using Google.AR.Core;
using XamAR.Core.Models;
using XamAR.Core.Models.Direction;
using XamAR.Platform.Android.Sceneform.Extensions;

namespace XamAR.UI.Android.Sceneform.Direction
{
    public class AnchorDirectionAndroid : AnchorDirection
    {
        public AnchorDirectionAndroid(Anchor anchor)
        {
            Anchor = anchor;
        }

        public Anchor Anchor { get; }

        protected override Vector3 GetDirectionInternal(WorldConverter converter, Vector3 realWorldPosition)
        {
            return converter.ArToRealWorld(Anchor.Pose.GetZAxis().ToVector());
        }
    }
}
