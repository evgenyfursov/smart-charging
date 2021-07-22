using System;

namespace SmartCharging.Data
{
    public class LogicException : Exception
    {
        public LogicException()
        {
        }

        public LogicException(string message) : base(message)
        {
        }
    }
}
