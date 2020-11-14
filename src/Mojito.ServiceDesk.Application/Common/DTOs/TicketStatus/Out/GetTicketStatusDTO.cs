using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;

namespace Mojito.ServiceDesk.Application.Common.DTOs.TicketStatus.Out
{
    public class GetTicketStatusDTO : BaseDTOGet, IMapFrom<Core.Entities.Ticketing.TicketStatus>
    {
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Ticketing.TicketStatus, GetTicketStatusDTO>();
        }
    }

    public class TicketStatusDTO_Cross : BaseDTOOut_Cross, IMapFrom<Core.Entities.Ticketing.TicketStatus>
    {
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Ticketing.TicketStatus, TicketStatusDTO_Cross>();
        }
    }
}
