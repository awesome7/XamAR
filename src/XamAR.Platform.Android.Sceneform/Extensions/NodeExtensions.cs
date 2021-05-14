using Google.AR.Sceneform;
using XamAR.Core.Models;

namespace XamAR.Platform.Android.Sceneform.Extensions
{
    public static class NodeExtensions
    {
        /// <summary>
        ///     Wraps Node with ModelWrapper.
        /// </summary>
        public static ARModel AsARModel(this Node node)
        {
            return ARModel.CreateWrapper(node);
        }
    }
}
