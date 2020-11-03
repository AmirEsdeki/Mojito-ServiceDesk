using System;

namespace Mojito.ServiceDesk.Application.Common.Exceptions
{
    public class EntityDoesNotExistException : CustomException
    {
        public EntityDoesNotExistException(Exception ex = null)
            : base($"موجودیت موردنظر یافت نشد.", 400, ex)
        {
        }
    }
}
