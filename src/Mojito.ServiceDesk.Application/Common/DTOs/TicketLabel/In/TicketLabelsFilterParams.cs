using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;

namespace Mojito.ServiceDesk.Application.Common.DTOs.TicketLabel.In
{
    public class TicketLabelsFilterParams : PaginationParamsDTO, IBaseDTOFilter
    {
        public string Title { get; set; }
    }
}
