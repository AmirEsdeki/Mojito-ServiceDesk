using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Ticket.In
{
    public class PutTicketDTO : BaseDTOPut, IMapFrom<Core.Entities.Ticketing.Ticket>
    {
        public string Title { get; set; }

        public int? IssueUrlId { get; set; }

        public int? PriorityId { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<PutTicketDTO, Core.Entities.Ticketing.Ticket>();
        }
    }
}
