using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Ticket.In
{
    public class TicketsFilterParams : PaginationParamsDTO, IBaseDTOFilter
    {
        public string Title { get; set; }
    }
}
