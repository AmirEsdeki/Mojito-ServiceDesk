using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.TicketStatus.In;
using Mojito.ServiceDesk.Application.Common.DTOs.TicketStatus.Out;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.TicketStatusService;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using Mojito.ServiceDesk.Infrastructure.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.TicketStatusService
{
    public class TicketStatusService
        : BaseService<TicketStatus, PostTicketStatusDTO, PutTicketStatusDTO, GetTicketStatusDTO, TicketStatusFilterParams>, ITicketStatusService
    {
        #region ctor
        public TicketStatusService(ApplicationDBContext db, IMapper mapper)
            : base(db, mapper)
        { }
        #endregion

        #region CRUD
        public override async Task<PaginatedList<GetTicketStatusDTO>> GetAllAsync(TicketStatusFilterParams arg)
        {
            try
            {
                var query = GetAllAsync();

                if (arg.Title != null)
                    query = query.Where(data => data.Title.StartsWith(arg.Title)
                        || data.Title.Contains(arg.Title));

                var list = await new PaginatedListBuilder<TicketStatus, GetTicketStatusDTO>(mapper)
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
