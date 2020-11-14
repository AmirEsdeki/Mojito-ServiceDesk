using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.CustomerOrganization.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Ticket.Out
{
    public class GetTicketDTO : BaseDTOGet, IMapFrom<Core.Entities.Ticketing.Ticket>
    {
        public string Title { get; set; }

        public string Color { get; set; }

        public int TicketsCount { get; set; }

        public CustomerOrganizationDTO_Cross CustomerOrganization { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Ticketing.Ticket, GetTicketDTO>();
        }
    }

    public class TicketDTO_Cross : BaseDTOOut_Cross, IMapFrom<Core.Entities.Ticketing.Ticket>
    {
        public string Title { get; set; }

        public string Color { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Ticketing.Ticket, TicketDTO_Cross>();
        }
    }
}
