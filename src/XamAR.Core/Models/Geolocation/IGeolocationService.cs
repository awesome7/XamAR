using System.Threading.Tasks;

namespace XamAR.Core.Models.Geolocation
{
    public interface IGeolocationService
    {
        /// <summary>
        /// Returns the last known location of the device.
        /// </summary>
        Task<Location> GetLastKnownLocationAsync();

        /// <summary>
        /// Returns the current location of the device.
        /// </summary>
        Task<Location> GetLocationAsync();

        /// <summary>
        /// Returns the current location of the device using the specified criteria.
        /// </summary>
        Task<Location> GetLocationAsync(GeolocationAccuracy accuracy);

        /// <summary>
        /// Calculates distance between two locations.
        /// <para>Result is in meters.</para>
        /// </summary>
        double CalculateDistance(Location l1, Location l2);
    }
}
