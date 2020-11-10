using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Post.In
{
    public class PutPostDTO : BaseDTOPut, IMapFrom<Core.Entities.Identity.Post>
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PutPostDTO, Core.Entities.Identity.Post>();
        }
    }
}
