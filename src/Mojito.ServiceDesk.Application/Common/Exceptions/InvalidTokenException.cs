﻿using Mojito.ServiceDesk.Application.Common.Constants.Messages;
using System;

namespace Mojito.ServiceDesk.Application.Common.Exceptions
{
    public class InvalidTokenException : CustomException
    {
        public InvalidTokenException(Exception ex = null)
            : base(ErrorMessages.InvalidToken, 401, ex)
        {
        }
    }
}
