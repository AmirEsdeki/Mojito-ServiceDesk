using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Ticket.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Ticket.Out;
using System;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.TicketService
{
    public interface ITicketService
    {
        #region CRUD
        Task<GetTicketDTO> GetAsync(Guid ticketId);

        Task<PaginatedList<GetTicketDTO>> GetAllAsync(TicketsFilterParams arg);

        Task<GetTicketDTO> CreateAsync(PostTicketDTO entity);

        Task UpdateAsync(Guid ticketId, PutTicketDTO entity);

        Task DeleteAsync(Guid ticketId);
        #endregion

        #region RelationActions
        Task CloseTicketAsync(Guid ticketId);

        Task OpenTicketAsync(Guid ticketId);

        Task SetAsigneeAsync(Guid ticketId, string userId);

        Task SetNominatedGroupAsync(Guid ticketId, int groupId);

        Task SetIssueUrlAsync(Guid ticketId, int issueUrlId);

        Task SetStatusAsync(Guid ticketId, int ticketStatusId);

        Task SetPriorityAsync(Guid ticketId, int priorityId);

        Task AddLabelAsync(Guid ticketId, int labelId);

        Task RemoveLabelAsync(Guid ticketId, int labelId);
        #endregion

        #region TicketPiplineActions
        Task PassToNextNominee(Guid ticketId);
        #endregion
    }
}
