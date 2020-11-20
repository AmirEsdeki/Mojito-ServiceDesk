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
using Mojito.ServiceDesk.Core.Entities.Identity;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using Mojito.ServiceDesk.Infrastructure.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.TicketService
{
    public class TicketService : HasDependedEntityWithGuidIdBaseClass<Ticket>, ITicketService
    {
        #region ctor
        private readonly ApplicationDBContext db;
        private readonly IAppUser appUser;
        private readonly IMapper mapper;

        public TicketService(ApplicationDBContext db,
            IAppUser appUser, IMapper mapper)
            : base(db)
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
                    query = db.Tickets.Where(w => w.AssigneeId == appUser.Id.ToString());

                else if (arg.OnlyOpenedByUser)
                    query = db.Tickets.Where(w => w.OpenedById == appUser.Id.ToString());

                else if (arg.OnlyClosedByUser)
                    query = db.Tickets.Where(w => w.ClosedById == appUser.Id.ToString());

                else if (arg.OnlyIfUserHasParticipatedInConversation)
                    query = db.Tickets.Where(w => w.Conversations.Any(a => a.CreatedById == appUser.Id));

                //if none of above is true it means that the user want to see all of the tickets
                //based on the role of the user the result is different
                else
                {
                    //admin or observer can see all of the tickets in the system
                    query = db.Tickets.AsQueryable();

                    if (userRoles.Any(a => a == Roles.Admin || a == Roles.Observer))
                    {
                        //admin or observer can see all.
                    }

                    else if (userRoles.Any(a => a == Roles.Employee))
                    {
                        //employee can see all of the tickets that he has somehow participated in
                        query = FilterQueryForUsersThatHaveParticipatedInTheTicket(appUser, query);
                    }

                    else
                    {
                        //users can only see the tickets that has opened by them
                        query = FilterQueryForUsersThatHaveCreatedTheTicket(appUser, query);
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

        public async Task<GetTicketDTO> GetAsync(Guid ticketId)
        {
            try
            {
                var userRoles = appUser.Roles;

                Ticket entity;
                var query = db.Tickets.AsQueryable<Ticket>();

                if (userRoles.Any(a => a == Roles.Admin || a == Roles.Observer))
                {
                    //admin or observer can see all of the tickets in the system
                    entity = await query.FirstOrDefaultAsync(f => f.Id == ticketId);
                }

                else if (userRoles.Any(a => a == Roles.Employee))
                {
                    //employee can see all of the tickets that he has somehow participated in
                    query = FilterQueryForUsersThatHaveParticipatedInTheTicket(appUser, query);
                    entity = await query.FirstOrDefaultAsync(f => f.Id == ticketId);
                }
                else
                {
                    //users can only see the tickets that has opened by them.
                    query = FilterQueryForUsersThatHaveCreatedTheTicket(appUser, query);
                    entity = await query.FirstOrDefaultAsync(f => f.Id == ticketId);
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

            ticket.OpenedById = appUser.Id.ToString();

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
                                ticket.AssigneeId = issueUrl.Users.FirstOrDefault().UserId;
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
                // but other roles can make their massage public or private 
                //(private means it is just visible to employees and not customers)
                var isMessagePublic = appUser.Roles.Any(a => a == Roles.User) ?
                    true :
                    entity.IsMessagePublic;

                db.Conversations.Add(
                    new Conversation
                    {
                        Message = entity.Message,
                        IsPublic = isMessagePublic,
                        TicketId = ticketId.ToString()
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

        public async Task DeleteAsync(Guid ticketId)
        {
            var ticket = await db.Tickets.FirstOrDefaultAsync(x => x.Id == ticketId);

            //admins can delete any ticket
            //others just can delete a ticket if the ticket is opened by them
            ThrowIfUserIsNotTheCreatorOfTheTicket(appUser, ticket);

            if (ticket != null)
            {
                ticket.IsDeleted = true;

                await db.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(Guid ticketId, PutTicketDTO ticket)
        {
            try
            {
                var ticketInDb = await db.Tickets.FirstOrDefaultAsync(f => f.Id == ticketId);

                if (ticketInDb == null)
                    throw new EntityNotFoundException();

                //admins can edit any ticket
                //others just can edit a ticket if the ticket is opened by them
                ThrowIfUserIsNotTheCreatorOfTheTicket(appUser, ticketInDb);

                var mappedEntity = mapper.Map(ticket, ticketInDb);

                db.Update(ticketInDb);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region RelationActions
        public async Task AddLabelAsync(Guid ticketId, int labelId)
        {
            try
            {
                var ticket = await ReturnParentEntityIfBothExistsElseThrow<TicketLabel>(ticketId, labelId);

                ThrowIfUserHasNotParticipatedInTicket(appUser, ticket);

                (ticket.TicketLabels ??= new List<TicketTicketLabel>()).Add(
                    new TicketTicketLabel
                    {
                        TicketId = ticketId,
                        TicketLabelId = labelId
                    });

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RemoveLabelAsync(Guid ticketId, int labelId)
        {
            try
            {
                var ticket = await ReturnParentEntityIfBothExistsElseNull<TicketLabel>(ticketId, labelId);

                if (ticket == null)
                    return;

                ThrowIfUserHasNotParticipatedInTicket(appUser, ticket);

                var label = ticket.TicketLabels.FirstOrDefault(w => w.TicketLabelId == labelId);

                if (label != null)
                {
                    ticket.TicketLabels.Remove(label);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task OpenTicketAsync(Guid ticketId)
        {
            try
            {
                var ticket = await db.Tickets.FirstOrDefaultAsync(w => w.Id == ticketId);

                if (ticket == null)
                    throw new EntityNotFoundException();

                ThrowIfUserHasNotParticipatedInTicket(appUser, ticket);

                ticket.IsClosed = false;

                await db.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task CloseTicketAsync(Guid ticketId)
        {
            try
            {
                var ticket = await db.Tickets.FirstOrDefaultAsync(w => w.Id == ticketId);

                if (ticket == null)
                    throw new EntityNotFoundException();

                ThrowIfUserHasNotParticipatedInTicket(appUser, ticket);

                ticket.IsClosed = false;
                ticket.ClosedById = appUser.IdToString;

                await db.SaveChangesAsync();

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task PassToNextNominee(Guid ticketId)
        {
            var ticket = await db.Tickets.FirstOrDefaultAsync(x => x.Id == ticketId);

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
                var pipeline = thisTicketIssuePipeline.FirstOrDefault(f => f.Step == ticket.CurrentStep + 1);//Finding the next step action in pipeline.

                ticket.CurrentStep = ticket.CurrentStep + 1;
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
                                ticket.AssigneeId = issueUrl.Users.FirstOrDefault().UserId;
                        }
                    }

                    await db.SaveChangesAsync();
                }
            }

            #endregion
        }

        public async Task SetAsigneeAsync(Guid ticketId, string userId)
        {
            try
            {
                var ticket = await ReturnParentEntityIfParentAndUserBothExistsElseThrow(ticketId, userId);

                ThrowIfUserHasNotParticipatedInTicket(appUser, ticket);

                ticket.AssigneeId = userId;

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SetIssueUrlAsync(Guid ticketId, int issueUrlId)
        {
            try
            {
                var ticket = await ReturnParentEntityIfBothExistsElseThrow<IssueUrl>(ticketId, issueUrlId);

                ThrowIfUserHasNotParticipatedInTicket(appUser, ticket);

                ticket.IssueUrlId = issueUrlId;

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SetNominatedGroupAsync(Guid ticketId, int groupId)
        {
            try
            {
                var ticket = await ReturnParentEntityIfBothExistsElseThrow<Group>(ticketId, groupId);

                ThrowIfUserHasNotParticipatedInTicket(appUser, ticket);

                ticket.NomineeGroupId = groupId;

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SetPriorityAsync(Guid ticketId, int priorityId)
        {
            try
            {
                var ticket = await ReturnParentEntityIfBothExistsElseThrow<Priority>(ticketId, priorityId);

                ThrowIfUserHasNotParticipatedInTicket(appUser, ticket);

                ticket.PriorityId = priorityId;

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task SetStatusAsync(Guid ticketId, int ticketStatusId)
        {
            try
            {
                var ticket = await ReturnParentEntityIfBothExistsElseThrow<TicketStatus>(ticketId, ticketStatusId);

                ThrowIfUserHasNotParticipatedInTicket(appUser, ticket);

                ticket.TicketStatusId = ticketStatusId;

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region Private
        private void ThrowIfUserHasNotParticipatedInTicket(IAppUser appUser, Ticket ticket)
        {
            if (!appUser.Roles.Any(a => a == Roles.Admin || a == Roles.Observer))
                if (!(ticket.AssigneeId == appUser.IdToString
                    || ticket.OpenedById == appUser.IdToString
                    || ticket.ClosedById == appUser.IdToString
                    || appUser.Groups.Any(a => a == ticket.NomineeGroupId)
                    || ticket.Conversations.Any(a => a.CreatedById == appUser.Id)))
                    throw new UnauthorizedException();
        }

        private void ThrowIfUserIsNotTheCreatorOfTheTicket(IAppUser appUser, Ticket ticket)
        {
            if (!appUser.Roles.Any(a => a == Roles.Admin || a == Roles.Observer))
                if (!(ticket.OpenedById == appUser.IdToString))
                    throw new UnauthorizedException();
        }

        private IQueryable<Ticket> FilterQueryForUsersThatHaveParticipatedInTheTicket(IAppUser appUser, IQueryable<Ticket> query)
        {
            var newQuery = query.Where(w =>
                appUser.Groups.Any(a => a == w.NomineeGroupId)
                || w.AssigneeId == appUser.Id.ToString()
                || w.OpenedById == appUser.Id.ToString()
                || w.ClosedById == appUser.Id.ToString()
                || w.Conversations.Any(a => a.CreatedById == appUser.Id)
                );

            return newQuery;
        }

        private IQueryable<Ticket> FilterQueryForUsersThatHaveCreatedTheTicket(IAppUser appUser, IQueryable<Ticket> query)
        {
            var newQuery = query.Where(w => w.OpenedById == appUser.Id.ToString());

            return newQuery;
        }
        #endregion
    }
}
