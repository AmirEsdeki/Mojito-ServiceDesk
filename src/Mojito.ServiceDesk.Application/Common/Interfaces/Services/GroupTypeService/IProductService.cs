using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.GroupType.In;
using Mojito.ServiceDesk.Application.Common.DTOs.GroupType.Out;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.BaseService;
using Mojito.ServiceDesk.Core.Entities.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.GroupTypeService
{
    public interface IGroupTypeService : IBaseService<GroupType, PostGroupTypeDTO, PutGroupTypeDTO, GetGroupTypeDTO, GroupTypesFilterParams>
    {
        Task<ICollection<KeyValueDTO>> FilterAsync(string phrase = "");
    }
}
