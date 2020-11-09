using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Post.Out
{
    public class PostDTO_Cross : BaseDTOOut, IMapFrom<Core.Entities.Identity.Post>
    {
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Identity.Post, PostDTO_Cross>();
        }
    }
}
