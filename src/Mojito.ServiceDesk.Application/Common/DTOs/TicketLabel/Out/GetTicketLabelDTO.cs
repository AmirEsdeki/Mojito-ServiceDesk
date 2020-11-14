using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.CustomerOrganization.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.Linq;

namespace Mojito.ServiceDesk.Application.Common.DTOs.TicketLabel.Out
{
    public class GetTicketLabelDTO : BaseDTOGet, IMapFrom<Core.Entities.Ticketing.TicketLabel>
    {
        public string Title { get; set; }

        public string Color { get; set; }

        public int TicketsCount { get; set; }

        public CustomerOrganizationDTO_Cross CustomerOrganization { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Ticketing.TicketLabel, GetTicketLabelDTO>()
                    .ForMember(dest => dest.TicketsCount, opt => opt.MapFrom(src => src.Tickets != null ?
                    src.Tickets.Where(w => !w.Ticket.IsClosed).Count() : 0));
        }
    }

    public class TicketLabelDTO_Cross : BaseDTOOut_Cross, IMapFrom<Core.Entities.Ticketing.TicketLabel>
    {
        public string Title { get; set; }

        public string Color { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Ticketing.TicketLabel, TicketLabelDTO_Cross>();
        }
    }
}
