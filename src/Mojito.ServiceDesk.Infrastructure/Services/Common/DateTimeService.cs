using Mojito.ServiceDesk.Application.Common.Interfaces.Common;
using System;

namespace Mojito.ServiceDesk.Infrastructure.Services.Common
{
    public class DateTimeService : IDateTimeService
    {
        public DateTime Now => DateTime.Now;

    }
}
