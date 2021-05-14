using DryIoc;
using XamAR.Core.Display;
using XamAR.UI.iOS.SceneKit.Display;

namespace XamAR.UI.iOS.SceneKit
{
    public static class UIiOSSceneKit
    {
        public static IContainer AddUIiOSSceneKit(this IContainer container)
        {
            container.Register<IDisplayService, DisplayIos>(Reuse.Singleton);

            return container;
        }
    }
}
