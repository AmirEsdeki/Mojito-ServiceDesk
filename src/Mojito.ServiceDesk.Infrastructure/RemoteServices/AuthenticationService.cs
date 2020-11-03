using Mojito.ServiceDesk.Application.Common.Interfaces;
using Mojito.ServiceDesk.Application.Common.Models;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.RemoteServices
{
    public class AuthenticationService : RemoteServiceHandler, IAuthenticationService
    {
        public JwtClaims Identity { get; private set; }

        public async Task<JwtClaims> LoadUserIdentityAsync(string token)
        {
            return Identity;
        }
    }
}
