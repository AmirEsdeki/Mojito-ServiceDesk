using AutoMapper;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Post.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Post.Out;
using Mojito.ServiceDesk.Core.Entities.Identity;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using Mojito.ServiceDesk.Infrastructure.Services.BaseService;
using System;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.PostService
{
    public class PostService
        : BaseService<Post, PostPostDTO, PutPostDTO, GetPostDTO, PostsFilterParams>
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
                var query = GetAllAsync(post => post.Title.StartsWith(arg.Title));

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
    }
}
