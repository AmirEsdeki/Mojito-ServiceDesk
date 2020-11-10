using Mojito.ServiceDesk.Application.Common.DTOs.Base.In;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Post.In
{
    public class PostsFilterParams : PaginationParamsDTO, IBaseDTOFilter
    {

        public string Title { get; set; }
    }
}
