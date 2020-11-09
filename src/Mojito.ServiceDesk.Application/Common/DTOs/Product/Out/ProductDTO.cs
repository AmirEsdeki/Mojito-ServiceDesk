using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.CustomerOrganization.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Product.Out
{
    public class ProductDTO : BaseDTOOut, IMapFrom<Core.Entities.Proprietary.Product>
    {
        public string Name { get; set; }

        #region relations
        public virtual GetCustomerOrganizationDTO_Cross Customer { get; set; }
        #endregion

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Proprietary.Product, ProductDTO>();
        }
    }

    public class ProductDTO_Cross : BaseDTOOut_Cross, IMapFrom<Core.Entities.Proprietary.Product>
    {
        public string Name { get; set; }

        #region relations
        public virtual GetCustomerOrganizationDTO_Cross Customer { get; set; }
        #endregion

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Proprietary.Product, ProductDTO_Cross>();
        }
    }
}
