using Mojito.ServiceDesk.Application.Common.Interfaces;
using System;

namespace Mojito.ServiceDesk.Infrastructure.Services.Common
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;

    }
}
