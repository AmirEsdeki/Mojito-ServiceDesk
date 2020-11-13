using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.IssueUrl.In;
using Mojito.ServiceDesk.Application.Common.DTOs.IssueUrl.Out;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.BaseService;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.IssueUrlService
{
    public interface IIssueUrlService : IBaseService<IssueUrl, PostIssueUrlDTO, PutIssueUrlDTO, GetIssueUrlDTO, IssueUrlsFilterParams>
    {
        Task<ICollection<KeyValueDTO>> FilterAsync(string phrase = "");
    }
}
