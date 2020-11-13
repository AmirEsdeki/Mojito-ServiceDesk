using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using Mojito.ServiceDesk.Core.Entities.Identity;
using System.Collections.Generic;

namespace Mojito.ServiceDesk.Core.Entities.Ticketing
{
    public class Ticket : BaseEntity
    {
        new public long Id { get; set; }

        public string Identifier { get; set; }

        public string Title { get; set; }

        public bool IsClosed { get; set; }

        #region relations
        public string OpenedById { get; set; }
        public virtual User OpenedBy { get; set; }

        public string AssigneeId { get; set; }
        public virtual User Assignee { get; set; }

        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }

        public string ClosedById { get; set; }
        public virtual User ClosedBy { get; set; }

        public int? IssueUrlId { get; set; }
        public virtual IssueUrl IssueUrl { get; set; }

        public int? TicketStatusId { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }

        public int? PriorityId { get; set; }
        public virtual Priority Priority { get; set; }

        public virtual ICollection<Conversation> Conversations { get; set; }

        public virtual ICollection<TicketTicketLabel> TicketLabels { get; set; }

        #endregion

    }
}
