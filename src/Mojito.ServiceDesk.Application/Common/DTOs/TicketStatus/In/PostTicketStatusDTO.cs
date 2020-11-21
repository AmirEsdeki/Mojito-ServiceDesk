using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.TicketStatus.In
{
    public class PostTicketStatusDTO : BaseDTOPost, IMapFrom<Core.Entities.Ticketing.TicketStatus>
    {
        [Required(ErrorMessage ="تمام فیلدهای اجباری باید دارای مقدار باشند.")]
        [StringLength(255)]
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostTicketStatusDTO, Core.Entities.Ticketing.TicketStatus>();
        }
    }
}

