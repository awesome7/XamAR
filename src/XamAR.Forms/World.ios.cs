using DryIoc;
using XamAR.Core;
using XamAR.Core.Display;
using XamAR.Core.Factories;
using XamAR.Diagnostics;
using XamAR.Platform.Core;
using XamAR.Platform.iOS.SceneKit;
using XamAR.UI.Forms.iOS.SceneKit;
using XamAR.UI.iOS.SceneKit;

// ReSharper disable once CheckNamespace
namespace XamAR
{
    public sealed class WorldForms : World
    {
        public WorldForms(ObjectManagerService objectManagerService, IDisplayService displayService, IFactoryPOI factoryPOI)
            : base(objectManagerService, displayService, factoryPOI) { }

        public static void Init()
        {
            DI.Container
                .AddCore()
                .AddPlatformCore()
                .AddPlatformIOSSceneKit()
                .AddUIiOSSceneKit()
                .AddUIFormsiOSSceneKit();
                //.AddDiagnostics();

            DI.Container.Register<World, WorldForms>();
            DI.Container.Resolve<EntityUpdateService>().Run();
        }
    }
}
