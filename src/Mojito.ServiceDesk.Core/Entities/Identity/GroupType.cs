using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using System.Collections.Generic;

namespace Mojito.ServiceDesk.Core.Entities.Identity
{
    public class GroupType : BaseEntity
    {
        public string Title { get; set; }

        #region relations
        public ICollection<Group> Groups { get; set; }
        #endregion
    }
}
