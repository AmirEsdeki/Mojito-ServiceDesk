using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Priority.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Priority.Out;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.PriorityService;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using Mojito.ServiceDesk.Infrastructure.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.PriorityService
{
    public class PriorityService
        : BaseService<Priority, PostPriorityDTO, PutPriorityDTO, GetPriorityDTO, PrioritiesFilterParams>, IPriorityService
    {
        #region ctor
        public PriorityService(ApplicationDBContext db, IMapper mapper)
            : base(db, mapper)
        { }
        #endregion

        #region CRUD
        public override async Task<PaginatedList<GetPriorityDTO>> GetAllAsync(PrioritiesFilterParams arg)
        {
            try
            {
                var query = GetAllAsync();

                if (arg.Title != null)
                    query = query.Where(data => data.Title.StartsWith(arg.Title)
                        || data.Title.Contains(arg.Title));

                var list = await new PaginatedListBuilder<Priority, GetPriorityDTO>(mapper)
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
