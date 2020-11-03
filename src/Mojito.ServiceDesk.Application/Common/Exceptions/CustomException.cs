using System;

namespace Mojito.ServiceDesk.Application.Common.Exceptions
{
    public class CustomException : Exception
    {
        protected int _statusCode;
        public int StatusCode => _statusCode;

        public CustomException(string message, int statusCode, Exception ex = null) : base(message, ex)
        {
            _statusCode = statusCode;
        }
    }
}


