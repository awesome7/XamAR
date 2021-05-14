using SceneKit;
using XamAR.Core.Models;

namespace XamAR.Platform.iOS.SceneKit.Extensions
{
    public static class ScnNodeExtensions
    {
        /// <summary>
        ///     Wraps SCNNode with ModelWrapper.
        /// </summary>
        public static ARModel AsModelWrapper(this SCNNode node)
        {
            return ARModel.CreateWrapper(node);
        }
    }
}
