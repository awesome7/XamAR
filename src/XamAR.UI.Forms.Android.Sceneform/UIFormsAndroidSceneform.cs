using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using DryIoc;

namespace XamAR.UI.Forms.Android.Sceneform
{
    public static class UIFormsAndroidSceneform
    {
        public static IContainer AddUIFormsAndroidSceneform(this IContainer container)
        {
            // This is needed to force library to be loaded.
            return container;
        }
    }
}
