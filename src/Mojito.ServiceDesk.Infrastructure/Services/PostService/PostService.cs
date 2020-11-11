using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Post.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Post.Out;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.PostService;
using Mojito.ServiceDesk.Core.Entities.Identity;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using Mojito.ServiceDesk.Infrastructure.Services.BaseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.PostService
{
    public class PostService
        : BaseService<Post, PostPostDTO, PutPostDTO, GetPostDTO, PostsFilterParams>, IPostService
    {
        #region ctor
        public PostService(ApplicationDBContext db, IMapper mapper)
            : base(db, mapper)
        { }
        #endregion

        #region CRUD
        public override async Task<PaginatedList<GetPostDTO>> GetAllAsync(PostsFilterParams arg)
        {
            try
            {
                arg.Title ??= string.Empty;

                var query = GetAllAsync(data => data.Title.StartsWith(arg.Title) 
                    || data.Title.Contains(arg.Title));

                var list = await new PaginatedListBuilder<Post, GetPostDTO>(mapper)
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
