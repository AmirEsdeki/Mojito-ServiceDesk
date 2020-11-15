using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Conversation.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Conversation.Out;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.ConversationService
{
    public interface IConversationService
    {
        Task<PaginatedList<GetConversationDTO>> GetAllAsync(ConversationsFilterParams arg);

        Task<GetConversationDTO> CreateAsync(PostConversationDTO entity);

        Task DeleteAsync(string id);
    }
}
