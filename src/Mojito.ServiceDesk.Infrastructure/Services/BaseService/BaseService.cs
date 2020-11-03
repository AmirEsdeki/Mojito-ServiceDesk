using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.BaseService;
using Mojito.ServiceDesk.Core.Common.Interfaces;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.BaseService
{
    public abstract class BaseService<Entity, TDTOIn, TDTOOut> : IBaseService<Entity, TDTOIn, TDTOOut>
        where Entity : class, IBaseEntity
        where TDTOIn : class, IBaseDTOIn
        where TDTOOut : class, IBaseDTOOut
    {

        #region Ctor
        protected readonly ApplicationDBContext db;

        protected readonly IMapper mapper;

        public BaseService(ApplicationDBContext db, IMapper mapper)
        {
            this.db = db;
            this.mapper = mapper;
        }
        #endregion
        public virtual async Task<TDTOOut> CreateAsync(TDTOIn entity)
        {
            var mappedEntity = mapper.Map<Entity>(entity);
            var t = db.Set<Entity>().Add(mappedEntity);
            await db.SaveChangesAsync();
            var createdEntity = mapper.Map<TDTOOut>(t.Entity);
            return createdEntity;
        }

        public virtual async Task DeleteAsync(int Id, bool isHard = false)
        {
            var entity = db.Set<Entity>().Where(x => x.Id == Id).FirstOrDefault();
            if (entity != null)
            {
                if (isHard)
                {
                    db.Set<Entity>().Remove(entity);
                }
                else
                {
                    entity.IsDeleted = true;
                }
            }
            await db.SaveChangesAsync();
        }

        public virtual async Task<IEnumerable<TDTOOut>> GetAllAsync(Expression<Func<Entity, bool>> predicate)
        {
            return await Task.Run(() =>
            {
                var entities = db.Set<Entity>().Where(predicate);
                return entities.ProjectTo<TDTOOut>(mapper.ConfigurationProvider);
            });
        }

        public virtual async Task<TDTOOut> GetAsync(int id)
        {
            try
            {
                var entity = await db.Set<Entity>().FirstAsync(f => f.Id == id);
                var dto = mapper.Map<TDTOOut>(entity);
                return dto;
            }
            catch (Exception ex)
            {
                throw new EntityDoesNotExistException(ex);
            }
        }

        public virtual async Task UpdateAsync(int id, TDTOIn entity)
        {
            try
            {
                var mappedEntity = mapper.Map<Entity>(entity);
                var entityInDb = await db.Set<Entity>().FirstAsync(f => f.Id == id);
                db.Entry(entityInDb).CurrentValues.SetValues(mappedEntity);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new EntityDoesNotExistException(ex);
            }
        }
    }
}
