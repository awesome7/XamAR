using DryIoc;
using XamAR.Core.Models.Geolocation;
using XamAR.Core.Models.Orientation;
using XamAR.Platform.Core.Sensors;

namespace XamAR.Platform.Core
{
    public static class PlatformCore
    {
        public static IContainer AddPlatformCore(this IContainer container)
        {
            container.Register<IGeolocationService, GeolocationService>(Reuse.Singleton);
            container.Register<IOrientationService, OrientationService>(Reuse.Singleton);
            
            return container;
        }
    }
}
