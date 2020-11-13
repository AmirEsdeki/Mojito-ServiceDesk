using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.TicketIssue.In;
using Mojito.ServiceDesk.Application.Common.DTOs.TicketIssue.Out;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.BaseService;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.TicketIssueService
{
    public interface ITicketIssueService : IBaseService<TicketIssue, PostTicketIssueDTO, PutTicketIssueDTO, GetTicketIssueDTO, TicketIssuesFilterParams>
    {
        Task<ICollection<KeyValueDTO>> FilterAsync(string phrase = "");
    }
}
