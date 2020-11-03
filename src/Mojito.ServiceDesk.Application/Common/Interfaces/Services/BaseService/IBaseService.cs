using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;
using Mojito.ServiceDesk.Core.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.BaseService
{
    public interface IBaseService<Entity, TDTOIn, TDTOOut>
        where Entity : class, IBaseEntity
        where TDTOIn : class, IBaseDTOIn
        where TDTOOut : class, IBaseDTOOut
    {
        Task<TDTOOut> GetAsync(int id);

        Task<IEnumerable<TDTOOut>> GetAllAsync(Expression<Func<Entity, bool>> predicate);

        Task<TDTOOut> CreateAsync(TDTOIn entity);

        Task UpdateAsync(int id, TDTOIn entity);

        Task DeleteAsync(int Id, bool isHard);
    }
}
