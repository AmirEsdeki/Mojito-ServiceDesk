using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Conversation.In
{
    public class PutConversationDTO : BaseDTOPut, IMapFrom<Core.Entities.Ticketing.Conversation>
    {
        [Required]
        public string Message { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<PutConversationDTO, Core.Entities.Ticketing.Conversation>();
        }
    }
}
