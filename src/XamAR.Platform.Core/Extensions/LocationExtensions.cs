using Xamarin.Essentials;

namespace XamAR.Platform.Core.Extensions
{
    internal static class LocationExtensions
    {
        public static Location Convert(this XamAR.Core.Models.Geolocation.Location l)
        {
            return new Location(l.Latitude, l.Longitude, l.Altitude ?? 0);
        }
    }
}
