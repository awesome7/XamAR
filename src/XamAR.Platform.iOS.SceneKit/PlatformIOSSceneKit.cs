using DryIoc;
using XamAR.Core.Factories;
using XamAR.Platform.iOS.SceneKit.Factories;

namespace XamAR.Platform.iOS.SceneKit
{
    public static class PlatformIOSSceneKit
    {
        public static IContainer AddPlatformIOSSceneKit(this IContainer container)
        {
            container.Register<IFactoryPOI, FactoryPOI>(Reuse.Singleton);
            container.Register<IFactoryWrapper, FactoryWrapper>(Reuse.Singleton);
            container.Register<IFactoryBillboard, FactoryBillboard>(Reuse.Singleton);

            return container;
        }
    }
}
