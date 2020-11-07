using Mojito.ServiceDesk.Core.Entities.Identity;
using System.Collections.Generic;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService
{
    public interface IJwtService
    {
        string GenerateAuthorizationToken(User user, IEnumerable<string> role);
        RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
