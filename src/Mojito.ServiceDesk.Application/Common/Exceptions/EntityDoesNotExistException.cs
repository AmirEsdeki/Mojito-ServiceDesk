using Mojito.ServiceDesk.Application.Common.Constants.Messages;
using System;

namespace Mojito.ServiceDesk.Application.Common.Exceptions
{
    public class EntityDoesNotExistException : CustomException
    {
        public EntityDoesNotExistException(Exception ex = null)
            : base(ErrorMessages.EntityNotFound, 400, ex)
        {
        }
    }
}
