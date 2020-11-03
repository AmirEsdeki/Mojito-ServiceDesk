using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using Mojito.ServiceDesk.Core.Entities.Identity;
using System.Collections.Generic;

namespace Mojito.ServiceDesk.Core.Entities.Ticketing
{
    public class Ticket : BaseEntity
    {
        new public long Id { get; set; }

        public string Title { get; set; }

        #region relations
        public int? OpenedById { get; set; }
        public User OpenedBy { get; set; }

        public int? AssigneeId { get; set; }
        public User Assignee { get; set; }

        public int? GroupId { get; set; }
        public Group Group { get; set; }

        public int? ClosedById { get; set; }
        public User ClosedBy { get; set; }

        public int? IssueUrlId { get; set; }
        public IssueUrl IssueUrl { get; set; }

        public int? TicketStatusId { get; set; }
        public TicketStatus TicketStatus { get; set; }

        public int? PriorityId { get; set; }
        public Priority Priority { get; set; }

        public ICollection<Conversation> Conversations { get; set; }
        #endregion

    }
}
