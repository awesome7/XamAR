using System.Numerics;

namespace XamAR.Core.Models.Direction
{
    /// <summary>
    /// Heading is to bearing angle [0-360) relative to North.
    /// (clockwise)
    /// </summary>
    public abstract class AnchorDirection : DirectionSourceBase
    {
        // Not a good solution, but we don't want to give user DirectionParameters.
        // Method is declared as internal, which doesn't allow us to create it here 
        // as abstract.
        protected override DirectionParameters GetDirection(WorldConverter converter, Vector3 realWorldPosition)
        {
            Vector3 direction = GetDirectionInternal(converter, realWorldPosition);
            var par = new DirectionParameters(direction);

            return par;
        }

        /// <summary>
        /// Returns direction in real world.
        /// </summary>
        /// <param name="converter"></param>
        /// <param name="realWorldPosition"></param>
        /// <returns></returns>
        protected abstract Vector3 GetDirectionInternal(WorldConverter converter, Vector3 realWorldPosition);
    }
}
