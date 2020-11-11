using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;

namespace Mojito.ServiceDesk.Application.Common.DTOs.CustomerOrganization.In
{
    public class CustomerOrganizationsFilterParams : PaginationParamsDTO, IBaseDTOFilter
    {

        public string Name { get; set; }
    }

}
