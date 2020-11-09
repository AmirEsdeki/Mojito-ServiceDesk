using Mojito.ServiceDesk.Application.Common.Constants.Messages;
using System;

namespace Mojito.ServiceDesk.Application.Common.Exceptions
{
    public class UserNameOrEmailIsNotAvailableException : CustomException
    {
        public UserNameOrEmailIsNotAvailableException(Exception ex = null)
            : base(ErrorMessages.UserNameOrEmailNotAvailable, 401, ex)
        {
        }
    }
}
