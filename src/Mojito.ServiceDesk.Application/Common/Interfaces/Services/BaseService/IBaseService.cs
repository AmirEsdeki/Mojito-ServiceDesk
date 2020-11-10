using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;
using Mojito.ServiceDesk.Core.Common.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.BaseService
{
    public interface IBaseService<Entity, TDTOPost, TDTOPut, TDTOGet , TDTOFilter>
        where Entity : class, IBaseEntity
        where TDTOPost : class, IBaseDTOPost
        where TDTOPut : class, IBaseDTOPut
        where TDTOGet : class, IBaseDTOGet
        where TDTOFilter : class, IBaseDTOFilter
    {
        Task<TDTOGet> GetAsync(int id);

        Task<PaginatedList<TDTOGet>> GetAllAsync(TDTOFilter arg);

        Task<TDTOGet> CreateAsync(TDTOPost entity);

        Task UpdateAsync(int id, TDTOPut entity);

        Task DeleteAsync(int Id, bool isHard = false);
    }
}
