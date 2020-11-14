using Mojito.ServiceDesk.Application.Common.DTOs.Product.In;
using Mojito.ServiceDesk.Application.Common.DTOs.ProfileImage.In;
using Mojito.ServiceDesk.Application.Common.DTOs.ProfileImage.Out;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.ProfileImageService
{
    public interface IProfileImageService
    {
        Task<GetProfileImageDTO> GetAsync(int id);

        Task<GetProfileImageDTO> CreateAsync(PostProfileImageDTO entity);

        Task DeleteAsync(int Id);
    }
}
