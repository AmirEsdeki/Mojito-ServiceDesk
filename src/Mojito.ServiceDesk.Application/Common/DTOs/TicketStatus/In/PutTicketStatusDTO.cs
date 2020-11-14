using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.TicketStatus.In
{
    public class PutTicketStatusDTO : BaseDTOPut, IMapFrom<Core.Entities.Ticketing.TicketStatus>
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PutTicketStatusDTO, Core.Entities.Ticketing.TicketStatus>();
        }
    }
}
