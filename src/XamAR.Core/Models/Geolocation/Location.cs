namespace XamAR.Core.Models.Geolocation
{
    public class Location
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double? Altitude { get; set; }

        public Location(double latitude = 0, double longitude = 0, double? altitude = null)
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
        }
    }
}
