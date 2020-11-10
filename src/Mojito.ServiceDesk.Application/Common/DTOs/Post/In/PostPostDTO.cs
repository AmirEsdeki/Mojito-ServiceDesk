using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Post.In
{
    public class PostPostDTO : BaseDTOPost, IMapFrom<Core.Entities.Identity.Post>
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostPostDTO, Core.Entities.Identity.Post>();
        }
    }
}

