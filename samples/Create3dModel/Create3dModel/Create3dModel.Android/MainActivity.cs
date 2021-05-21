using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using XamAR.Factory;
using Create3dModel.Droid.Factories;

namespace Create3dModel.Droid
{
    [Activity(Label = "Create3dModel", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            XamAR.WorldForms.Init(this, savedInstanceState);

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);


            FactoryService.RegisterFactory<Sphere>("sphere");
            FactoryService.RegisterFactory<Text>("text");

            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}