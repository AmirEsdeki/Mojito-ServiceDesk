using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;

namespace Mojito.ServiceDesk.Application.Common.DTOs.User.In
{
    public class UsersFilterParams : PaginationParamsDTO, IBaseDTOFilter
    {
        public string GeneralName { get; set; }

        public int GroupId { get; set; }

        public int IssueUrlId { get; set; }

        public int CustomerOrganizationId { get; set; }

        public int PostId { get; set; }

        public bool? IsCompanyMember { get; set; }
    }
}
