using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Conversation.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Conversation.Out;
using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.ConversationService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService;
using Mojito.ServiceDesk.Core.Constant;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.ConversationService
{
    public class ConversationService : IConversationService
    {
        #region ctor
        private readonly ApplicationDBContext db;
        private readonly IAppUser appUser;
        private readonly IMapper mapper;

        public ConversationService(ApplicationDBContext db,
            IAppUser appUser, IMapper mapper)
        {
            this.db = db;
            this.appUser = appUser;
            this.mapper = mapper;
        }
        #endregion

        #region CRUD
        public async Task<PaginatedList<GetConversationDTO>> GetAllAsync(ConversationsFilterParams arg)
        {
            try
            {
                var query = db.Conversations.Where(w => w.TicketId == arg.TicketId);


                //to restrict the query access to just admins or those 
                //who are some how assigned to this conversation or are the owner of the ticket
                if (!appUser.Roles.Any(a => a == Roles. Admin || a == Roles.Observer))
                {
                    if(appUser.Roles.Any(a => a == Roles.Employee))
                        query = query.Where(w =>
                            w.CreatedById.ToString() == appUser.Id
                            || appUser.Groups.Any(a => a == w.Ticket.NomineeGroupId)
                            || w.Ticket.AssigneeId == appUser.Id
                            || w.Ticket.OpenedById == appUser.Id
                            || w.Ticket.ClosedById == appUser.Id);

                    else if (appUser.Roles.Any(a => a == Roles.User))
                        query = query.Where(w => w.Ticket.OpenedById == appUser.Id);
                }

                //if user is not an employee of the company only can see the public posts
                if (!appUser.IsCompanyMember)
                    query = query.Where(w => w.IsPublic);

                if (arg.Title != null)
                    query = query.Where(data => data.Message.StartsWith(arg.Title)
                        || data.Message.Contains(arg.Title));

                if (arg.FromDate != null)
                    query = query.Where(w => w.Created >= arg.FromDate);

                if (arg.ToDate != null)
                    query = query.Where(w => w.Created <= arg.ToDate);

                //sort by date of the ticket so newer ones come first.
                query = query.OrderByDescending(o => o.Created);

                var list = await new PaginatedListBuilder<Conversation, GetConversationDTO>(mapper)
                    .CreateAsync(query, arg.PageNumber, arg.PageSize);

                list.Items.ForEach(async f => f.FullName =
                    (await db.Users.FirstOrDefaultAsync(u => u.Id == f.CreatedById.ToString())).FullName
                );

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<GetConversationDTO> CreateAsync(PostConversationDTO entity)
        {
            var mappedEntity = mapper.Map<Conversation>(entity);

            var addedEntity = db.Conversations.Add(mappedEntity);
            await db.SaveChangesAsync();

            var dto = mapper.Map<GetConversationDTO>(addedEntity.Entity);

            return dto;
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await db.Conversations.FirstOrDefaultAsync(x => x.Id == Guid.Parse(id));

            if (entity != null)
            {
                // if the person trying to delete the conversation is not admin we check if 
                // he/she is the creator of the entity if not he/she has not the permission to delete 
                //anotherone's conversation
                if (!appUser.Roles.Any(a => a == "admin"))
                {
                    if (entity.CreatedById.ToString() != appUser.Id)
                        throw new UnauthorizedException();
                }

                //if user is admin it is ok 
                //if is not admin but is creator of the entity again it is ok
                entity.IsDeleted = true;

                await db.SaveChangesAsync();
            }
        }
        #endregion

    }
}
