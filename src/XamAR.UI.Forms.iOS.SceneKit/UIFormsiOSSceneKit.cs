using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DryIoc;
using Foundation;
using UIKit;

namespace XamAR.UI.Forms.iOS.SceneKit
{
    public static class UIFormsiOSSceneKit
    {
        public static IContainer AddUIFormsiOSSceneKit(this IContainer container)
        {
            // This is needed to force library to be loaded.
            return container;
        }
    }
}
