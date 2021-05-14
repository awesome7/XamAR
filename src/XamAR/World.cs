using System;
using System.Collections.Generic;
using System.Linq;
using DryIoc;
using XamAR.Core;
using XamAR.Core.Display;
using XamAR.Core.Events;
using XamAR.Core.Factories;
using XamAR.Core.Models;
using XamAR.Core.Models.Geolocation;
using XamAR.Core.Models.Position;
using XamAR.Exceptions;
using XamAR.Factory;

namespace XamAR
{
    /// <summary>
    ///     Access point to ARLibrary.
    /// </summary>
    /// <remarks>
    ///     Id of the object is used to identify it in the system.
    ///     For example it can be used in events, to remove from world...
    /// </remarks>
    public abstract class World
    {
        private readonly ObjectManagerService _objectManagerService;
        private readonly IDisplayService _displayService;
        private readonly IFactoryPOI _factoryPOI;

        protected World(
            ObjectManagerService objectManagerService,
            IDisplayService displayService,
            IFactoryPOI factoryPOI)
        {
            _objectManagerService = objectManagerService;
            _displayService = displayService;
            _factoryPOI = factoryPOI;

            ObjectManager = objectManagerService;
            ObjectManager.GlobalPressed += e => { GlobalPressed?.Invoke(e); };

            displayService.PlaneTapped += DisplayPlaneTapped;
        }

        public static World Instance
        {
            get
            {
                return DI.Container.Resolve<World>();
            }
        }

        /// <summary>
        ///     Global event, raised when any object is pressed.
        ///     <para>For specific object, attach to it's Pressed event.</para>
        /// </summary>
        public event Action<PressedEventsArgs> GlobalPressed;

        /// <summary>
        ///     Plane is recognized by AR, and user has tapped on it.
        /// </summary>
        public event Action<TappedPlaneEventArgs> PlaneTapped;

        private ObjectManagerService ObjectManager { get; }

        private void DisplayPlaneTapped(TappedPlaneEventArgs obj)
        {
            PlaneTapped?.Invoke(obj);
        }

        /// <summary>
        ///     Creates model based on factory name.
        ///     <para>Factory must be registered before.</para>
        /// </summary>
        /// <param name="model"></param>
        /// <exception cref="FactoryNotRegisteredException"></exception>
        public ARModel CreateModel(string model)
        {
            ModelFactory mf = FactoryService.GetInstance(model);
            if (mf == null)
            {
                throw new FactoryNotRegisteredException($"Model \"{model}\" not registered!");
            }

            ARModel wrapper = mf.CreateModel();
            return wrapper;
        }

        /// <summary>
        ///     Creates model based on factory name and its parameters.
        ///     <para>Factory must be registered before.</para>
        /// </summary>
        /// <param name="model"></param>
        /// <param name="parameters"></param>
        /// <exception cref="FactoryNotRegisteredException"></exception>
        public ARModel CreateModel<T>(string model, T parameters)
        {
            ModelFactory mf = FactoryService.GetInstance(model);
            if (mf == null)
            {
                throw new FactoryNotRegisteredException($"Model \"{model}\" not registered!");
            }

            ARModel wrapper = (mf as ModelFactory<T>)?.CreateModel(parameters);
            if (wrapper == null)
            {
                throw new FactoryNotRegisteredException($"Model \"{model}\" with parameter of type \"{typeof(T).Name}\" not registered!");
            }

            return wrapper;
        }

        /// <summary>
        ///     Adds POI object to the virtual world,
        ///     on specific GPS position.
        ///     <para>Returns id of the object.</para>
        /// </summary>
        public AnchoredObject AddPointOfInterest(Location location, string text = "")
        {
            IEnumerable<Drawable> drawables = _factoryPOI.Create(text);
            FixedLocation position = new FixedLocation(location);

            // Create and return Entity
            return ObjectManager.Add(drawables, position);
        }

        /// <summary>
        ///     Display user-defined object in AR on provided GPS coordinates.
        /// </summary>
        public AnchoredObject AddModel(Location location, params ARModel[] models)
        {
            FixedLocation position = new FixedLocation(location);

            return AddModel(position, models);
        }

        public AnchoredObject AddModel(IPositionSource position, params ARModel[] models)
        {
            // Check if object is of correct type.
            List<ARModel> wrapperList = models.ToList();

            AnchoredObject obj = ObjectManager.Add(wrapperList, position);
            obj.AssignModelWrappers(models);

            return obj;
        }

        public void RemoveModel(AnchoredObject obj)
        {
            ObjectManager.Remove(obj);
        }

        public void RemoveModel(Guid id)
        {
            AnchoredObject e = ObjectManager.Get(id);
            if (e == null)
            {
                return;
            }

            ObjectManager.Remove(e);
        }
    }
}
