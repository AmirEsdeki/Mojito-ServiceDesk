using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using Mojito.ServiceDesk.Core.Entities.Identity;
using System.Collections.Generic;

namespace Mojito.ServiceDesk.Core.Entities.Ticketing
{
    public class IssueUrl : BaseEntity
    {
        public string Url { get; set; }

        #region relations
        public int GroupId { get; set; }

        public Group Group { get; set; }

        public ICollection<UserIssueUrl> Users { get; set; }
        #endregion
    }
}
