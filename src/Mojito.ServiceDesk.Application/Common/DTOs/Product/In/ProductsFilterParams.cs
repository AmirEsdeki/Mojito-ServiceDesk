using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Product.In
{
    public class ProductsFilterParams : PaginationParamsDTO, IBaseDTOFilter
    {

        public string Name { get; set; }

        public int? CustomerId { get; set; }
    }

}
