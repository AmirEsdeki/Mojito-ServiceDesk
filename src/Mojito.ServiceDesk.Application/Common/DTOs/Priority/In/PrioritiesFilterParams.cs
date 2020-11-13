using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Priority.In
{
    public class PrioritiesFilterParams : PaginationParamsDTO, IBaseDTOFilter
    {
        public string Title { get; set; }
    }
}
