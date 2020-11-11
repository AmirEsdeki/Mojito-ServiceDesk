using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.GroupType.In
{
    public class PostGroupTypeDTO : BaseDTOPost, IMapFrom<Core.Entities.Identity.GroupType>
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostGroupTypeDTO, Core.Entities.Identity.GroupType>();
        }

    }
}
