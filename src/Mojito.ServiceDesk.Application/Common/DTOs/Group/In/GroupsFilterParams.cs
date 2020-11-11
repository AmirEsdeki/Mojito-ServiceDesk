using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Group.In
{
    public class GroupsFilterParams : PaginationParamsDTO, IBaseDTOFilter
    {
        public string Name { get; set; }

        public int? GroupTypeId { get; set; }
    }

}
