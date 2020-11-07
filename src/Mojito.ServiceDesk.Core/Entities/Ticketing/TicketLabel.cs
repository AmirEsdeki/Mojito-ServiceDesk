using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using Mojito.ServiceDesk.Core.Entities.Identity;
using System.Collections.Generic;

namespace Mojito.ServiceDesk.Core.Entities.Ticketing
{
    public class TicketLabel : BaseEntity
    {
        public string Title { get; set; }

        #region relations
        public int? CustomerOrganizationId { get; set; }
        public virtual CustomerOrganization CustomerOrganization { get; set; }

        public virtual ICollection<TicketTicketLabel> Tickets { get; set; }
        #endregion
    }
}
