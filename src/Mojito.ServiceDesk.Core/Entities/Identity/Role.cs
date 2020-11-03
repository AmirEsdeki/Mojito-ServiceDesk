using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using System.Collections.Generic;

namespace Mojito.ServiceDesk.Core.Entities.Identity
{
    public class Role : BaseEntity
    {
        public string RoleTitle { get; set; }

        #region relations
        public ICollection<User> Users { get; set; }
        #endregion
    }
}
