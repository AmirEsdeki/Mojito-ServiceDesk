using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Ticket.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Ticket.Out;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.TicketService
{
    public interface ITicketService
    {
        #region CRUD
        Task<GetTicketDTO> GetAsync(string ticketId);

        Task<PaginatedList<GetTicketDTO>> GetAllAsync(TicketsFilterParams arg);

        Task<GetTicketDTO> CreateAsync(PostTicketDTO entity);

        Task UpdateAsync(string ticketId, PutTicketDTO entity);

        Task DeleteAsync(string ticketId);
        #endregion

        #region RelationActions
        Task CloseTicketAsync(string ticketId);

        Task OpenTicketAsync(string ticketId);

        Task SetAsigneeAsync(string ticketId, string userId);

        Task SetNominatedGroupAsync(string ticketId, int groupId);

        Task SetIssueUrlAsync(string ticketId, int issueUrlId);

        Task SetStatusAsync(string ticketId, int ticketStatusId);

        Task SetPriorityAsync(string ticketId, int priorityId);

        Task AddLabelAsync(string ticketId, int labelId);

        Task RemoveLabelAsync(string ticketId, int labelId);
        #endregion

        #region TicketPiplineActions
        Task PassToNextNominee(string ticketId);
        #endregion
    }
}
