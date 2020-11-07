using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using System.Collections.Generic;

namespace Mojito.ServiceDesk.Core.Entities.Ticketing
{
    public class TicketIssue : BaseEntity
    {
        public string Title { get; set; }

        #region relations
        public virtual ICollection<Ticket> Tickets { get; set; }
        #endregion
    }
}
