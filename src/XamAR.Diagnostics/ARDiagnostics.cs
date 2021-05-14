using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using DryIoc;
using XamAR.Core;
using XamAR.Core.Display;
using XamAR.Core.Models;
using XamAR.Core.Models.Geolocation;
using XamAR.Core.Sensors;

namespace XamAR.Diagnostics
{
    public class ARDiagnostics
    {
        private bool _running;

        private ARDiagnostics()
        {
        }

        public static ARDiagnostics Instance { get; } = new ARDiagnostics();

        public Vector3 CameraPosition { get; private set; }

        public Vector3 CameraDirection { get; private set; }

        public int NumberOfEntities { get; private set; } = -1;

        public Location Location { get; private set; } = new Location();

        public IEnumerable<AnchoredObject> Objects { get; private set; } = new List<AnchoredObject>();

        public Matrix4x4 World { get; private set; }

        public void Stop()
        {
            _running = false;
        }

        public async void Run()
        {
            int checkInterval = 200; // ms
            _running = true;
            IDisplayService display = DI.Container.Resolve<IDisplayService>();
            ObjectManagerService objectManager = DI.Container.Resolve<ObjectManagerService>();

            while (_running)
            {
                await Task.Delay(checkInterval);

                CameraPosition = display.CameraPosition;
                CameraDirection = display.CameraDirection;
                NumberOfEntities = objectManager.AllObjects.Count();
                Location = LocationMonitor.LastLocation;
                World = OrientationMonitor.World.WorldMatrixInverted;

                Objects = objectManager.AllObjects
                    .Select(x => objectManager.Get(x.Id))
                    .ToList();
            }
        }

        public void DrawCoordinateSystem()
        {
            DI.Container.Resolve<IDisplayService>().DrawCoordinateSystem();
        }
    }
}
