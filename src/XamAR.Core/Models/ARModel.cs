using System;
using System.Numerics;
using DryIoc;
using XamAR.Core.Factories;
using XamAR.Core.Models.Direction;

namespace XamAR.Core.Models
{
    /// <summary>
    ///     Wraps platform specific Drawable object, that user can
    ///     update at any moment.
    /// </summary>
    public class ARModel
    {
        private Vector3 _offset;

        /// <summary>
        ///     Unique id in single DisplayedObject.
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        public bool Visible
        {
            get => Drawable.Visible;
            set => Drawable.Visible = value;
        }

        public Vector3 Offset
        {
            get => _offset;
            set
            {
                _offset = value;
                if (Drawable != null)
                {
                    Drawable.Offset = value;
                }
            }
        }

        public IDirectionSource Direction
        {
            get => Drawable.Direction;
            set => Drawable.Direction = value;
        }

        //TODO This mustn't be accessible to the user.
        /// <summary>
        ///     For internal use.
        /// </summary>
        internal Drawable Drawable { get; private set; }

        /// <summary>
        ///     Creates ModelWrapper around platform specific object
        ///     (for example, Node for Android/Sceneform, SCNNode for iOS/ARKit).
        /// </summary>
        public static ARModel CreateWrapper(object o)
        {
            if (o == null)
            {
                throw new ArgumentNullException();
            }

            Drawable drawable = DI.Container.Resolve<IFactoryWrapper>().Create(o);
            ARModel wrapper = new ARModel(drawable);
            return wrapper;
        }

        /// <summary>
        ///     Creates ModelWrapper around Drawable, containing platform
        ///     specific object.
        /// </summary>
        public static ARModel CreateWrapper(Drawable drawable)
        {
            if (drawable == null)
            {
                throw new ArgumentNullException();
            }

            ARModel wrapper = new ARModel(drawable);
            return wrapper;
        }

        /// <summary>
        ///     Drawable which is assigned to this Wrapper.
        ///     Wrapper updates values in drawable (which are used in
        ///     next calculation iteration).
        /// </summary>
        private ARModel(Drawable drawable)
        {
            AssignDrawable(drawable);
        }

        private void AssignDrawable(Drawable drawable)
        {
            Drawable = drawable;

            // Copy default values from Drawable.
            Offset = drawable.Offset;
        }
    }
}
