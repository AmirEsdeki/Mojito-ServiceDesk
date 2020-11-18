using Microsoft.EntityFrameworkCore;
using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Core.Common.Interfaces;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
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

        protected async Task<Entity> ReturnParentEntityIfBothExistsElseThrow<TChild>(string parentId, int childId)
                where TChild : class, IBaseEntity
        {
            var parent = await db.Set<Entity>().FirstOrDefaultAsync(f => f.Id == parentId);

            if (parent == null)
                throw new EntityNotFoundException();

            if (!await db.Set<TChild>().AnyAsync(a => a.Id == childId))
                throw new EntityNotFoundException();

            return parent;
        }

        protected async Task<Entity> ReturnParentEntityIfBothExistsElseNull<TChild>(string parentId, int childId)
            where TChild : class, IBaseEntity
        {
            var parent = await db.Set<Entity>().FirstOrDefaultAsync(f => f.Id == parentId);

            if (parent == null)
                return null;

            if (!await db.Set<TChild>().AnyAsync(a => a.Id == childId))
                return null;

            return parent;
        }
    }
}
