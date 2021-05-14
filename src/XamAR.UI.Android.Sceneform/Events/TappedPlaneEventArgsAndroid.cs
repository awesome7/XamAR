using Google.AR.Core;
using XamAR.Core.Events;
using XamAR.Core.Models.Direction;
using XamAR.Core.Models.Position;
using XamAR.UI.Android.Sceneform.Direction;
using XamAR.UI.Android.Sceneform.Position;
using static Google.AR.Sceneform.UX.BaseArFragment;

namespace XamAR.UI.Android.Sceneform.Events
{
    internal class TappedPlaneEventArgsAndroid : TappedPlaneEventArgs
    {
        private readonly Anchor _anchor;

        public TapArPlaneEventArgs Data { get; }

        public TappedPlaneEventArgsAndroid(TapArPlaneEventArgs data)
        {
            Data = data;
            _anchor = data.HitResult.CreateAnchor();
        }

        public override IPositionSource GetPosition()
        {
            return new AnchorPositionAndroid(_anchor);
        }

        public override IDirectionSource GetDirection()
        {
            return new AnchorDirectionAndroid(_anchor);
        }
    }
}
