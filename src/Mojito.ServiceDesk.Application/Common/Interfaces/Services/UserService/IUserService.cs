using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity.Out;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.UserService
{
    public interface IUserService
    {
        Task<GuidIdDTO> SignUpAsync(SignUpDTO arg);

        Task<UserTokenDTO> VerifyUserAsync(VerifyUserDTO arg);

        Task ResendVerificationCodeAsync(string userId);

        Task<UserTokenDTO> SignInAsync(SignInDTO arg);
    }
}
