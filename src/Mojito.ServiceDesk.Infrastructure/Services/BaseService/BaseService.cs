using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.BaseService;
using Mojito.ServiceDesk.Core.Common.Interfaces;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.BaseService
{
    public abstract class BaseService<Entity, TDTOPost, TDTOPut, TDTOGet, TDTOFilter>
        : IBaseService<Entity, TDTOPost, TDTOPut, TDTOGet, TDTOFilter>
        where Entity : class, IBaseEntity
        where TDTOPost : class, IBaseDTOPost
        where TDTOPut : class, IBaseDTOPut
        where TDTOGet : class, IBaseDTOGet
        where TDTOFilter : class, IBaseDTOFilter
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


        #region PublicMembers
        public virtual async Task<PaginatedList<TDTOGet>> GetAllAsync(TDTOFilter arg)
        {
            try
            {
                var query = GetAllAsync();

                var list = await new PaginatedListBuilder<Entity, TDTOGet>(mapper)
                    .CreateAsync(query, arg.PageNumber, arg.PageSize);

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<TDTOGet> GetAsync(int id)
        {
            try
            {
                var entity = await db.Set<Entity>().FirstOrDefaultAsync(f => f.Id == id);

                if (entity == null)
                    throw new EntityNotFoundException();

                var dto = mapper.Map<TDTOGet>(entity);

                return dto;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public virtual async Task<TDTOGet> CreateAsync(TDTOPost entity)
        {
            var mappedEntity = mapper.Map<Entity>(entity);

            var addedEntity = db.Set<Entity>().Add(mappedEntity);
            await db.SaveChangesAsync();

            var dto = mapper.Map<TDTOGet>(addedEntity.Entity);

            return dto;
        }

        public virtual async Task DeleteAsync(int id, bool isHard = false)
        {
            var entity = await db.Set<Entity>().FirstOrDefaultAsync(x => x.Id == id);
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

                await db.SaveChangesAsync();
            }
        }

        public virtual async Task UpdateAsync(int id, TDTOPut entity)
        {
            try
            {
                var entityInDb = await db.Set<Entity>().FirstOrDefaultAsync(f => f.Id == id);

                if (entityInDb == null)
                    throw new EntityNotFoundException();

                var mappedEntity = mapper.Map(entity, entityInDb);

                db.Update(entityInDb);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion


        #region ProtectedMembers

        protected virtual IQueryable<Entity> GetAllAsync(Expression<Func<Entity, bool>> predicate = null)
        {
            var entities = predicate is null ? db.Set<Entity>() : db.Set<Entity>().Where(predicate);
            return entities;
        }

        protected async Task<Entity> ReturnParentEntityIfBothExistsElseThrow<TChild>(int parentId, int childId)
            where TChild : class, IBaseEntity
        {
            var parent = await db.Set<Entity>().FirstOrDefaultAsync(f => f.Id == parentId);

            if (parent == null)
                throw new EntityNotFoundException();

            if (!await db.Set<Entity>().AnyAsync(a => a.Id == childId))
                throw new EntityNotFoundException();

            return parent;
        }

        protected async Task<Entity> ReturnParentEntityIfBothExistsElseThrow<TChild>(int parentId, string childId)
            where TChild : class, IBaseEntityWithGuid
        {
            var parent = await db.Set<Entity>().FirstOrDefaultAsync(f => f.Id == parentId);

            if (parent == null)
                throw new EntityNotFoundException();

            if (!await db.Set<TChild>().AnyAsync(a => a.Id == childId))
                throw new EntityNotFoundException();

            return parent;
        }

        protected async Task<Entity> ReturnParentEntityIfBothExistsElseNull<TChild>(int parentId, int childId)
            where TChild : class, IBaseEntity
        {
            var parent = await db.Set<Entity>().FirstOrDefaultAsync(f => f.Id == parentId);

            if (parent == null)
                return null;

            if (!await db.Set<TChild>().AnyAsync(a => a.Id == childId))
                return null;

            return parent;
        }

        protected async Task<Entity> ReturnParentEntityIfBothExistsElseNull<TChild>(int parentId, string childId)
           where TChild : class, IBaseEntityWithGuid
        {
            var parent = await db.Set<Entity>().FirstOrDefaultAsync(f => f.Id == parentId);

            if (parent == null)
                return null;

            if (!await db.Set<TChild>().AnyAsync(a => a.Id == childId))
                return null;

            return parent;
        }
        #endregion
    }
}
