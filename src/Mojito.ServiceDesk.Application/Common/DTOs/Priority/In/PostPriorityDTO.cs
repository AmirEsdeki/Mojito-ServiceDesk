using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Priority.In
{
    public class PostPriorityDTO : BaseDTOPost, IMapFrom<Core.Entities.Ticketing.Priority>
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostPriorityDTO, Core.Entities.Ticketing.Priority>();
        }
    }
}

