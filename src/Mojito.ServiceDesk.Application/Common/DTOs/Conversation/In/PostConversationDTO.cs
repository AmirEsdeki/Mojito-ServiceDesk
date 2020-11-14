using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Conversation.In
{
    public class PostConversationDTO : BaseDTOPost, IMapFrom<Core.Entities.Ticketing.Conversation>
    {
        public string Message { get; set; }

        [Required]
        public long? TicketId { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PostConversationDTO, Core.Entities.Ticketing.Conversation>();
        }
    }
}

