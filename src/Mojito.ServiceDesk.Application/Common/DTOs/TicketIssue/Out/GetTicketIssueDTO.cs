using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.Linq;

namespace Mojito.ServiceDesk.Application.Common.DTOs.TicketIssue.Out
{
    public class GetTicketIssueDTO : BaseDTOGet, IMapFrom<Core.Entities.Ticketing.TicketIssue>
    {
        public string Title { get; set; }

        public int TicketsCount { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Ticketing.TicketIssue, GetTicketIssueDTO>()
                .ForMember(dest => dest.TicketsCount, opt => opt.MapFrom(src => src.Tickets != null ? 
                    src.Tickets.Where(w => !w.IsClosed).Count() : 0)); 
        }
    }

    public class TicketIssueDTO_Cross : BaseDTOOut_Cross, IMapFrom<Core.Entities.Ticketing.TicketIssue>
    {
        public string Title { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Ticketing.TicketIssue, TicketIssueDTO_Cross>();
        }
    }
}
