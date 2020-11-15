using Mojito.ServiceDesk.Core.Entities.BaseEntities;

namespace Mojito.ServiceDesk.Core.Entities.Ticketing
{
    public class TicketManagingPipeline : BaseEntity
    {
        #region MyRegion
        public int CustomerOrganizationId { get; set; }

        public int TicketIssueId { get; set; }

        public int NomineeGroupId { get; set; }

        public bool SetToNomineeGroupBasedOnIssueUrl { get; set; }

        public bool SetToNomineePersonBasedOnIssueUrl { get; set; }

        public int Step { get; set; }
        #endregion
    }
}
