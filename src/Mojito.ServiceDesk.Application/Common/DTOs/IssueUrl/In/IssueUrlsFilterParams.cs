using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;

namespace Mojito.ServiceDesk.Application.Common.DTOs.IssueUrl.In
{
    public class IssueUrlsFilterParams : PaginationParamsDTO, IBaseDTOFilter
    {
        public string Url { get; set; }

        public string NameOfUser { get; set; }

        public string NameOfGroup { get; set; }
    }

}
