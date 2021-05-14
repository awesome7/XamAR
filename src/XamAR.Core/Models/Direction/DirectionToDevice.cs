using System.Numerics;
using XamAR.Core.Geometry;

namespace XamAR.Core.Models.Direction
{
    /// <summary>
    /// Heading is to current device position.
    /// </summary>
    public class DirectionToDevice : DirectionSourceBase
    {
        protected override DirectionParameters GetDirection(WorldConverter converter, Vector3 realWorldPosition)
        {
            return new DirectionParameters(realWorldPosition.Negate());
        }
    }
}
