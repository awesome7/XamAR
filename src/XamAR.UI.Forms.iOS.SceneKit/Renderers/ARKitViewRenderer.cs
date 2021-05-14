using ARKit;
using DryIoc;
using XamAR.Core;
using XamAR.Core.Display;
using XamAR.UI.Forms.iOS.SceneKit.Renderers;
using XamAR.UI.Forms.Views;
using XamAR.UI.iOS.SceneKit.Display;
using XamAR.UI.iOS.SceneKit.Events;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ARView), typeof(ARKitViewRenderer))]

namespace XamAR.UI.Forms.iOS.SceneKit.Renderers
{
    public class ARKitViewRenderer : ViewRenderer<ARView, ARSCNView>
    {
        private ARSCNView _view;
        private AREventDelegate _viewDelegate;

        protected override void OnElementChanged(ElementChangedEventArgs<ARView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
            }

            if (e.NewElement != null)
            {
                if (Control == null)
                {
                    _view = new ARSCNView {DebugOptions = ARSCNDebugOptions.ShowWorldOrigin};
                    /*{
                        DebugOptions = ARSCNDebugOptions.ShowWorldOrigin
                    };*/
                    SetNativeControl(_view);

                    //ARLibraryiOS.InitializeNative(view);
                    _view.Session.Run(new ARWorldTrackingConfiguration
                    {
                        PlaneDetection = ARPlaneDetection.Horizontal | ARPlaneDetection.Vertical
                    });

                    _viewDelegate = new AREventDelegate(_view);
                    _view.Session.Delegate = _viewDelegate;

                    ((DisplayIos)DI.Container.Resolve<IDisplayService>()).Attach(_view, _viewDelegate);
                }
            }
        }
    }
}
