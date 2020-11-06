using System;

namespace Mojito.ServiceDesk.Application.Common.Exceptions
{
    public class UnauthorizedException : CustomException
    {
        public UnauthorizedException(Exception ex = null)
            : base($"شما دسترسی به این سرویس را ندارید.", 401, ex)
        {
        }
    }
}
