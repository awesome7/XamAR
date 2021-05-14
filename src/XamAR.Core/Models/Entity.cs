using System;
using System.Numerics;
using XamAR.Core.Models.Direction;
using XamAR.Core.Models.Distance;
using XamAR.Core.Models.Position;

namespace XamAR.Core.Models
{
    /// <summary>
    ///     Contains Drawables which share same Location.
    ///     Each Drawable can have independent offset in local world.
    /// </summary>
    public class Entity
    {
        private IDistanceOverride _distanceOverride = new UnchangedDistance();
        private bool _visible = true;

        public Entity(Drawable drawables, IPositionSource position)
        {
            Drawable = drawables;
            Position = position;
            Direction = new DefaultDirection();
        }

        public Guid Id { get; } = Guid.NewGuid();

        public IPositionSource Position { get; set; }

        public IDistanceOverride DistanceOverride
        {
            get => _distanceOverride;
            set => _distanceOverride = value ?? _distanceOverride;
        }

        public IDirectionSource Direction
        {
            get => Drawable.Direction;
            set => Drawable.Direction = value;
        }

        /// <summary>
        ///     Offset of Entity from calculated position.
        /// </summary>
        public Vector3 Offset
        {
            get; // => Drawable.Offset;
            set; // => Drawable.Offset = value;
        } = new Vector3();

        public Quaternion Rotation
        {
            get => Drawable.UserRotation;
            set => Drawable.UserRotation = value;
        }

        /// <summary>
        ///     Direction as heading relative to North.
        /// </summary>
        public float NorthHeading { get; set; }

        public float DirectionOffset { get; set; }

        /// <summary>
        ///     Drawable root - contains Drawable children.
        /// </summary>
        /// <remarks>
        ///     Root drawable takes parameters
        /// </remarks>
        public Drawable Drawable { get; }

        public bool Visible
        {
            get => _visible;
            set
            {
                if (_visible == value)
                {
                    return;
                }

                _visible = value;
                Drawable.Visible = value;
            }
        }

        /// <summary>
        ///     Checks if provided object belongs to Drawable or it's children.
        ///     (Node on both platforms is a tree root that can contain other nodes).
        /// </summary>
        public bool ContainsObject(object obj) => Drawable.ContainsObject(obj, true);
    }
}
