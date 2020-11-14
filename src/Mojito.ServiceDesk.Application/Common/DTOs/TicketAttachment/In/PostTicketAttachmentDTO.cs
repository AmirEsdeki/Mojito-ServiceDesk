using AutoMapper;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.TicketAttachment.In
{
    public class PostTicketAttachmentDTO : IMapFrom<Core.Entities.Ticketing.TicketAttachment>
    {
        public byte[] File { get; set; }

        [Required]
        public long ConversationId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostTicketAttachmentDTO, Core.Entities.Ticketing.TicketAttachment>();
        }
    }
}
