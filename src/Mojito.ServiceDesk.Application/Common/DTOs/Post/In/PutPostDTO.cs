using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Post.In
{
    public class PutPostDTO : BaseDTOPut, IMapFrom<Core.Entities.Identity.Post>
    {
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Identity.Post, PutPostDTO>();
        }
    }
}
