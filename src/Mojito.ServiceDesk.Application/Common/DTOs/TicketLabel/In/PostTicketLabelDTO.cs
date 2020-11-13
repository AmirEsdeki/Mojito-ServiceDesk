using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.TicketLabel.In
{
    public class PostTicketLabelDTO : BaseDTOPost, IMapFrom<Core.Entities.Ticketing.TicketLabel>
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [Required]
        public int CustomerOrganizationId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostTicketLabelDTO, Core.Entities.Ticketing.TicketLabel>();
        }
    }
}

