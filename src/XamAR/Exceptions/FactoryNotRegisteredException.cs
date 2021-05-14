using System;

namespace XamAR.Exceptions
{
    public class FactoryNotRegisteredException : Exception
    {
        public FactoryNotRegisteredException(string message = "") : base(message)
        {
        }
    }
}
