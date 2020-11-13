using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.TicketLabel.In;
using Mojito.ServiceDesk.Application.Common.DTOs.TicketLabel.Out;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.BaseService;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.TicketLabelService
{
    public interface ITicketLabelService : IBaseService<TicketLabel, PostTicketLabelDTO, PutTicketLabelDTO, GetTicketLabelDTO, TicketLabelsFilterParams>
    {
        Task<ICollection<KeyValueDTO>> FilterAsync(string phrase = "");
    }
}
