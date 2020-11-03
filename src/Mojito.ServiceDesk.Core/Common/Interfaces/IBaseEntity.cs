using System;

namespace Mojito.ServiceDesk.Core.Common.Interfaces
{
    public interface IBaseEntity
    {
        int Id { get; set; }

        public DateTime Created { get; set; }

        public string CreatedById { get; set; }

        public DateTime? LastModified { get; set; }

        public string LastModifiedById { get; set; }

        bool IsDeleted { get; set; }
    }
}
