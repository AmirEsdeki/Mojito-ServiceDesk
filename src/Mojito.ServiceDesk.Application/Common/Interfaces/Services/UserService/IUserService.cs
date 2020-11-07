using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity.Out;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.UserService
{
    public interface IUserService
    {
        Task<GuidIdDTO> SignUpAsync(SignUpDTO arg);

        Task<UserTokenDTO> VerifyUserAsync(VerifyUserDTO arg, string ip);

        Task ResendVerificationCodeAsync(string userId);

        Task<UserTokenDTO> SignInAsync(SignInDTO arg, string ip);

        Task<UserTokenDTO> RefreshToken(string token, string ipAddress);

        Task<bool> RevokeToken(string token, string ipAddress);
    }
}
