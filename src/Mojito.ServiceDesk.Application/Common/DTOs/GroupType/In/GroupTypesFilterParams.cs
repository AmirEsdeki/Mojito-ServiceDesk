using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;

namespace Mojito.ServiceDesk.Application.Common.DTOs.GroupType.In
{
    public class GroupTypesFilterParams : PaginationParamsDTO, IBaseDTOFilter
    {
        public string Title { get; set; }
    }

}
