using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.CustomerOrganization.In;
using Mojito.ServiceDesk.Application.Common.DTOs.CustomerOrganization.Out;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.BaseService;
using Mojito.ServiceDesk.Core.Entities.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.CustomerOrganizationService
{
    public interface ICustomerOrganizationService : IBaseService<CustomerOrganization, PostCustomerOrganizationDTO, PutCustomerOrganizationDTO, GetCustomerOrganizationDTO, CustomerOrganizationsFilterParams>
    {
        Task<ICollection<KeyValueDTO>> FilterAsync(string phrase = "");
    }
}
