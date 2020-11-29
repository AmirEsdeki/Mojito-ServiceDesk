using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.TicketLabel.In;
using Mojito.ServiceDesk.Application.Common.DTOs.TicketLabel.Out;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.TicketLabelService;
using Mojito.ServiceDesk.Core.Constant;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using Mojito.ServiceDesk.Infrastructure.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.TicketLabelService
{
    public class TicketLabelService
        : BaseService<TicketLabel, PostTicketLabelDTO, PutTicketLabelDTO, GetTicketLabelDTO, TicketLabelsFilterParams>, ITicketLabelService
    {
        private readonly IAppUser appUser;
        #region ctor
        public TicketLabelService(ApplicationDBContext db, IAppUser appUser, IMapper mapper)
            : base(db, mapper)
        {
            this.appUser = appUser;
        }
        #endregion

        #region CRUD
        public override async Task<PaginatedList<GetTicketLabelDTO>> GetAllAsync(TicketLabelsFilterParams arg)
        {
            try
            {
                var userRoles = appUser.Roles;

                var query = GetAllAsync();

                if (arg.Title != null)
                    query = query.Where(data => data.Title.StartsWith(arg.Title)
                        || data.Title.Contains(arg.Title));

                if (userRoles.Any(a => a == Roles.User))
                    query = query.Where(w => w.CustomerOrganizationId == appUser.CustomerOrganizationId);

                    var list = await new PaginatedListBuilder<TicketLabel, GetTicketLabelDTO>(mapper)
                    .CreateAsync(query, arg.PageNumber, arg.PageSize);

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region OtherActions

        public async Task<ICollection<KeyValueDTO>> FilterAsync(string phrase)
        {
            var filteredData = await GetAllAsync(data => data.Title.StartsWith(phrase)
                || data.Title.Contains(phrase))
                .ToListAsync();
            return filteredData.Select(s => new KeyValueDTO(s.Id, s.Title)).ToList();
        }
        #endregion
    }
}
