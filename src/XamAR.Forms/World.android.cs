using Android.App;
using Android.OS;
using DryIoc;
using XamAR.Core;
using XamAR.Core.Display;
using XamAR.Core.Factories;
using XamAR.Diagnostics;
using XamAR.Platform.Android.Sceneform;
using XamAR.Platform.Core;
using XamAR.UI.Android.Sceneform;

// ReSharper disable once CheckNamespace
namespace XamAR
{
    public sealed class WorldForms : World
    {
        public WorldForms(ObjectManagerService objectManagerService, IDisplayService displayService, IFactoryPOI factoryPOI)
            : base(objectManagerService, displayService, factoryPOI) {}

        public static void Init(Activity activity, Bundle bundle)
        {
            DI.Container
                .AddCore()
                .AddPlatformCore()
                .AddPlatformAndroidSceneform()
                .AddUIAndroidSceneform()
                .AddDiagnostics();

            Xamarin.Essentials.Platform.Init(activity, bundle);

            DI.Container.Register<World, WorldForms>();
            DI.Container.Resolve<EntityUpdateService>().Run();
        }
    }
}
