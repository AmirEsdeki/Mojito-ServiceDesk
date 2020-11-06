using System;

namespace Mojito.ServiceDesk.Core.Common.Interfaces
{
    public interface IBaseEntity
    {
        int Id { get; set; }

        public DateTime Created { get; set; }

        public Guid? CreatedById { get; set; }

        public DateTime? LastModified { get; set; }

        public Guid? LastModifiedById { get; set; }

        bool IsDeleted { get; set; }
    }
}
