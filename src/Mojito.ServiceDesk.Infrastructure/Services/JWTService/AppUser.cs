using Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService;

namespace Mojito.ServiceDesk.Infrastructure.Services.JWTService
{
    public class AppUser : IAppUser
    {
        public string[] Roles { get; set; }

        public string Id { get; set; }

        public string IsVerified { get; set; }

        public bool IsCompanyMember { get; set; }

        public int[] Groups { get; set; }

        public int CustomerOrganizationId { get; set; }
    }
}
