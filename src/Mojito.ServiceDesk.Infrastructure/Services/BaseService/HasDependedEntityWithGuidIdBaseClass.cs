using Microsoft.EntityFrameworkCore;
using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Application.Common.Extensions;
using Mojito.ServiceDesk.Core.Common.Interfaces;
using Mojito.ServiceDesk.Core.Entities.Identity;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using System;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.BaseService
{
    public abstract class HasDependedEntityWithGuidIdBaseClass<Entity> where Entity : class, IBaseEntityWithGuidId
    {
        private readonly ApplicationDBContext db;

        public HasDependedEntityWithGuidIdBaseClass(ApplicationDBContext db)
        {
            this.db = db;
        }

        protected async Task<Entity> ReturnParentEntityIfBothExistsElseThrow<TChild>(Guid parentId, int childId)
                where TChild : class, IBaseEntity
        {
            var parent = await db.Set<Entity>().FirstOrDefaultAsync(f => f.Id == parentId);

            if (parent == null)
                throw new EntityNotFoundException();

            if (!await db.Set<TChild>().AnyAsync(a => a.Id == childId))
                throw new EntityNotFoundException();

            return parent;
        }

        protected async Task<Entity> ReturnParentEntityIfParentAndUserBothExistsElseThrow(Guid parentId, string userId)
        {
            var parent = await db.Set<Entity>().FirstOrDefaultAsync(f => f.Id == parentId);

            if (parent == null)
                throw new EntityNotFoundException();

            if (!await db.Set<User>().AnyAsync(a => a.Id == userId))
                throw new EntityNotFoundException();

            return parent;
        }

        protected async Task<Entity> ReturnParentEntityIfBothExistsElseNull<TChild>(Guid parentId, int childId)
            where TChild : class, IBaseEntity
        {
            var parent = await db.Set<Entity>().FirstOrDefaultAsync(f => f.Id == parentId);

            if (parent == null)
                return null;

            if (!await db.Set<TChild>().AnyAsync(a => a.Id == childId))
                return null;

            return parent;
        }

        protected async Task<Entity> ReturnParentEntityIfParentAndUserBothExistsElseNull(Guid parentId, string userId)
        {
            var parent = await db.Set<Entity>().FirstOrDefaultAsync(f => f.Id == parentId);

            if (parent == null)
                return null;

            if (!await db.Set<User>().AnyAsync(a => a.Id == userId))
                return null;

            return parent;
        }
    }
}
