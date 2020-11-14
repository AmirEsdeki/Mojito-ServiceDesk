using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Ticket.In
{
    public class PutTicketDTO : BaseDTOPut, IMapFrom<Core.Entities.Ticketing.Ticket>
    {

        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(255)]
        public string Color { get; set; }

        public int CustomerOrganizationId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PutTicketDTO, Core.Entities.Ticketing.Ticket>();
        }
    }
}
