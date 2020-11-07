using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using System;

namespace Mojito.ServiceDesk.Core.Entities.Identity
{
    public class UserGroup : BaseEntity
    {
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}
