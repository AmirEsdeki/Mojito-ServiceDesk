using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;

namespace Mojito.ServiceDesk.Application.Common.DTOs.CustomerOrganization.Out
{
    public class GetCustomerOrganizationDTO : BaseDTOOut, IMapFrom<Core.Entities.Identity.CustomerOrganization>
    {
        public string Name { get; set; }

        #region relation
        public int UsersCount { get; set; }
        public int ProductsCount { get; set; }
        #endregion

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Identity.CustomerOrganization, GetCustomerOrganizationDTO>()
                .ForMember(dest => dest.UsersCount, opt => opt.MapFrom(src => src.Users !=null? src.Users.Count : 0))
                .ForMember(dest => dest.ProductsCount, opt => opt.MapFrom(src => src.Products !=null? src.Products.Count : 0));
        }
    }
}
