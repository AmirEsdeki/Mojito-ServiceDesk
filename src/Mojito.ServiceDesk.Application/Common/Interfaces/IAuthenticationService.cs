using Mojito.ServiceDesk.Application.Common.Models;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces
{
    public interface IAuthenticationService
    {
        Task<JwtClaims> LoadUserIdentityAsync(string token);

        JwtClaims Identity { get; }
    }
}
