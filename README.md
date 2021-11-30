![XamAR Logo](./github.png)

# XamAR
[![Build Status](https://dev.azure.com/awesome7/Public/_apis/build/status/awesome7.XamAR?branchName=main)](https://dev.azure.com/awesome7/Public/_build/latest?definitionId=2&branchName=main) [![NuGet Version](https://img.shields.io/nuget/vpre/XamAR.Forms)](https://img.shields.io/nuget/vpre/XamAR.Forms)

XamAR - cross-platform Augmented Reality (AR) SDK for Xamarin. It works with Xamarin.Forms, abstracting native AR frameworks (ARCore-Android and ARKit-iOS) under single GUI project, while all platform-specific operations are implemented in Xamarin native projects (Xamarin.Android and Xamarin.iOS). 

XamAR can also can make connection between real and AR worlds, by assigning GPS coordinates to objects in AR world.
```cs
// Location of The Victor monument, Belgrade, Serbia.
var location = new Location(44.823052, 20.447704);
string title = "The Victor";
var poiObject = XamAR.World.Instance.AddPointOfInterest(location, title);
```


## XamAR Goal
Main goal of XamAR SDK is to bring AR (augmented reality) world closer to Xamarin developers, including those who are not familiar with AR and math behind it, to enjoy benefits and new opportunities that AR brings to world of mobile devices. 
XamAR can be used in Xamarin.Forms app (with plans on adding Xamarin.Android and Xamarin.iOS in the future)


> Example for newcomers to AR:
If you are found in an empty room, using AR you can put a table (as 3d model) in the center of room, and observe it through the device's camera. You can move device around, walk around the room - table will stay in the same place all the time, and appear as if it really in the room.

To learn more about AR on each platform, visit:
[Android - ARCore - Official Overview](https://developers.google.com/ar/develop)
[iOS - ARKit - Official Overview](https://developer.apple.com/augmented-reality/arkit/)

### Benefits
Benefit that XamAR brings, among others, is to use real-world GPS position to add 3d object to AR world (for example, mark position of a building) and allow any user of your application to see that object on same real-world position. More on topic later in this document.

### Features
- Position object in AR world using real GPS coordinate
- Position object in AR world relative to device position
- Position object in AR world relative to another position
- Set orientation of object in AR world relative to North
- Set orientation of object in AR world relative to device's orientation
- Set orientation of object in AR world toward another object
- Create custom 3d object using desired platform libraries, or use model created with modelling program
- Move object by updating it's position at any time
- Override distance of object, setting it to fixed or variable value
- On Android XamAR is using ARCore with [Sceneform](https://developers.google.com/sceneform/develop)  (framework for 3d rendering)
*currently is archived and not being developed anymore, but last version has many things to offer and can be used for many use cases
- On iOS XamAR is using ARKit with [SceneKit](https://developer.apple.com/documentation/scenekit/) (framework for 3d rendering)

#### Notes
- GPS coordinate system in use is *WGS84* (this means that coordinates can be copied from Bing maps of Google maps directly)
- Since GPS functionality relies on GPS reading provided by the device, and is limited by precision of GPS receiver which can have margin of error from few meters, all way to tens of meters, current API is considered not to be reliable in scenarios where objects are close to the device (for example, closer than 20 meters, in general case). 
- Functions which don't rely on GPS can be used for all scenarios .
- Elevation of a GPS coordinate is calculated relative to initial position of the device (which is 0). This means positioning object on elevation of 50m, will put it, approximately, 50m from the current ground level. This is due to unreliable real elevation returned by GPS receiver.
- API supports various use cases, which can be combined. Setting different position source, direction source, distance source, allows creating more complex scenarios
- For easier start, developers can use predefined 3d model, and later replace it with custom one - either manually created or loaded from outside source.

### Getting started
Followed are basic steps needed to prepare project:
- Create Xamarin Forms project (currently versions 5.0.0.2012)
- Add nuget *XamAR.Forms* to all projects that will be used
- Open* MainPage.xaml*
- Add namespace `xmlns:views="clr-namespace:XamAR.UI.Forms.Views;assembly=XamAR.UI.Forms"` as attribute to ContentPage
- Add `<views:ARView/>` where you want AR control to appear (note: camera feed will populated complete control)
- As the final task, platform that will be used (Android and/or iOS) needs to be initialized. Steps for each platform can be found below.

### Android
Open *MainActivity* and in overriden *OnCreate* method insert:
```csharp
base.OnCreate(savedInstanceState);
 
// Add this line here
XamAR.WorldForms.Init(this, savedInstanceState);

global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
```
- In project properties set minimum version 28 and target version 30
- Next, open *AndroidManifest.xml* and set permissions:
    
    	<uses-permission android:name="android.permission.ACCESS_NETWORK_STATE" />
    	<uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />
    	<uses-permission android:name="android.permission.CAMERA" />
    	<uses-feature android:name="android.hardware.camera.ar" />
- Also in *AndroidManifest.xml*  find application tag and insert as subelement:
`<meta-data android:name="com.google.ar.core" android:value="required" />`
- Connect [supported ARCore Android device](https://developers.google.com/ar/devices) and select it as Debug target

### iOS
Open *AppDelegate* and in overriden *FinishedLaunhcing*  method insert:
```csharp
// Add this line here
XamAR.WorldForms.Init();

global::Xamarin.Forms.Forms.Init();
LoadApplication(new App());
```
- In project properties Bundle Signing need to be set - check (instructions)[https://docs.microsoft.com/en-us/xamarin/ios/get-started/installation/]

### Android - prepare external 3d model
Sceneform requires model in **.sfb** format, which needs to be created using Android Studio. Instructions can be found on [official documentation](https://developers.google.com/sceneform/develop/import-assets) .
Next, add created **.sfb** model to Assets directory in the Android project.
- Make sure to use Android Studio version 3.5 or 3.4, and to have [Sceneform plugin](https://developers.google.com/sceneform/develop/getting-started#import-sceneform-plugin) installed

### iOS - prepare external 3d model
iOS can use several different formats, without conversion ([SO post](https://stackoverflow.com/a/55879013)) . Directory is needed in iOS project with name *art.scnassets* , which holds all models.
For example **.obj**: copy *.obj, *.mtl with all texture files inside *art.scnassets* - and that's it!


### XamAR API
Main concept:
- Position - defines position in AR world; this can be position with fixed GPS coordinates, position relative to current device's position (object is always in front of device, even while moving), position relative to another position
- Direction - defines direction at which model will be oriented to (forward of the model); this can be North (or some angle bearing to the North), another position, towards device, relative to device's current orientation, etc...
These simple concepts can be combined for creating more complex scenarios.

Simple scenario example:
- Use GPS position to set 3d model - this can be used to mark some significant landmark, and everybody with same application would be able to see it in AR, on the same real-world position.
- In addtion, guide arrow can be set in front of the device, with direction to that GPS position, so it is always oriented towards it and can be used as guide to show where that GPS position is. It can guide user to get to the destination marked with the GPS position.

#### Load model to XamAR
TODO (create model using 3d framework and assign to POI, load predefined model)

### Example 1
Add POI (point of interest) - show sphere with text above it, on GPS defined position
Thanks to *World* class, which already has some predefined functionality, POI can be defined in just a few lines. 
*MainPage.xaml.cs*
```cs
public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        
        // Location of The Victor monument, Belgrade, Serbia.
        var location = new Location(44.823052, 20.447704);
        string title = "The Victor";
        var poiObject = XamAR.World.Instance.AddPointOfInterest(location, title);
    }
}
```

If POI is too far away from current location, distance can be overriden. 
Adding following lines of code (just below *poiObject* assignment) will show object at 2 meters from the phone, but in direction of POI object.
No matter how big is real distance between POI and the device, it will be fixed to 2 meters.
```cs
IDistanceOverride distanceOverride = new FixedDistance(2);
poiObject.DistanceOverride = distanceOverride;
```

### Example 2
This is similar example like previous one, except displayed object can be user created. Just sphere will be displayed.
Since this is framework-related operation, model needs to be defined in desired platform first, and then used in Xamarin.Forms project.
Model is defined in *Factory* class, and later registered and referenced by name.

**Android**
Create factory in Android project
*Sphere.cs*
```cs
public class SphereFactory : ModelFactory
{
    public override ARModel CreateModel()
    {
        var nodeSphere = new Node()
        {
            LocalPosition = new Vector3().ToAR()
        };

        ModelRenderable model;
        MaterialFactory
            .MakeOpaqueWithColor(Android.App.Application.Context,
                new Google.AR.Sceneform.Rendering.Color(100, 150, 40))
            .ThenAccept(
                new DelegateConsumer<Material>((m) =>
                {
                    model = ShapeFactory.MakeSphere(0.1f, new Vector3(0, 0, 0).ToAR(), m);
                    nodeSphere.Renderable = model;
                })
            );
        return nodeSphere.AsARModel();
    }
}
```
Then, register Factory class in Android - *MainActivity.cs*
``` cs
protected override void OnCreate(Bundle savedInstanceState)
{
    base.OnCreate(savedInstanceState);

    XamAR.WorldForms.Init(this, savedInstanceState);
    global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

    // Add this
    FactoryService.RegisterFactory<SphereFactory>("sphere");

    LoadApplication(new App());
}
```

And now go back to **Xamarin.Forms** project - *MainPage.xaml.cs*
```cs
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
        // The Victor monument, Belgrade, Serbia.
        var location = new Location(44.823052, 20.447704);
        IPositionSource positionSource = new FixedLocation(location);

        var sphereObject =  world.AddModel(positionSource, sphere);
    }
}
```

