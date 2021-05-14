using System;
using System.Numerics;
using DryIoc;
using XamAR.Core.Models;
using XamAR.Core.Models.Geolocation;
using XamAR.Core.Sensors;

namespace XamAR.Core.Geometry
{
    public static class GeolocationHelpers
    {
        //NOTE ARCore should have it's own calculations. Check if that is better.

        private static IGeolocationService _service;

        private static IGeolocationService Geolocation()
        {
            return _service ??= DI.Container.Resolve<IGeolocationService>();
        }

        /// <summary>
        ///     Calculation of bearing algorithm from internet.
        ///     <para>
        ///         Bearing is angle between l1 and North, and l1 and l2
        ///         (in clockwise direction)
        ///     </para>
        /// </summary>
        /// <seealso cref="https://www.igismap.com/formula-to-find-bearing-or-heading-angle-between-two-points-latitude-longitude/" />
        /// <seealso cref="https://towardsdatascience.com/calculating-the-bearing-between-two-geospatial-coordinates-66203f57e4b4" />
        /// <see cref="https://www.movable-type.co.uk/scripts/latlong.html" />
        public static float GetBearingDeg(this Location l1, Location l2)
        {
            double lat1 = l1.Latitude;
            double lon1 = l1.Longitude;
            double lat2 = l2.Latitude;
            double lon2 = l2.Longitude;

            double ToRad(double d)
            {
                return d * Math.PI / 180;
            }

            lat1 = ToRad(lat1);
            lon1 = ToRad(lon1);
            lat2 = ToRad(lat2);
            lon2 = ToRad(lon2);

            double x = Math.Cos(lat2) * Math.Sin(lon2 - lon1);
            double y = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(lon2 - lon1);

            double beta = Math.Atan2(x, y);

            beta = beta * 180 / Math.PI; // Rad to Deg.
            return (float)beta;
        }

        /// <summary>
        ///     Returns distance between locations, in meters.
        /// </summary>
        public static float GetDistance(this Location l1, Location l2)
        {
            double d = Geolocation().CalculateDistance(l1, l2);
            return (float)(d * 1000);
        }

        /// <summary>
        ///     Returns vector of target's position relative to source's position pointed
        ///     to north, in coordinate system of the World (y is north, x is east, z is height) and NOT
        ///     the phone (phone's orientation is irrelevant).
        ///     <para>
        ///         (consider phone is pointed to North (0,1,0),
        ///         and calculate target relative location, by bearing).
        ///     </para>
        /// </summary>
        public static Vector3 GetVectorToTargetRelativeToNorth(Location source, Location target)
        {
            Vector3 direction = new Vector3(0, 1, 0);

            float bearing = GetBearingDeg(source, target);
            float distance = GetDistance(source, target);
            float deltaAngleDeg = bearing;

            Matrix4x4 matrix = Matrix4x4.CreateRotationZ(-deltaAngleDeg.DegToRad());

            return Vector3.Transform(direction, matrix) * distance;
        }

        /// <summary>
        ///     Same as GetVectorToTargetRelativeToNorth, except returned vector is in
        ///     coordinate system of phone (includes phone orientation).
        /// </summary>
        public static Vector3 GetVectorToTargetRelativeToPhone(Location source, Location target)
        {
            Vector3 position = GetVectorToTargetRelativeToNorth(source, target);
            position = OrientationMonitor.World.ConvertToUcs(position);
            return position;
        }

        /// <summary>
        ///     Returns vector of target position relative to source's position and
        ///     forward orientation (-z axis of phone). Vector is in phone's world (right handed, y is up).
        /// </summary>
        /// <remarks>
        ///     In simpler words: Take vector (0,0,-1) (forward, -z axis of phone) and rotate it until it's direction
        ///     matches target location.
        /// </remarks>
        [Obsolete("Check other methods before using it, left just for reference")]
        public static Vector3 GetVectorToTargetInDeviceCoordinateSystem(Location source, Location target)
        {
            throw new NotImplementedException("Don't use this method anymore!");
#pragma warning disable CS0162 // Unreachable code detected
            float north = OrientationMonitor.MagneticNorthDeg;
#pragma warning restore CS0162 // Unreachable code detected
            Vector3 direction = new Vector3(0, 0, -1);

            float bearing = GetBearingDeg(source, target);
            float distance = GetDistance(source, target);

            float deltaAngleDeg = north - bearing;
            while (deltaAngleDeg > 360)
            {
                deltaAngleDeg -= 360;
            }

            while (deltaAngleDeg < 0)
            {
                deltaAngleDeg += 360;
            }

            Matrix4x4 matrix = Matrix4x4.CreateRotationY(-deltaAngleDeg.DegToRad());
            Vector3 v = Vector3.Transform(direction, matrix);
            v = v * distance;
            Vector3 position = v;

            return position;
        }


        //TODO
        // Need some platform dependent solution (get platform depended declination, true north direction… and apply directly to orientation service, or handle it on different place? needs little bit of exploring)
        // https://developer.android.com/reference/android/hardware/GeomagneticField
        // https://developer.apple.com/documentation/corelocation/clheading/1423568-trueheading
        // https://developer.apple.com/documentation/corelocation/clheading/1423763-magneticheading


        /// <summary>
        /// Bearing delta which should be added to Magneting north pole to get True (geographic) north pole,
        /// at specific location.
        /// </summary>
        /// <remarks>
        /// This is also called "magnetic declination angle".
        /// Magnetic north (from compass, magnetic center) and True north (from GPS, Earth's rotation 
        /// center) are different. This causes declination angle to be different on different places
        /// on Earth. 
        /// <see cref="https://en.wikipedia.org/wiki/Magnetic_declination"/>
        /// <see cref="https://www.ngdc.noaa.gov/geomag/declination.shtml"/>
        /// <see cref="https://www.ngdc.noaa.gov/geomag/WMM/"/>
        /// </remarks>
        //public float TrueNorthCorrection(Location l)
        //{
        //    var gf = new Android.Hardware.GeomagneticField((float)l.Latitude, (float)l.Longitude, (float)(l.Altitude ?? 50), DateTime.Now.Millisecond);
        //    return gf.Declination;
        //}

        /// <summary>
        ///     Creates vector from heading relative to North.
        ///     North is (0,1), East is (1, 0).
        ///     <para>Heading is clockwise, 0 degree is North.</para>
        /// </summary>
        /// <param name="headingRad"></param>
        /// <returns></returns>
        /// <example>
        ///     For heading = 90, vector is (1, 0).
        /// </example>
        public static Vector2 CreateFromHeading(float headingRad)
        {
            // Replacing x and y flips coordinate system around y=x line.
            float x = (float)Math.Sin(headingRad);
            float y = (float)Math.Cos(headingRad);
            Vector2 v = new Vector2(x, y);
            return v;
        }
    }
}
