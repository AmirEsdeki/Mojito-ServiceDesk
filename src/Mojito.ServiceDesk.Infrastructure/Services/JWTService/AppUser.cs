using Mojito.ServiceDesk.Application.Common.Extensions;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService;

namespace Mojito.ServiceDesk.Infrastructure.Services.JWTService
{
    public class AppUser : IAppUser
    {
        private string[] roles;
        public string[] Roles
        {
            get
            {
                roles.ForEach(f => f.ToLower());
                return roles;
            }
            set
            {
                roles = value;
            }
        }

        public string Id { get; set; }

        public string IsVerified { get; set; }

        public bool IsCompanyMember { get; set; }

        public int[] Groups { get; set; }

        public int CustomerOrganizationId { get; set; }
    }
}
