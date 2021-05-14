using System;
using System.Collections.Generic;
using System.Linq;
using XamAR.Core.Display;
using XamAR.Core.Events;
using XamAR.Core.Models;
using XamAR.Core.Models.Position;

namespace XamAR.Core
{
    public class ObjectManagerService
    {
        private readonly Dictionary<Guid, AnchoredObject> _displayedObjects = new Dictionary<Guid, AnchoredObject>();
        private readonly List<Entity> _entities = new List<Entity>();

        public ObjectManagerService(IDisplayService displayService)
        {
            Display = displayService;
            Display.Pressed += o =>
            {
                Entity entity = _entities.FirstOrDefault(x => x.ContainsObject(o));
                if (entity != null)
                {
                    AnchoredObject dObj = _displayedObjects[entity.Id];
                    PressedEventsArgs args = new PressedEventsArgs(_displayedObjects[entity.Id]);
                    dObj.RaisePressedEvent();
                    GlobalPressed?.Invoke(args);
                }
            };
        }

        public IEnumerable<Entity> AllObjects => _entities;

        private IDisplayService Display { get; }

        /// <summary>
        ///     Global event when object is pressed (this event catches Pressed
        ///     events from all displayed objects).
        ///     <para>
        ///         Each object rises its own Pressed event,
        ///         before this global one is raised.
        ///     </para>
        /// </summary>
        public event Action<PressedEventsArgs> GlobalPressed;

        public AnchoredObject Add(IEnumerable<ARModel> models, IPositionSource positionSource)
        {
            return Add(models.Select(x => x.Drawable), positionSource);
        }

        /// <summary>
        ///     Creates Entity which encapsulates all drawables, and assigns them ID.
        /// </summary>
        public AnchoredObject Add(IEnumerable<Drawable> drawables, IPositionSource positionSource)
        {
            Drawable drawable = Display.Add(drawables.ToArray());
            Entity entity = new Entity(drawable, positionSource);
            AnchoredObject anchoredObject = new AnchoredObject(entity);
            _entities.Add(entity);
            _displayedObjects.Add(anchoredObject.Entity.Id, anchoredObject);

            return anchoredObject;
        }

        /// <summary>
        ///     Returns Entity with provided Id, or null if not found.
        /// </summary>
        public AnchoredObject Get(Guid id)
        {
            if (_displayedObjects.ContainsKey(id))
            {
                return _displayedObjects[id]; //.FirstOrDefault(x => x.Id == id);
            }

            return null;
        }

        /// <summary>
        ///     Removes entity from
        /// </summary>
        /// <param name="obj"></param>
        public void Remove(AnchoredObject obj)
        {
            _displayedObjects.Remove(obj.Entity.Id);
            _entities.Remove(obj.Entity);
            // Remove from display.
            Display.Remove(obj.Entity.Drawable);
        }
    }
}
