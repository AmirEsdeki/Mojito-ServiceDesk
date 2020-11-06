using Mojito.ServiceDesk.Application.Common.Constants.Messages;
using System;

namespace Mojito.ServiceDesk.Application.Common.Exceptions
{
    public class AccountLockedException : CustomException
    {
        public AccountLockedException(Exception ex = null)
            : base(ErrorMessages.AccountHasLocked, 401, ex)
        {
        }
    }
}
