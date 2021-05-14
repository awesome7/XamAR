using System.Numerics;

namespace XamAR.Core.Models.Direction
{
    public abstract class DirectionSourceBase : IDirectionSource
    {
        private WorldConverter _converter;
        private DirectionParameters _direction;
        private Vector3 _realWorldPosition;
        private bool _recalculateFlag = true;

        public DirectionParameters Current
        {
            get
            {
                if (_recalculateFlag)
                {
                    _recalculateFlag = false;
                    _direction = GetDirection(_converter, _realWorldPosition);
                }

                return _direction;
            }
        }

        public void RefreshDirection(WorldConverter converter, Vector3 realWorldPosition)
        {
            _converter = converter;
            _realWorldPosition = realWorldPosition;
            _recalculateFlag = true;
        }

        protected abstract DirectionParameters GetDirection(WorldConverter converter, Vector3 realWorldPosition);
    }
}
