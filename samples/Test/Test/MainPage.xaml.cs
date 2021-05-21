using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Test.ViewModels;
using XamAR.Core.Events;
using XamAR.Core.Models;
using XamAR.Core.Models.Direction;
using XamAR.Core.Models.Distance;
using XamAR.Core.Models.Geolocation;
using XamAR.Core.Models.Position;
using XamAR.Core.Sensors;
using XamAR.Diagnostics;
using Xamarin.Forms;

namespace Test
{
    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _model;

        public MainPage()
        {
            InitializeComponent();
            _model = new MainPageViewModel();
            BindingContext = _model;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            //return;
            var world = XamAR.World.Instance;
            var sphere = world.CreateModel("sphere");
            var text = world.CreateModel("text", "FixedLocation");

            Location current = LocationMonitor.LastLocation;
            IPositionSource pos = new FixedLocation(new Location(current.Latitude + 0.001f, current.Longitude));
            AnchoredObject obj;
            List<AnchoredObject> positionObjects = new List<AnchoredObject>();
            List<AnchoredObject> directionObjects = new List<AnchoredObject>();

            {
                text.Offset = new Vector3(0, 0.1f, 0);
                obj = world.AddModel(
                    pos,
                    //sphere,
                    text
                );
                obj.DistanceOverride = new FixedDistance(2);
                obj.Pressed += ObjPressed;
                positionObjects.Add(obj);

                pos = new RelativeToNorth(20, 2) {Height = 1};
                sphere = world.CreateModel("sphere");
                text = world.CreateModel("text", "RelativeToNorth");
                text.Offset = new Vector3(0, 0.1f, 0);
                obj = world.AddModel(
                    pos,
                    //sphere,
                    text
                );
                obj.DistanceOverride = new FixedDistance(2);
                positionObjects.Add(obj);

                pos = new RelativeToDeviceDirection(new Vector3(0, 0, -1));
                sphere = world.CreateModel("sphere");
                text = world.CreateModel("text", "RelativeToDeviceDirection");
                text.Offset = new Vector3(0, 0.1f, 0);
                obj = world.AddModel(
                    pos,
                    //sphere,
                    text
                );
                obj.DistanceOverride = new FixedDistance(2);
                positionObjects.Add(obj);

                pos = new RelativeToDeviceStabilized(new Vector3(0, 0, -1));
                sphere = world.CreateModel("sphere");
                text = world.CreateModel("text", "RelativeToDeviceStabilized");
                text.Offset = new Vector3(0, 0.1f, 0);
                obj = world.AddModel(
                    pos,
                    //sphere,
                    text
                );
                obj.DistanceOverride = new FixedDistance(2);
                positionObjects.Add(obj);
            }

            {
                IDirectionSource dir = new DirectionRelativeToDevice(0);
                pos = new RelativeToDeviceStabilized(new Vector3(0, -1, -0.5f));
                sphere = world.CreateModel("sphere");
                text = world.CreateModel("text", "DirectionRelativeToDevice");
                text.Offset = new Vector3(0, 0.1f, 0);
                obj = world.AddModel(
                    pos,
                    //sphere,
                    text
                );
                obj.DirectionSource = dir;
                directionObjects.Add(obj);

                dir = new DirectionToBearing(45f);
                pos = new RelativeToDeviceStabilized(new Vector3(0.5f, -1, -0.5f));
                sphere = world.CreateModel("sphere");
                text = world.CreateModel("text", "DirectionToBearing");
                text.Offset = new Vector3(0, 0.1f, 0);
                obj = world.AddModel(
                    pos,
                    //sphere,
                    text
                );
                obj.DirectionSource = dir;
                directionObjects.Add(obj);

                dir = new DirectionToDevice();
                pos = new RelativeToNorth(0, 1) {Height = -1};
                sphere = world.CreateModel("sphere");
                text = world.CreateModel("text", "DirectionToDevice");
                text.Offset = new Vector3(0, 0.1f, 0);
                obj = world.AddModel(
                    pos,
                    //sphere,
                    text
                );
                obj.DirectionSource = dir;
                AnchoredObject temp = obj;
                Task.Run(async () =>
                {
                    float angle = 0;
                    float totalSeconds = 10; // seconds for full circle.
                    int delay = 100; // ms
                    float step = 360f / totalSeconds / (1000.0f / delay);

                    while (true)
                    {
                        await Task.Delay(delay);
                        angle -= step;
                        if (angle < 0)
                        {
                            angle += 360;
                        }

                        ((RelativeToNorth)temp.PositionSource).HeadingDeg = angle;
                    }
                });

                directionObjects.Add(obj);

                dir = new DirectionToLocation(new Location(current.Latitude + 0.001f, current.Longitude - 0.001f));
                pos = new RelativeToNorth(-45, 1) {Height = -1};
                sphere = world.CreateModel("sphere");
                text = world.CreateModel("text", "DirectionToLocation");
                text.Offset = new Vector3(0, 0.1f, 0);
                obj = world.AddModel(
                    pos,
                    //sphere,
                    text
                );
                obj.DirectionSource = dir;
                directionObjects.Add(obj);
            }

            pos = new RelativeToDeviceStabilized(new Vector3(0, 0, -1f));
            var leftArrow = world.CreateModel("leftArrow");
            pos = new RelativeToDeviceStabilized(new Vector3(0, -0.5f, -1f));
            var rightArrow = world.CreateModel("rightArrow");

            obj = world.AddModel(
                pos,
                leftArrow,
                rightArrow);

            obj.DirectionSource = new DirectionToBearing(0);

            _model.PropertyChanged += (_, __) =>
            {
                if (__.PropertyName == nameof(MainPageViewModel.DrawPositions))
                {
                    foreach (AnchoredObject o in positionObjects)
                    {
                        o.Visible = _model.DrawPositions;
                    }
                }

                if (__.PropertyName == nameof(MainPageViewModel.DrawDirections))
                {
                    foreach (AnchoredObject o in directionObjects)
                    {
                        o.Visible = _model.DrawDirections;
                    }
                }
            };

            _model.DrawDirections = false;
            _model.DrawPositions = false;

            DiagnoseData();
        }

        private void ObjPressed(PressedEventsArgs obj)
        {
            obj.Object.Visible = false;
        }

        private async void DiagnoseData()
        {
            ARDiagnostics.Instance.Run();
            ARDiagnostics dg = ARDiagnostics.Instance;
            dg.DrawCoordinateSystem();
            StringBuilder builder = new StringBuilder();
            while (true)
            {
                await Task.Delay(300);
                builder.Clear();
                builder.AppendLine($"Camera Pos: {GetStringV(dg.CameraPosition)} ");
                builder.AppendLine($"Camera Dir: {GetStringV(dg.CameraDirection)} ");
                builder.AppendLine($"Total objects: {dg.NumberOfEntities} ");
                builder.AppendLine($"Location: {GetStringL(dg.Location)}");

                double north = OrientationMonitor.MagneticNorthDeg;
                builder.AppendLine($"North: {north:0.00} °");
                //builder.AppendLine(dg.World.ToArray().MatrixString());
                /*foreach (var t in TargetSource.Targets)
                {

                    float bearing = GeolocationHelpers.GetBearingDeg(dg.Location, t.Location);
                    double targetBearing = north + bearing;
                    if (targetBearing > 360)
                        targetBearing -= 360;
                    builder.AppendLine($"Bearing: {getStringD(bearing)} ({targetBearing:0.00})");
                }*/

                builder.AppendLine("Targets:");
                foreach (AnchoredObject v in ARDiagnostics.Instance.Objects)
                {
                    builder.AppendLine(GetStringV(v.PositionSource.RealWorldPosition) +
                                       $"D {GetStringD(v.DirectionRelativeToDevice, 1)} H {GetStringD(v.DirectionHeading, 1)}");
                }


                text.Text = builder.ToString();
            }

            string GetStringD(double d, int decimals = 2)
            {
                return d.ToString("N" + decimals);
            }

            string GetStringV(Vector3 v)
            {
                return $"X: {GetStringD(v.X)}, Y: {GetStringD(v.Y)}, Z: {GetStringD(v.Z)}";
            }

            string GetStringL(Location l)
            {
                return $"Lat: {GetStringD(l.Latitude, 6)}, {GetStringD(l.Longitude, 5)}";
            }
        }
    }
}
