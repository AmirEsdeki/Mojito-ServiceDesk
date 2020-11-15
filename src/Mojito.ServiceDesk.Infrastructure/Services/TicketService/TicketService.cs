using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Ticket.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Ticket.Out;
using Mojito.ServiceDesk.Application.Common.Extensions;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.TicketService;
using Mojito.ServiceDesk.Core.Constant;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using System;
using System.Linq;
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
        public async Task<PaginatedList<GetTicketDTO>> GetAllAsync(TicketsFilterParams arg)
        {
            try
            {
                var userRoles = appUser.Roles;

                IQueryable<Ticket> query;

                #region AllOfTheTicketsThatUserCanAccess
                if (arg.OnlyTicketsOfGroup)
                    query = db.Tickets.Where(w => appUser.Groups.Any(a => a == w.GroupId));

                else if (arg.OnlyTicketsOfAssignee)
                    query = db.Tickets.Where(w => w.AssigneeId == appUser.Id);

                else if (arg.OnlyOpenedByUser)
                    query = db.Tickets.Where(w => w.OpenedById == appUser.Id);

                else if (arg.OnlyClosedByUser)
                    query = db.Tickets.Where(w => w.ClosedById == appUser.Id);

                else if (arg.OnlyIfUserHasParticipatedInConversation)
                    query = db.Tickets.Where(w => w.Conversations.Any(a => a.CreatedById == Guid.Parse(appUser.Id)));

                //if none of above is true it means that the user want to see all of the tickets
                //based on the role of the user the result is different
                else
                {
                    if (userRoles.Any(a => a == Roles.Admin || a == Roles.Observer))
                    {
                        //admin or observer can see all of the tickets in the system
                        query = db.Tickets.AsQueryable();
                    }

                    else if (userRoles.Any(a => a == Roles.Employee))
                    {
                        //employee can see all of the tickets that he has somehow participated in
                        query = db.Tickets.Where(w =>
                            appUser.Groups.Any(a => a == w.GroupId)
                            || w.AssigneeId == appUser.Id
                            || w.OpenedById == appUser.Id
                            || w.ClosedById == appUser.Id
                            || w.Conversations.Any(a => a.CreatedById == Guid.Parse(appUser.Id))
                            );
                    }

                    else
                    {
                        //users can only see the tickets that has opened by them
                        query = db.Tickets.Where(w => w.OpenedById == appUser.Id);
                    }
                }
                #endregion

                #region OtherFilters
                if (arg.IsClosed)
                    query = query.Where(w => w.IsClosed);

                if (arg.PriorityId != 0)
                    query = query.Where(w => w.PriorityId == arg.PriorityId);

                if (arg.IssueUrlId != 0)
                    query = query.Where(w => w.IssueUrlId == arg.IssueUrlId);

                if (arg.TicketStatusId != 0)
                    query = query.Where(w => w.TicketStatusId == arg.TicketStatusId);

                if (arg.CustomerOrganizationId != 0)
                    query = query.Where(w => w.CustomerOrganizationId == arg.CustomerOrganizationId);

                if (arg.GroupId != 0)
                    query = query.Where(w => w.GroupId == arg.GroupId);

                if (arg.AssigneeId != null)
                    query = query.Where(w => w.AssigneeId == arg.AssigneeId);

                #endregion

                #region Ordering
                //if user want to orderby his needs then:
                if (arg.OrderByProperty != null && query.PropertyExists(arg.OrderByProperty))
                {
                    if (arg.HowToOrder != null && arg.HowToOrder.ToLower() == "asc")
                    {
                        query = query.OrderByProperty(arg.OrderByProperty);
                    }
                    else
                    {
                        query = query.OrderByPropertyDescending(arg.OrderByProperty);
                    }
                }
                else
                {
                    //sort by date of the ticket so newer ones come first.
                    query = query.OrderByDescending(o => o.Created);
                }
                #endregion

                var list = await new PaginatedListBuilder<Ticket, GetTicketDTO>(mapper)
                    .CreateAsync(query, arg.PageNumber, arg.PageSize);

                return list;
            }

            catch (Exception ex)
            {
                throw;
            }
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
