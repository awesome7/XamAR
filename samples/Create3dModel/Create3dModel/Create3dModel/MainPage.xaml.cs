using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using XamAR.Core.Models.Distance;
using XamAR.Core.Models.Geolocation;
using XamAR.Core.Models.Position;
using Xamarin.Forms;

namespace Create3dModel
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();


            var world = XamAR.World.Instance;
            var sphere = world.CreateModel("sphere");
            string title = "Branko's bridge";
            var text = world.CreateModel("text", title);
            // Branko's bridge.
            var location = new Location(44.814846, 20.447514);
            IPositionSource positionSource = new FixedLocation(location);

            var sphereObject =  world.AddModel(positionSource, sphere);
            // Without this object would be very far away, thus not visible.
            sphereObject.DistanceOverride = new FixedDistance(2);

            var textObject = world.AddModel(positionSource, text);
            textObject.DistanceOverride = new FixedDistance(2);
        }
    }
}
