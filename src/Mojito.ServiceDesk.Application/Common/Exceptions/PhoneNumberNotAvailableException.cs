using Mojito.ServiceDesk.Application.Common.Constants.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mojito.ServiceDesk.Application.Common.Exceptions
{
    public class PhoneNumberNotAvailableException : CustomException
    {
        public PhoneNumberNotAvailableException(Exception ex = null)
            : base(ErrorMessages.PhoneNumberNotAvailable, 400, ex)
        {
        }
    }
}
