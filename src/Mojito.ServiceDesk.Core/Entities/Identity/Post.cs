using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using System.Collections.Generic;

namespace Mojito.ServiceDesk.Core.Entities.Identity
{
    public class Post : BaseEntity
    {
        public string Title { get; set; }

        #region relations
        public virtual ICollection<User> Users { get; set; }
        #endregion
    }
}
