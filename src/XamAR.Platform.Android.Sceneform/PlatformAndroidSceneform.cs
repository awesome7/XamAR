using DryIoc;
using XamAR.Core.Factories;
using XamAR.Platform.Android.Sceneform.Factories;

namespace XamAR.Platform.Android.Sceneform
{
    public static class PlatformAndroidSceneform
    {
        public static IContainer AddPlatformAndroidSceneform(this IContainer container)
        {
            container.Register<IFactoryPOI, FactoryPOI>(Reuse.Singleton);
            container.Register<IFactoryWrapper, FactoryWrapper>(Reuse.Singleton);
            container.Register<IFactoryBillboard, FactoryBillboard>(Reuse.Singleton);

            return container;
        }
    }
}
