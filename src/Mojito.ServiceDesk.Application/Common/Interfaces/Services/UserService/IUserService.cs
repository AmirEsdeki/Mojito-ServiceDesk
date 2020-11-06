using Mojito.ServiceDesk.Application.Common.DTOs.Identity.In;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.UserService
{
    public interface IUserService
    {
        Task RegisterAsync(SignupDTO arg);

        Task VerifyUserAsync(VerifyUserDTO arg);
    }
}
