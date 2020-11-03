using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using Mojito.ServiceDesk.Core.Entities.Identity;

namespace Mojito.ServiceDesk.Core.Entities.Ticketing
{
    public class UserIssueUrl : BaseEntity
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public int IssueUrlId { get; set; }

        public IssueUrl IssueUrl { get; set; }
    }
}
