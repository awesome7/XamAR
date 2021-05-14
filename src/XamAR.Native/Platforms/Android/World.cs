using DryIoc;
using XamAR.Core;
using XamAR.Core.Display;
using XamAR.Core.Factories;
using XamAR.Diagnostics;
using XamAR.Platform.Android.Sceneform;
using XamAR.UI.Android.Sceneform;

// ReSharper disable once CheckNamespace
namespace XamAR
{
    public sealed class WorldNative : World
    {
        public WorldNative(ObjectManagerService objectManagerService, IDisplayService displayService, IFactoryPOI factoryPOI)
            : base(objectManagerService, displayService, factoryPOI) { }

        private static void Init()
        {
            DI.Container
                .AddCore()
                .AddPlatformAndroidSceneform()
                .AddUIAndroidSceneform()
                .AddDiagnostics();

            DI.Container.Register<World, WorldNative>();
        }
    }
}
