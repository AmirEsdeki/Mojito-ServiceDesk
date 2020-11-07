using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using System.Text.RegularExpressions;

namespace Mojito.ServiceDesk.Core.Entities.Ticketing
{
    public class TicketManagingPipeline : BaseEntity
    {
        #region MyRegion
        public int TicketIssueId { get; set; }
        public TicketIssue TicketIssue { get; set; }

        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        #endregion
    }
}
