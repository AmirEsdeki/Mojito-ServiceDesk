using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;

namespace Mojito.ServiceDesk.Application.Common.DTOs.TicketAttachment.Out
{
    public class TicketAttachmentDTO_Cross : BaseDTOOut_Cross, IMapFrom<Core.Entities.Ticketing.TicketAttachment>
    {
        public string Location { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Ticketing.TicketAttachment, TicketAttachmentDTO_Cross>();
        }
    }
}
