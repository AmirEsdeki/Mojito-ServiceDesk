using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.TicketStatus.In;
using Mojito.ServiceDesk.Application.Common.DTOs.TicketStatus.Out;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.BaseService;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.TicketStatusService
{
    public interface ITicketStatusService : IBaseService<TicketStatus, PostTicketStatusDTO, PutTicketStatusDTO, GetTicketStatusDTO, TicketStatusFilterParams>
    {
        Task<ICollection<KeyValueDTO>> FilterAsync(string phrase = "");
    }
}
