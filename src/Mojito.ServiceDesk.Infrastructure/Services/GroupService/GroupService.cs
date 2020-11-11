using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Group.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Group.Out;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.GroupService;
using Mojito.ServiceDesk.Core.Entities.Identity;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using Mojito.ServiceDesk.Infrastructure.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.GroupService
{
    public class GroupService
         : BaseService<Group, PostGroupDTO, PutGroupDTO, GetGroupDTO, GroupsFilterParams>
        , IGroupService
    {
        #region ctor
        public GroupService(ApplicationDBContext db, IMapper mapper)
            : base(db, mapper)
        { }
        #endregion

        #region CRUD
        public override async Task<PaginatedList<GetGroupDTO>> GetAllAsync(GroupsFilterParams arg)
        {
            try
            {
                var query = GetAllAsync();

                if (arg.Name != null)
                    query = query.Where(data => data.Name.StartsWith(arg.Name)
                        || data.Name.Contains(arg.Name));

                if (arg.GroupTypeId != 0)
                    query = query.Where(data => data.GroupTypeId == arg.GroupTypeId);

                var list = await new PaginatedListBuilder<Group, GetGroupDTO>(mapper)
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
            var filteredData = await GetAllAsync(data => data.Name.StartsWith(phrase)
                || data.Name.Contains(phrase))
                .ToListAsync();
            return filteredData.Select(s => new KeyValueDTO(s.Id, s.Name)).ToList();
        }
        #endregion
    }
}
