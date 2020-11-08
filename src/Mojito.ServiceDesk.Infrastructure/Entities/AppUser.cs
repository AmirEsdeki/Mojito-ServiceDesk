using Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService;

namespace Mojito.ServiceDesk.Infrastructure.Entities
{
    public class AppUser : IAppUser
    {
        public string Roles { get; set; }
        public string Id { get; set; }
        public string IsVerified { get; set; }
    }
}
