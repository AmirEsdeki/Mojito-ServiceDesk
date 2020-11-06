using Mojito.ServiceDesk.Application.Common.Constants.Messages;
using System;

namespace Mojito.ServiceDesk.Application.Common.Exceptions
{
    public class UnauthorizedException : CustomException
    {
        public UnauthorizedException(Exception ex = null)
            : base(ErrorMessages.Unauthorized, 401, ex)
        {
        }
    }
}
