using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.GroupType.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Group.Out
{
    public class GroupDTO : BaseDTOGet, IMapFrom<Core.Entities.Identity.Group>
    {
        public string Name { get; set; }

        public int UsersCount { get; set; }
            
        public int IssueUrlsCount { get; set; }

        #region relations
        public GroupTypeDTO_Cross GroupType { get; set; }
        #endregion

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Identity.Group, GroupDTO>()
                .ForMember(dest => dest.UsersCount, opt => opt.MapFrom(src => src.Users != null ? src.Users.Count : 0))
                .ForMember(dest => dest.IssueUrlsCount, opt => opt.MapFrom(src => src.IssueUrls != null ? src.IssueUrls.Count : 0));
        }
    }

    public class GroupDTO_Cross : BaseDTOOut_Cross, IMapFrom<Core.Entities.Identity.Group>
    {
        public string Name { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Identity.Group, GroupDTO_Cross>().ReverseMap();
        }
    }
}
