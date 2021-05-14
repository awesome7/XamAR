using System;
using System.Numerics;
using Xamarin.Essentials;

namespace XamAR.Platform.Core.Sensors
{
    public class OrientationService : XamAR.Core.Models.Orientation.IOrientationService
    {
        private bool _running;
        public event Action<XamAR.Core.Models.Orientation.OrientationChanged> ReadingChanged;

        public void StartListening()
        {
            try
            {
                if (_running)
                {
                    return;
                }

                _running = true;

                if (!OrientationSensor.IsMonitoring)
                {
                    OrientationSensor.Start(SensorSpeed.UI
                        //, applyLowPassFilter:true
                    );
                }

                OrientationSensor.ReadingChanged += OrientationSensorReadingChanged;
            }
            catch (Exception)
            {
                StopListening();
            }
        }

        public void StopListening()
        {
            OrientationSensor.ReadingChanged -= OrientationSensorReadingChanged;

            _running = false;
        }

        private void OrientationSensorReadingChanged(object sender, OrientationSensorChangedEventArgs e)
        {
            XamAR.Core.Models.Orientation.OrientationChanged args =
                new XamAR.Core.Models.Orientation.OrientationChanged
                {
                    Orientation = new Quaternion(
                        e.Reading.Orientation.X,
                        e.Reading.Orientation.Y,
                        e.Reading.Orientation.Z,
                        e.Reading.Orientation.W)
                };

            ReadingChanged?.Invoke(args);
        }
    }
}
