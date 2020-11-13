using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.TicketIssue.In
{
    public class PostTicketIssueDTO : BaseDTOPost, IMapFrom<Core.Entities.Ticketing.TicketIssue>
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostTicketIssueDTO, Core.Entities.Ticketing.TicketIssue>();
        }
    }
}

