using Mojito.ServiceDesk.Application.Common.Constants.Messages;
using System;

namespace Mojito.ServiceDesk.Application.Common.Exceptions
{
    public class EntityNotFoundException : CustomException
    {
        public EntityNotFoundException(Exception ex = null)
            : base(ErrorMessages.EntityNotFound, 400, ex)
        {
        }
    }
}
