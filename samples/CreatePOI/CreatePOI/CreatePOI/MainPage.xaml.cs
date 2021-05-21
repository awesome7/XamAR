using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamAR.Core.Models.Direction;
using XamAR.Core.Models.Distance;
using XamAR.Core.Models.Geolocation;
using Xamarin.Forms;

namespace CreatePOI
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

            // Branko's bridge, Belgrade, Serbia.
            var location = new Location(44.814846, 20.447514);
            string title = "Branko's bridge";
            // Keep reference to created object, to be
            // able to manipulate it later on.
            var obj = XamAR.World.Instance.AddPointOfInterest(location, title);

            {
                // [Optional]
                // Keep object on fixed distance from us at all times.
                IDistanceOverride distanceOverride = new FixedDistance(2);
                // Don't change to keep real distance.
                obj.DistanceOverride = distanceOverride;
            }

            {
                // [Optional]
                IDirectionSource direction = new DirectionToDevice();
                // By default, object is oriented towards -Z axis of 
                // augmented world. 
                // This override keeps text always oriented toward device.
                obj.DirectionSource = direction;
            }
        }
    }
}
