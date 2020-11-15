using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Ticket.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Ticket.Out;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.TicketService;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.TicketService
{
    public class TicketService : ITicketService
    {
        #region ctor
        private readonly ApplicationDBContext db;
        private readonly IAppUser appUser;
        private readonly IMapper mapper;

        public TicketService(ApplicationDBContext db,
            IAppUser appUser, IMapper mapper)
        {
            this.db = db;
            this.appUser = appUser;
            this.mapper = mapper;
        }
        #endregion

        #region CRUD
        public Task<PaginatedList<GetTicketDTO>> GetAllAsync(TicketsFilterParams arg)
        {
            throw new System.NotImplementedException();
        }

        public Task<GetTicketDTO> GetAsync(string ticketId)
        {
            throw new System.NotImplementedException();
        }
        public Task<GetTicketDTO> CreateAsync(PostTicketDTO entity)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteAsync(string ticketId)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateAsync(string ticketId, PutTicketDTO entity)
        {
            throw new System.NotImplementedException();
        }
        #endregion
        #region RelationActions
        public Task AddLabelAsync(string ticketId, int labelId)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveLabelAsync(string ticketId, int labelId)
        {
            throw new System.NotImplementedException();
        }

        public Task OpenTicketAsync(string ticketId)
        {
            throw new System.NotImplementedException();
        }

        public Task CloseTicketAsync(string ticketId)
        {
            throw new System.NotImplementedException();
        }

        public Task SetAsigneeAsync(string ticketId, string userId)
        {
            throw new System.NotImplementedException();
        }

        public Task SetIssueUrlAsync(string ticketId, int issueUrlId)
        {
            throw new System.NotImplementedException();
        }

        public Task SetNominatedGroupAsync(string ticketId, int groupId)
        {
            throw new System.NotImplementedException();
        }

        public Task SetPriorityAsync(string ticketId, int priorityId)
        {
            throw new System.NotImplementedException();
        }

        public Task SetStatusAsync(string ticketId, int ticketStatusId)
        {
            throw new System.NotImplementedException();
        }
        #endregion
    }
}
