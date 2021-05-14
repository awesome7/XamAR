using System;
using System.Numerics;
using XamAR.Core.Geometry;
using XamAR.Core.Models.Geolocation;
using XamAR.Core.Sensors;

namespace XamAR.Core.Models.Direction
{
    /// <summary>
    /// This location is directed to target location.
    /// </summary>
    public class DirectionToLocation : DirectionSourceBase
    {
        /// <summary>
        /// Target location, to point to.
        /// </summary>
        public Location Location { get; }

        public DirectionToLocation(Location location)
        {
            Location = location ?? throw new ArgumentNullException(nameof(location));
        }

        protected override DirectionParameters GetDirection(WorldConverter converter, Vector3 realWorldPosition)
        {
            Location current = LocationMonitor.LastLocation;
            Vector3 deviceToLocation = GeolocationHelpers.GetVectorToTargetRelativeToNorth(current, Location);
            Vector3 direction = realWorldPosition.Negate().Add(deviceToLocation);

            return new DirectionParameters(direction.GetUnit());
        }

        //TODO DirectionToLocation should be related to current object (its GPS position).,
        //TODO not GPS position of current location (as it is now).
        //TODO Current location is needed to be calculated from existing object (which requires 
        //TODO different calculation strategy).
    }
}
