using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Ticket.In
{
    public class PostTicketDTO : BaseDTOPost, IMapFrom<Core.Entities.Ticketing.Ticket>
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public int? IssueUrlId { get; set; }

        public int? PriorityId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostTicketDTO, Core.Entities.Ticketing.Ticket>();
        }
    }
}

