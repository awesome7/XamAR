using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using XamAR.Core.Models.Direction;

namespace XamAR.Core.Models
{
    /// <summary>
    ///     Encapsulates platform-specific displayed object.
    ///     Contains data needed for recalculation of position,
    ///     with initial data extracted on the start.
    /// </summary>
    public abstract class Drawable
    {
        private readonly List<Drawable> _children = new List<Drawable>();

        private bool _visible = true;

        /// <summary>
        ///     Children are responsible for extracting initial data.
        /// </summary>
        /// <param name="children"></param>
        protected Drawable(IEnumerable<Drawable> children = null)
        {
            IEnumerable<Drawable> collection = children?.ToList();

            if (collection != null && collection.Any())
            {
                _children.AddRange(collection);
            }
        }

        /// <summary>
        ///     Offset in local space.
        /// </summary>
        public Vector3 Offset { get; set; } = new Vector3();

        /// <summary>
        ///     Rotation controlled by the user.
        /// </summary>
        public Quaternion UserRotation { get; set; }

        /// <summary>
        ///     Calculated (real) rotation, which includes UserRotation.
        /// </summary>
        public Quaternion Rotation { get; set; } = Quaternion.Identity;

        public float Scale { get; set; } = 1;

        public abstract object Object { get; }

        public IEnumerable<Drawable> Children => _children;

        public IDirectionSource Direction { get; set; }

        /// <summary>
        ///     Indicates if Drawable is displayed (true), or hidden (false).
        /// </summary>
        public bool Visible
        {
            get => _visible;
            set
            {
                if (_visible != value)
                {
                    _visible = value;
                    SetVisibleInternal(_visible);
                }
            }
        }

        /// <summary>
        ///     Changes visibility of Drawable (shows or hides from scene).
        ///     Invoked only when flag is changed.
        /// </summary>
        protected abstract void SetVisibleInternal(bool visible);

        /// <summary>
        ///     Not sure about this. Nodes keep inner structure also.
        /// </summary>
        public void AddChild(Drawable d)
        {
            _children.Add(d);
        }

        public void RemoveChild(Drawable d)
        {
            _children.Remove(d);
        }

        /// <summary>
        ///     Invoke when geometry of Drawable is changed,
        ///     to update displayed node.
        /// </summary>
        public abstract void RefreshDisplayedObject();

        /// <summary>
        ///     Checks if object belongs to Drawable (and maybe it's children).
        /// </summary>
        public abstract bool ContainsObject(object obj, bool includeChildren = false);

        /// <summary>
        ///     Returns all children of this node (in all trees).
        ///     <para>Not including current Drawable.</para>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Drawable> GetAllChildren()
        {
            foreach (Drawable c in Children)
            {
                yield return c;
            }

            foreach (Drawable c in Children)
            {
                foreach (Drawable subC in c.GetAllChildren())
                {
                    yield return subC;
                }
            }
        }
    }
}
