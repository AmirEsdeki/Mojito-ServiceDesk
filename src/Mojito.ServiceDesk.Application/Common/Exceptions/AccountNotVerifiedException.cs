using Mojito.ServiceDesk.Application.Common.Constants.Messages;
using System;

namespace Mojito.ServiceDesk.Application.Common.Exceptions
{
    public class AccountNotVerifiedException : CustomException
    {
        public AccountNotVerifiedException(Exception ex = null)
            : base(ErrorMessages.AccountNotVerified, 401, ex)
        {
        }
    }
}
