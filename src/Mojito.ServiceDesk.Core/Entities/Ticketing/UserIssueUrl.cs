using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using Mojito.ServiceDesk.Core.Entities.Identity;
using System;

namespace Mojito.ServiceDesk.Core.Entities.Ticketing
{
    public class UserIssueUrl : BaseEntity
    {
        public string UserId { get; set; }

        public virtual User User { get; set; }

        public int IssueUrlId { get; set; }

        public virtual IssueUrl IssueUrl { get; set; }
    }
}
