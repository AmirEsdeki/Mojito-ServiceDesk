using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Post.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Post.Out;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.BaseService;
using Mojito.ServiceDesk.Core.Entities.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.PostService
{
    public interface IPostService : IBaseService<Post, PostPostDTO, PutPostDTO, GetPostDTO, PostsFilterParams>
    {
        Task<ICollection<KeyValueDTO>> FilterAsync(string phrase = "");
    }
}
