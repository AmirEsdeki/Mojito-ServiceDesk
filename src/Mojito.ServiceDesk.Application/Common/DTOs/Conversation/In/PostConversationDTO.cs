using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Conversation.In
{
    public class PostConversationDTO : BaseDTOPost, IMapFrom<Core.Entities.Ticketing.Conversation>
    {
        //todo: get the attachment with post data
        [Required]
        public long? TicketId { get; set; }

        public string Message { get; set; }

        public bool IsPublic { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostConversationDTO, Core.Entities.Ticketing.Conversation>();
        }
    }
}

