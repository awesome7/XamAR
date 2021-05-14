using System;
using SceneKit;
using XamAR.Core.Factories;
using XamAR.Core.Models;
using XamAR.Platform.iOS.SceneKit.Drawables;

namespace XamAR.Platform.iOS.SceneKit.Factories
{
    public class FactoryWrapper : IFactoryWrapper
    {
        public Drawable Create(object model)
        {
            _ = model ?? throw new ArgumentNullException(nameof(model));

            if (!(model is SCNNode node))
            {
                throw new NotSupportedException($"Object of type {model.GetType().Name} is not supported on iOS.");
            }

            DrawableScnNode drawable = new DrawableScnNode(node);
            return drawable;
        }
    }
}
