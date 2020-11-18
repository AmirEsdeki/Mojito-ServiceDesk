using System;

namespace Mojito.ServiceDesk.Core.Common.Interfaces
{
    //these properties are same between all entities.
    //some prereserved types like Identity also inherit this interface directly, user entity cannot inherit the class that implements this interface.
    //Id property has different types accross entities so it has seprated from this Interface;
    //with this approach changes in dbcontext will be done by refering to this interface.
    public interface ICoreBaseEntity
    {
        public DateTime Created { get; set; }

        public Guid? CreatedById { get; set; }

        public DateTime? LastModified { get; set; }

        public Guid? LastModifiedById { get; set; }

        bool IsDeleted { get; set; }
    }
}
