using System.Threading.Tasks;
using XamAR.Platform.Core.Extensions;
using Xamarin.Essentials;

namespace XamAR.Platform.Core.Sensors
{
    public class GeolocationService : XamAR.Core.Models.Geolocation.IGeolocationService
    {
        public async Task<XamAR.Core.Models.Geolocation.Location> GetLastKnownLocationAsync()
        {
            Location location = await Geolocation.GetLastKnownLocationAsync();

            return location != null ?
                new XamAR.Core.Models.Geolocation.Location(location.Latitude, location.Longitude, location.Altitude) :
                null;
        }

        public async Task<XamAR.Core.Models.Geolocation.Location> GetLocationAsync()
        {
            Location location = await Geolocation.GetLocationAsync();

            return location != null ?
                new XamAR.Core.Models.Geolocation.Location(location.Latitude, location.Longitude, location.Altitude) :
                null;
        }

        public async Task<XamAR.Core.Models.Geolocation.Location> GetLocationAsync(XamAR.Core.Models.Geolocation.GeolocationAccuracy accuracy)
        {
            GeolocationRequest request = new GeolocationRequest((GeolocationAccuracy)(int)accuracy);
            Location location = await Geolocation.GetLocationAsync(request);

            return location != null ?
                new XamAR.Core.Models.Geolocation.Location(location.Latitude, location.Longitude, location.Altitude) :
                null;
        }

        public double CalculateDistance(XamAR.Core.Models.Geolocation.Location l1, XamAR.Core.Models.Geolocation.Location l2)
        {
            return l1.Convert().CalculateDistance(l2.Convert(), DistanceUnits.Kilometers) * 1000;
        }
    }
}
