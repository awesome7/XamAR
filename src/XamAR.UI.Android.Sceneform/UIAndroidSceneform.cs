using DryIoc;
using XamAR.Core.Display;
using XamAR.UI.Android.Sceneform.Display;

namespace XamAR.UI.Android.Sceneform
{
    public static class UIAndroidSceneform
    {
        public static IContainer AddUIAndroidSceneform(this IContainer container)
        {
            container.Register<IDisplayService, DisplayAndroid>(Reuse.Singleton);

            return container;
        }
    }
}
