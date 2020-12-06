using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.TicketAttachment.Out;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System;
using System.Linq;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Conversation.Out
{
    public class GetConversationDTO : BaseDTOGetWithGuidId, IMapFrom<Core.Entities.Ticketing.Conversation>
    {
        public string Message { get; set; }

        public string FullName { get; set; }

        public string ProfileImage { get; set; }

        public Guid? TicketId { get; set; }

        public int AttachmentsCount { get; set; }

        public TicketAttachmentDTO_Cross Attachments { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Core.Entities.Ticketing.Conversation, GetConversationDTO>()
                .ForMember(dest => dest.AttachmentsCount, opt => opt.MapFrom(src => src.Attachment != null ?
                    src.Attachment.Count() : 0));
        }
    }
}
