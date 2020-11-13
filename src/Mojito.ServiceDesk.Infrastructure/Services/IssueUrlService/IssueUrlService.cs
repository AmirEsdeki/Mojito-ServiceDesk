using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.IssueUrl.In;
using Mojito.ServiceDesk.Application.Common.DTOs.IssueUrl.Out;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.IssueUrlService;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using Mojito.ServiceDesk.Infrastructure.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.IssueUrlService
{
    public class IssueUrlService
         : BaseService<IssueUrl, PostIssueUrlDTO, PutIssueUrlDTO, GetIssueUrlDTO, IssueUrlsFilterParams>
        , IIssueUrlService
    {
        #region ctor
        public IssueUrlService(ApplicationDBContext db, IMapper mapper)
            : base(db, mapper)
        { }
        #endregion

        #region CRUD
        public override async Task<PaginatedList<GetIssueUrlDTO>> GetAllAsync(IssueUrlsFilterParams arg)
        {
            try
            {
                var query = GetAllAsync();

                if (arg.Url != null)
                    query = query.Where(data => data.Url.StartsWith(arg.Url)
                        || data.Url.Contains(arg.Url));

                if (arg.NameOfUser != null)
                    query = query.Where(data => data.Users.Any(user => user.User.FullName.Contains(arg.NameOfUser)));

                if (arg.NameOfGroup != null)
                    query = query.Where(data => data.Group.Name.Contains(arg.NameOfGroup));

                var list = await new PaginatedListBuilder<IssueUrl, GetIssueUrlDTO>(mapper)
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
            var filteredData = await GetAllAsync(data => data.Url.StartsWith(phrase)
                || data.Url.Contains(phrase))
                .ToListAsync();
            return filteredData.Select(s => new KeyValueDTO(s.Id, s.Url)).ToList();
        }
        #endregion
    }
}
