using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using XamAR.Core.Events;
using XamAR.Core.Models.Direction;
using XamAR.Core.Models.Distance;
using XamAR.Core.Models.Position;

namespace XamAR.Core.Models
{
    /// <summary>
    /// Wrapper for Entity given to the user.
    /// Can update some display properties from here.
    /// </summary>
    public class AnchoredObject
    {
        /// <summary>
        /// User tapped part of this object).
        /// </summary>
        public event Action<PressedEventsArgs> Pressed;

        public Quaternion Rotation
        {
            get => Entity.Rotation;
            set => Entity.Rotation = value;
        }

        public float DirectionRelativeToDevice => Entity.DirectionOffset;

        public float DirectionHeading => Entity.NorthHeading;

        /// <summary>
        /// Offset from calculated Location.
        /// </summary>
        public Vector3 Offset
        {
            get => Entity.Offset;
            set => Entity.Offset = value;
        }

        public IPositionSource PositionSource
        {
            get => Entity.Position;
            set
            {
                Entity.Position = value ?? throw new NotSupportedException("Position can't be null!");
            }
        }

        public IDistanceOverride DistanceOverride
        {
            set => Entity.DistanceOverride = value;
        }

        /// <summary>
        /// If null, direction is default.
        /// </summary>
        public IDirectionSource DirectionSource
        {
            get
            {
                return Entity.Direction is DefaultDirection ? null : Entity.Direction;
            }
            set
            {
                value ??= new DefaultDirection();
                Entity.Direction = value;
            }
        }

        /// <summary>
        /// All ModelWrappers related to this DisplayObject.
        /// </summary>
        public IEnumerable<ARModel> ModelWrappers { get; private set; }

        /// <summary>
        /// False - Hides all models for object.
        /// True - Visibility state is determined by model.
        /// </summary>
        public bool Visible
        {
            get => Entity.Visible;
            set => Entity.Visible = value;
        }

        internal Entity Entity { get; }

        internal AnchoredObject(Entity ent)
        {
            Entity = ent;
        }

        public void AssignModelWrappers(IEnumerable<ARModel> wrappers)
        {
            ModelWrappers = wrappers.ToArray();
        }

        internal void RaisePressedEvent()
        {
            Pressed?.Invoke(new PressedEventsArgs(this));
        }

        /// <summary>
        /// Sets Visible state of each single model.
        /// <para>Note: models will be visible only if DisplayedObject.Visible is true.</para>
        /// </summary>
        public void SetVisibilityForEachModel(bool visible)
        {
            foreach (ARModel wrapper in ModelWrappers)
            {
                wrapper.Visible = visible;
            }
        }
    }
}
