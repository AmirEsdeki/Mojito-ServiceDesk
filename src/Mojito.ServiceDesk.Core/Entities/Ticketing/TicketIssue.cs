using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using Mojito.ServiceDesk.Core.Entities.Identity;
using System.Collections.Generic;

namespace Mojito.ServiceDesk.Core.Entities.Ticketing
{
    public class TicketIssue : BaseEntity
    {
        public string Title { get; set; }

        //some ticket issues are just for the application owner company
        //like income stuff, vacation appeal and etc.
        public bool IsIntraOrganizational { get; set; }

        #region relations
        public int? CustomerOrganizationId { get; set; }
        public virtual CustomerOrganization CustomerOrganization { get; set; }

        public virtual ICollection<Ticket> Tickets { get; set; }
        #endregion
    }
}
