using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using Mojito.ServiceDesk.Core.Entities.Identity;

namespace Mojito.ServiceDesk.Core.Entities.Proprietary
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        #region relations
        public int CustomerId { get; set; }
        public CustomerOrganization Customer { get; set; }
        #endregion
    }
}
