using System.Numerics;
using XamAR.Core.Geometry;

namespace XamAR.Core.Models.Direction
{
    public class DirectionToEntity : DirectionSourceBase
    {
        public AnchoredObject Object { get; }

        public DirectionToEntity(AnchoredObject obj)
        {
            Object = obj;
        }

        protected override DirectionParameters GetDirection(WorldConverter converter, Vector3 realWorldPosition)
        {
            Vector3 objPosition = Object.PositionSource.RealWorldPosition;
            Vector3 direction = realWorldPosition.Negate().Add(objPosition);

            return new DirectionParameters(direction);
        }
    }
}
