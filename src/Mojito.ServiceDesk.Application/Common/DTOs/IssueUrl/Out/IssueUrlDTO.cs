using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.Product.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;

namespace Mojito.ServiceDesk.Application.Common.DTOs.IssueUrl.Out
{
    public class IssueUrlDTO : BaseDTOGet, IMapFrom<Core.Entities.Ticketing.IssueUrl>
    {
        public string Url { get; set; }

        public int UsersCount { get; set; }

        public ProductDTO_Cross Product { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Ticketing.IssueUrl, IssueUrlDTO>()
                .ForMember(dest => dest.UsersCount, opt => opt.MapFrom(src => src.Users != null ? src.Users.Count : 0));
        }
    }

    public class IssueUrlDTO_Cross : BaseDTOOut_Cross, IMapFrom<Core.Entities.Ticketing.IssueUrl>
    {
        public string Url { get; set; }

        public ProductDTO_Cross Product { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Ticketing.IssueUrl, IssueUrlDTO_Cross>();
        }
    }
}
