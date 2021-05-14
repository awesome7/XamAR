namespace XamAR.Core.Models.Geolocation
{
    public enum GeolocationAccuracy
    {
        //
        // Summary:
        //     Represents default accuracy (Medium), typically within 30-500 meters.
        Default = 0,
        //
        // Summary:
        //     Represents the lowest accuracy, using the least power to obtain and typically
        //     within 1000-5000 meters.
        Lowest = 1,
        //
        // Summary:
        //     Represents low accuracy, typically within 300-3000 meters.
        Low = 2,
        //
        // Summary:
        //     Represents medium accuracy, typically within 30-500 meters.
        Medium = 3,
        //
        // Summary:
        //     Represents high accuracy, typically within 10-100 meters.
        High = 4,
        //
        // Summary:
        //     Represents the best accuracy, using the most power to obtain and typically within
        //     10 meters.
        Best = 5
    }
}
