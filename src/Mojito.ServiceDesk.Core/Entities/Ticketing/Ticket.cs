using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using Mojito.ServiceDesk.Core.Entities.Identity;
using System;
using System.Collections.Generic;

namespace Mojito.ServiceDesk.Core.Entities.Ticketing
{
    public class Ticket : BaseEntityWithGuidId
    {
        public string Identifier { get; set; }

        public string Title { get; set; }

        public bool IsClosed { get; set; }

        public int? CustomerOrganizationId { get; set; }

        #region TicketPipelines
        public int CurrentStep { get; set; }

        public int MaximumSteps { get; set; }

        public bool HasNextStep => CurrentStep != MaximumSteps;
        #endregion

        #region relations
        public string OpenedById { get; set; }
        public virtual User OpenedBy { get; set; }

        public string AssigneeId { get; set; }
        public virtual User Assignee { get; set; }

        public int? NomineeGroupId { get; set; }
        public virtual Group NomineeGroup { get; set; }

        public string ClosedById { get; set; }
        public virtual User ClosedBy { get; set; }

        public int? IssueUrlId { get; set; }
        public virtual IssueUrl IssueUrl { get; set; }

        public int? TicketStatusId { get; set; }
        public virtual TicketStatus TicketStatus { get; set; }

        public int? TicketIssueId { get; set; }
        public virtual TicketIssue TicketIssue { get; set; }

        public int? PriorityId { get; set; }
        public virtual Priority Priority { get; set; }

        public virtual ICollection<Conversation> Conversations { get; set; }

        public virtual ICollection<TicketTicketLabel> TicketLabels { get; set; }

        #endregion

    }
}
