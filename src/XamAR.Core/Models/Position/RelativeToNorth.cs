using System.Numerics;
using XamAR.Core.Geometry;
using XamAR.Core.Models.Geolocation;
using XamAR.Core.Sensors;

namespace XamAR.Core.Models.Position
{
    /// <summary>
    ///     Position relative to GPS position, and heading is relative to North.
    ///     If GPS position is not set, current location is used.
    /// </summary>
    public class RelativeToNorth : PositionSourceBase
    {
        private float _distance;
        private float _headingDeg;
        private float _height;
        private Vector3 _relativeRealWorldVector;

        public RelativeToNorth(float headingDeg, float distance)
        {
            Distance2d = distance;
            HeadingDeg = headingDeg;
        }

        public new Vector3 GetPositionInARWorld(WorldConverter converter)
        {
            return converter.RealToARWorld(RealWorldPosition);
        }

        /// <summary>
        ///     Heading relative to North (clockwise, [0 - 2 * Pi) ), in radians.
        /// </summary>
        public float HeadingRad { get; private set; }

        /// <summary>
        ///     Heading relative to North (clockwise, [0-360) ), in degrees.
        /// </summary>
        public float HeadingDeg
        {
            get => _headingDeg;
            set
            {
                while (value < 0)
                {
                    value += 360;
                }

                _headingDeg = value % 360;
                HeadingRad = _headingDeg.DegToRad();
                Refresh();
            }
        }

        /// <summary>
        ///     Distance without height component.
        /// </summary>
        public float Distance2d
        {
            get => _distance;
            set
            {
                _distance = value;
                Refresh();
            }
        }

        /// <summary>
        ///     Height in real world.
        /// </summary>
        public float Height
        {
            get => _height;
            set
            {
                _height = value;
                Refresh();
            }
        }

        /// <summary>
        ///     If null, current location is used.
        ///     Otherwise, relative position is added to provided location.
        /// </summary>
        public Location Location { get; set; } = null;


        /*public Vector3 GetPositionInDeviceSystem()
        {
            throw new NotImplementedException();
        }*/

        protected override Vector3 GetRealWorld(WorldConverter converter)
        {
            Vector3 v = _relativeRealWorldVector;
            if (Location != null)
            {
                Vector3 t = GeolocationHelpers.GetVectorToTargetRelativeToNorth(
                    LocationMonitor.LastLocation, Location);
                v = t.Add(v);
            }

            return v;
        }

        private void Refresh()
        {
            Matrix4x4 rotation = Matrix4x4.CreateRotationZ(-HeadingRad);
            Vector3 north = new Vector3(0, 1, 0);
            _relativeRealWorldVector = Vector3.Transform(north, rotation);
            _relativeRealWorldVector = _relativeRealWorldVector * Distance2d;
            _relativeRealWorldVector = _relativeRealWorldVector.GetOnZ(Height);
        }
    }
}
