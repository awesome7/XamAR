using System;
using System.Numerics;

namespace XamAR.Core.Models.Distance
{
    /// <summary>
    /// Provide custom function to calculate distance.
    /// </summary>
    public class CustomDistance : IDistanceOverride
    {
        public Func<Vector3, float> CustomFunc { get; }

        public CustomDistance(Func<Vector3, float> customFunc)
        {
            CustomFunc = customFunc ?? (d => d.Length());
        }

        public float GetDistance(Vector3 position)
        {
            return CustomFunc(position);
        }
    }
}
