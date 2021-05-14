using System;
using Google.AR.Sceneform;
using XamAR.Core.Factories;
using XamAR.Core.Models;
using XamAR.Platform.Android.Sceneform.Drawables;

namespace XamAR.Platform.Android.Sceneform.Factories
{
    public class FactoryWrapper : IFactoryWrapper
    {
        public Drawable Create(object model)
        {
            _ = model ?? throw new ArgumentNullException(nameof(model));

            if (!(model is Node node))
            {
                throw new NotSupportedException($"Object of type {model.GetType().Name} is not supported on Android");
            }

            //var d = ((DisplayAndroid)ServicesManager.Display);
            // Fragment is needed for TransformableNode, so user creates 
            // basic Node, and we will wrap it (to provide touch events).
            Node wrapNode = node; // new TransformableNode(d.Fragment.TransformationSystem);
            //wrapNode.AddChild(node);
            DrawableNode drawable = new DrawableNode(wrapNode);

            return drawable;
        }
    }
}
