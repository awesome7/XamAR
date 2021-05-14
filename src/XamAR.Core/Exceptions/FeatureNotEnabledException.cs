using System;

namespace XamAR.Core.Exceptions
{
    public class FeatureNotEnabledException : Exception
    {
        public FeatureNotEnabledException(string message = null) : base(message)
        {
        }
    }
}
