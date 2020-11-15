using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Ticket.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Ticket.Out;
using Mojito.ServiceDesk.Application.Common.Exceptions;
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
                    query = db.Tickets.Where(w => appUser.Groups.Any(a => a == w.NomineeGroupId));

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
                            appUser.Groups.Any(a => a == w.NomineeGroupId)
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
                if (arg.FromDate != null)
                    query = query.Where(w => w.Created >= arg.FromDate);

                if (arg.ToDate != null)
                    query = query.Where(w => w.Created <= arg.ToDate);

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

                if (arg.NomineeGroupId != 0)
                    query = query.Where(w => w.NomineeGroupId == arg.NomineeGroupId);

                if (arg.AssigneeId != null)
                    query = query.Where(w => w.AssigneeId == arg.AssigneeId);

                if (arg.HasAssignee != null)
                {
                    if (arg.HasAssignee.Value)
                        query = query.Where(w => w.AssigneeId != null || w.NomineeGroupId != null);
                    else
                        query = query.Where(w => w.AssigneeId == null || w.NomineeGroupId == null);
                }

                if (arg.Title != null)
                {
                    query = query.Where(w => w.Title.Contains(arg.Title)
                        || w.Conversations.Any(a => a.Message.Contains(arg.Title)));
                }

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

        public async Task<GetTicketDTO> GetAsync(string ticketId)
        {
            try
            {
                var userRoles = appUser.Roles;

                Ticket entity;

                if (userRoles.Any(a => a == Roles.Admin || a == Roles.Observer))
                {
                    //admin or observer can see all of the tickets in the system
                    entity = await db.Tickets.FirstOrDefaultAsync(f => f.Id == Guid.Parse(ticketId));
                }

                else if (userRoles.Any(a => a == Roles.Employee))
                {
                    //employee can see all of the tickets that he has somehow participated in
                    entity = await db.Tickets
                        .Where(f => f.Id == Guid.Parse(ticketId))
                        .Where(w =>
                        appUser.Groups.Any(a => a == w.NomineeGroupId)
                        || w.AssigneeId == appUser.Id
                        || w.OpenedById == appUser.Id
                        || w.ClosedById == appUser.Id
                        || w.Conversations.Any(a => a.CreatedById == Guid.Parse(appUser.Id))
                        )
                        .FirstOrDefaultAsync();
                }
                else
                {
                    //users can only see the tickets that has opened by them
                    entity = await db.Tickets
                        .Where(f => f.Id == Guid.Parse(ticketId))
                        .Where(w => w.OpenedById == appUser.Id)
                        .FirstOrDefaultAsync();
                }

                if (entity == null)
                    throw new EntityNotFoundException();

                var dto = mapper.Map<GetTicketDTO>(entity);

                return dto;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<GetTicketDTO> CreateAsync(PostTicketDTO entity)
        {
            var ticket = mapper.Map<Ticket>(entity);

            #region SetOtherProps
            ticket.Identifier = "#" + Guid.NewGuid().ToString().Substring(0, 8);

            ticket.CustomerOrganizationId = appUser.CustomerOrganizationId;

            ticket.OpenedById = appUser.Id;

            //the ticket first status is open, so we selected the TicketStatus that its title is "open"
            ticket.TicketStatusId = (await db.TicketStatus.FirstOrDefaultAsync(f => f.Title == "باز")).Id;

            //If no priority has set by the user the priority with title "medium" is selected automatically
            ticket.PriorityId ??= (await db.Priorities.FirstOrDefaultAsync(f => f.Title == "متوسط")).Id;
            #endregion

            #region SettingTicketPipelineRules
            var thisTicketIssuePipeline = await db.TicketManagingPipelines.
                Where(w =>
                (w.CustomerOrganizationId == ticket.CustomerOrganizationId
                || w.CustomerOrganizationId == 0) //for example intraorganizational tickets has no CustomerOrganizationId because they are not related to a customer
                && w.TicketIssueId == ticket.TicketIssueId)
                .ToListAsync();

            if (thisTicketIssuePipeline != null && thisTicketIssuePipeline.Count() != 0)
            {
                var stepCount = thisTicketIssuePipeline.Count();
                var pipeline = thisTicketIssuePipeline.FirstOrDefault(f => f.Step == 1);//when a ticket is created it is in the step 1

                ticket.CurrentStep = 1;
                ticket.MaximumSteps = stepCount;

                if (pipeline != null)
                {
                    //if for this step the pipeline declared a groupid it will assign to the nominee group of ticket
                    //and we skip through
                    if (pipeline.NomineeGroupId != 0)
                        ticket.NomineeGroupId = pipeline.NomineeGroupId;

                    //if in pipeline it has mentioned that for this step it should set to the corrosponding groupId
                    //based on IssueId this section will do that
                    else if (pipeline.SetToNomineeGroupBasedOnIssueUrl)
                    {
                        //if the ticket has not issue id assigning a nominee will never perform.
                        if (ticket.IssueUrlId != null)
                        {
                            //if the issueUrl is not a real one then assigning a nominee will never perform.
                            var issueUrl = await db.IssueUrls.FirstOrDefaultAsync(f => f.Id == ticket.IssueUrlId);

                            if (issueUrl != null)
                                ticket.NomineeGroupId = issueUrl.GroupId;
                        }
                    }

                    //if in pipeline it has mentioned that for this step it should set to the corrosponding userId
                    //based on IssueId this section will do that
                    else if (pipeline.SetToNomineePersonBasedOnIssueUrl)
                    {
                        //if the ticket has not issue id assigning a nominee will never perform.
                        if (ticket.IssueUrlId != null)
                        {
                            //if the issueUrl is not a real one then assigning a nominee will never perform.
                            var issueUrl = await db.IssueUrls.FirstOrDefaultAsync(f => f.Id == ticket.IssueUrlId);
                            //it will set the ticket to first user corrosponding to issueurl
                            if (issueUrl != null)
                                ticket.AssigneeId = issueUrl.Users.FirstOrDefault().Id.ToString();
                        }
                    }
                }
            }

            #endregion

            await using var transaction = await db.Database.BeginTransactionAsync();

            try
            {
                var addedTicket = db.Tickets.Add(ticket);
                var ticketId = addedTicket.Entity.Id;

                //a user just can send public messages 
                // but other roles can choose either their massage be public or private (means just visible to employees not customers)
                var isMessagePublic = appUser.Roles.Any(a => a == Roles.User) ?
                    true :
                    entity.IsMessagePublic;

                db.Conversations.Add(
                    new Conversation
                    {
                        Message = entity.Message,
                        IsPublic = isMessagePublic,
                        TicketId = ticketId
                    });

                //todo: handle attachment

                await transaction.CommitAsync();

                var dto = mapper.Map<GetTicketDTO>(addedTicket.Entity);

                return dto;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task DeleteAsync(string ticketId)
        {
            var entity = await db.Tickets.FirstOrDefaultAsync(x => x.Id == Guid.Parse(ticketId));

            if (entity != null)
            {
                entity.IsDeleted = true;

                await db.SaveChangesAsync();
            }
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

        public Task PassToNextNominee(string ticketId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
