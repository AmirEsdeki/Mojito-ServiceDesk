using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Mojito.ServiceDesk.Application.Common.Exceptions
{
    public class ValidationException : CustomException
    {
        public ValidationException(IEnumerable<IdentityError> errors, Exception ex = null)
            : base("خطا به دلایل ذیل:", 400, ex)
        {
            Errors = errors;
        }
        public IEnumerable<IdentityError> Errors { get; set; }
    }
}
