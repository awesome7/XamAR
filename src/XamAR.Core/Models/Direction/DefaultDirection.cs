using System.Numerics;

namespace XamAR.Core.Models.Direction
{
    /// <summary>
    /// Default direction - direction is not changed
    /// from initial values.
    /// </summary>
    public class DefaultDirection : DirectionSourceBase
    {
        private readonly DirectionParameters _defaultParameters = new DirectionParameters(new Vector3(), false);

        protected override DirectionParameters GetDirection(WorldConverter converter, Vector3 realWorldPosition)
        {
            return _defaultParameters;
        }
    }
}
