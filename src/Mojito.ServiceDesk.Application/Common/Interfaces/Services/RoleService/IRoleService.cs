using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.RoleService
{
    public interface IRoleService
    {
        Task<GuidIdDTO> CreateRole(string role);
    }
}
