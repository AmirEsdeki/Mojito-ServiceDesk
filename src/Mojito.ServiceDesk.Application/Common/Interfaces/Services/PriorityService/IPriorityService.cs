using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Priority.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Priority.Out;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.BaseService;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.PriorityService
{
    public interface IPriorityService : IBaseService<Priority, PostPriorityDTO, PutPriorityDTO, GetPriorityDTO, PrioritiesFilterParams>
    {
        Task<ICollection<KeyValueDTO>> FilterAsync(string phrase = "");
    }
}
