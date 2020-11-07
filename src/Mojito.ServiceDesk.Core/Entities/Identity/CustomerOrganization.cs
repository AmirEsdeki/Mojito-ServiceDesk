using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using Mojito.ServiceDesk.Core.Entities.Proprietary;
using System.Collections.Generic;

namespace Mojito.ServiceDesk.Core.Entities.Identity
{
    public class CustomerOrganization : BaseEntity
    {
        public string Name { get; set; }

        #region relation
        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        #endregion

    }
}
