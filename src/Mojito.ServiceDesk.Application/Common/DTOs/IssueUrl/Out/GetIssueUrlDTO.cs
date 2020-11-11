using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.Group.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.Product.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.Linq;
using System.Text;

namespace Mojito.ServiceDesk.Application.Common.DTOs.IssueUrl.Out
{
    public class GetIssueUrlDTO : BaseDTOGet, IMapFrom<Core.Entities.Ticketing.IssueUrl>
    {
        public string Url { get; set; }

        public string FirstUser { get; set; }

        public int UsersCount { get; set; }

        public ProductDTO_Cross Product { get; set; }

        public GroupDTO_Cross Group { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Ticketing.IssueUrl, GetIssueUrlDTO>()
                .ForMember(dest => dest.UsersCount, opt => opt.MapFrom(src => src.Users != null ? src.Users.Count : 0))
                .ForMember(dest => dest.FirstUser, opt => 
                    opt.MapFrom(src => src.Users != null ? 
                    new StringBuilder()
                    .Append(src.Users.FirstOrDefault().User.FirstName)
                    .Append(" ")
                    .Append(src.Users.FirstOrDefault().User.FirstName)
                    .ToString()
                    : string.Empty));
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
