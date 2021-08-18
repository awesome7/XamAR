![XamAR Logo](./github.png)

# XamAR
[![Build Status](https://dev.azure.com/awesome7/Public/_apis/build/status/awesome7.XamAR?branchName=main)](https://dev.azure.com/awesome7/Public/_build/latest?definitionId=2&branchName=main) [![NuGet Version](https://img.shields.io/nuget/vpre/XamAR.Forms)](https://img.shields.io/nuget/vpre/XamAR.Forms)

Cross-platform Augmented Reality (AR) SDK for Xamarin
Readme
## XamAR Goal
Main goal of XamAR SDK is to bring AR (augmented reality) world closer to Xamarin developers, including those who are not familiar with AR and math behind it, to enjoy benefits and new opportunities that AR brings to world of mobile devices. 
XamAR can be used in Xamarin.Forms app, and native on Android and iOS (iPhone and iPad), and in Xamarin.Forms (**trebalo bi da radi i posebno na platformama, ali nije testirano jos**).


> Example for newcomers to AR:
If you are found in an empty room, using AR you can put a table (as 3d model) in the center of room, and observe it through the device's camera. You can move device around, walk around the room - table will stay in the same place all the time, and appear as if it really in the room.

To learn more about AR on each platform, visit:
[Android - Official Overview](https://developers.google.com/ar/develop)
[iOS - Official Overview](https://developer.apple.com/augmented-reality/)

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
- On Android XamAR is using [Sceneform](https://developers.google.com/sceneform/develop) (currently is archived, and being actively developed, but can be used for many use cases)
- On iOS XamAR is using [ARKit](https://developer.apple.com/augmented-reality/arkit/)

#### Notes
- Since GPS functionality relies on GPS reading provided by the device, and is limited by precision of GPS receiver which can have margin of error from few meters, all way to tens of meters, current API is considered not to be reliable in scenarios where objects are close to the device (for example, closer than 20 meters, in general case). 
- Functions which don't rely on GPS can be used for all scenarios .
- Elevation of a GPS coordinate is calculated relative to initial position of the device (which is 0). This means positioning object on elevation of 50m, will put it, approximately, 50m from the current ground level. This is due to unreliable real elevation returned by GPS receiver.
- API supports various use cases, which can be combined. Setting different position source, direction source, distance source, allows creating more complex scenarios
- For easier start, developers can use predefined 3d model, and later replace it with custom one - either manually created or loaded from outside source.

### Getting started
Followed are basic steps needed to prepare project:
- Create Xamarin Forms project (currently versions 5.0.0.2012)
- Add nugget *XamAR.Forms* to all projects that will be used
- Open* MainPage.xaml*
- Add namespace `xmlns:views="clr-namespace:XamAR.UI.Forms.Views;assembly=XamAR.UI.Forms"` as attribute to ContentPage
- Add `<views:ARView/>` where you want AR control to appear (note: camera feed will populated complete control)
- To finish preparation, execute steps Android and/or iOS initialization, depending on platform that will be used. Those can be found below.

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
- You should be able to run the project now

### iOS
Open *AppDelegate* and in overriden *FinishedLaunhcing*  method insert:
```csharp
// Add this line here
XamAR.WorldForms.Init();

global::Xamarin.Forms.Forms.Init();
LoadApplication(new App());
```
- In project properties Bundle Signing need to be set - check (instructions)[https://docs.microsoft.com/en-us/xamarin/ios/get-started/installation/]
** moze jos detalja oko pokretanja**
- Open *Info.plist* with text editor, and add following elements (before *</dict>*) **srediti ovo jos**
- Connect Visual Studio to Mac (in case when Visual Studio is run on Windows)
- Select Debug target to your device
- You should be able to run the project now
