using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;

namespace Mojito.ServiceDesk.Application.Common.DTOs.GroupType.Out
{
    public class GroupTypeDTO : BaseDTOOut, IMapFrom<Core.Entities.Identity.GroupType>
    {
        public string Title { get; set; }

        public int GroupsCount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Identity.GroupType, GroupTypeDTO>()
                .ForMember(dest => dest.GroupsCount, opt => opt.MapFrom(src => src.Groups != null ? src.Groups.Count : 0));
        }
    }

    public class GroupTypeDTO_Cross : BaseDTOOut, IMapFrom<Core.Entities.Identity.GroupType>
    {
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Identity.GroupType, GroupTypeDTO_Cross>();
        }
    }
}
