using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;

namespace Mojito.ServiceDesk.Application.Common.DTOs.TicketIssue.In
{
    public class TicketIssuesFilterParams : PaginationParamsDTO, IBaseDTOFilter
    {
        public string Title { get; set; }
    }
}
