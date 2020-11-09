using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;

namespace Mojito.ServiceDesk.Application.Common.DTOs.User.In
{
    public class GetAllUserParams : IBaseDTOIn
    {
        public string GeneralName { get; set; }

        public int GroupId { get; set; }

        public int IssueUrlId { get; set; }

        public int CustomerOrganizationId { get; set; }

        public int PostId { get; set; }

        public bool? IsEmployee { get; set; }
    }
}
