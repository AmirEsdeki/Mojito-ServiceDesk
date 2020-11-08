using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.User.Out;
using System.Collections.Generic;
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

        //Task<IEnumerable<GetUserDTO>> GetAll(string id);

        Task<GetUserDTO> Get(string id);

        //Task Create(string id);
        //Task Update(string id);
        //Task Delete(string id);
    }
}
