using System;
using Java.Util.Functions;

namespace XamAR.Platform.Android.Sceneform
{
    /// <summary>
    ///     Helper class - wrapper to passing a delegate on android.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DelegateConsumer<T> : Java.Lang.Object, IConsumer
        where T : Java.Lang.Object
    {
        private readonly Action<T> _completed;

        public DelegateConsumer(Action<T> action)
        {
            _completed = action;
        }

        public void Accept(Java.Lang.Object t)
        {
            _completed(global::Android.Runtime.Extensions.JavaCast<T>(t));
        }
    }
}
