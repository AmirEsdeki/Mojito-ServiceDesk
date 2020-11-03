using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using System.Collections.Generic;

namespace Mojito.ServiceDesk.Core.Entities.Identity
{
    public class Group : BaseEntity
    {
        public string Name { get; set; }

        #region relations
        public int GroupTypeId { get; set; }

        public GroupType groupType { get; set; }

        public ICollection<UserGroup> Users { get; set; }

        public ICollection<IssueUrl> IssueUrls { get; set; }
        #endregion
    }
}
