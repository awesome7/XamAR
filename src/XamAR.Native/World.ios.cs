﻿using DryIoc;
using XamAR.Core;
using XamAR.Core.Display;
using XamAR.Core.Factories;
using XamAR.Diagnostics;
using XamAR.Platform.Core;
using XamAR.Platform.iOS.SceneKit;
using XamAR.UI.iOS.SceneKit;

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
                .AddPlatformCore()
                .AddPlatformIOSSceneKit()
                .AddUIiOSSceneKit();
                //.AddDiagnostics();

            DI.Container.Register<World, WorldNative>();
            DI.Container.Resolve<EntityUpdateService>().Run();
        }
    }
}
