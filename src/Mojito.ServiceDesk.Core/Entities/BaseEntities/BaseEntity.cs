using Mojito.ServiceDesk.Core.Common.Interfaces;
using System;

namespace Mojito.ServiceDesk.Core.Entities.BaseEntities
{
    public abstract class BaseEntity : IBaseEntity
    {
        public int Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

        public bool IsDeleted { get; set; }

        #region relations
        public Guid? LastModifiedById { get; set; }

        public Guid? CreatedById { get; set; }
        #endregion
    }

    public abstract class BaseEntityWithLongId : IBaseEntityWithLongId
    {
        public long Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

        public bool IsDeleted { get; set; }

        #region relations
        public Guid? LastModifiedById { get; set; }

        public Guid? CreatedById { get; set; }
        #endregion
    }

    public abstract class BaseEntityWithGuidId : IBaseEntityWithGuidId
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

        public bool IsDeleted { get; set; }

        #region relations
        public Guid? LastModifiedById { get; set; }

        public Guid? CreatedById { get; set; }
        #endregion
    }
}
