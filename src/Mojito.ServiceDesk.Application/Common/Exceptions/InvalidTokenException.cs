using System;

namespace Mojito.ServiceDesk.Application.Common.Exceptions
{
    public class InvalidTokenException : CustomException
    {
        public InvalidTokenException(Exception ex = null)
            : base($"توکن معتبر نیست.", 401, ex)
        {
        }
    }
}
