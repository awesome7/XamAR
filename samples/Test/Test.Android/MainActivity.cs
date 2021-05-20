using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Test.Droid.Factories;
using XamAR.Factory;

namespace Test.Droid
{
    [Activity(Label = "Test", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            XamAR.WorldForms.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            FactoryService.RegisterFactory<Sphere>("sphere");
            FactoryService.RegisterFactory<Text>("text");
            FactoryService.RegisterFactory<LeftArrow>("leftArrow");
            FactoryService.RegisterFactory<RightArrow>("rightArrow");

            LoadApplication(new App());
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
