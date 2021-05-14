using System.Threading.Tasks;
using DryIoc;
using XamAR.Core.Exceptions;
using XamAR.Core.Models.Geolocation;

namespace XamAR.Core.Sensors
{
    public class LocationMonitor
    {
        private static bool s_running;

        private static IGeolocationService s_geolocation;

        public static Location LastLocation { get; set; } = new Location(44f, 22f);

        public static async void StartTracking()
        {
            s_geolocation ??= DI.Container.Resolve<IGeolocationService>();

            s_running = true;
            int counter = 100;

            while (s_running)
            {
                counter++;
                // Check every 1000ms.
                if (counter > 10)
                {
                    counter = 0;
                    // TODO Is this really good? 
                    // TODO If 1 per second is not too much, GetLocation can be used.
                    // TODO Handle exceptions.
                    Location l = await s_geolocation.GetLastKnownLocationAsync();
                    if (l == null)
                    {
                        try
                        {
                            l = await s_geolocation.GetLocationAsync(GeolocationAccuracy.Best);
                        }
                        catch
                        {
                            throw new FeatureNotEnabledException("Location not enabled.");
                        }
                    }

                    if (l != null)
                    {
                        LastLocation = l;
                    }
                }

                await Task.Delay(100);
            }
        }

        public static void StopTracking()
        {
            s_running = false;
        }
    }
}
