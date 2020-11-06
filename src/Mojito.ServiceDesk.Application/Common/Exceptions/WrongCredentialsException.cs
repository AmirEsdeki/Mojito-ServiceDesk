using Mojito.ServiceDesk.Application.Common.Constants.Messages;
using System;

namespace Mojito.ServiceDesk.Application.Common.Exceptions
{
    public class WrongCredentialsException : CustomException
    {
        public WrongCredentialsException(Exception ex = null)
            : base(ErrorMessages.WrongCredentials, 401, ex)
        {
        }
    }
}
