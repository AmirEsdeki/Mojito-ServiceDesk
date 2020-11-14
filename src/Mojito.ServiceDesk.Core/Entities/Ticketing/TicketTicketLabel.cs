using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using System;

namespace Mojito.ServiceDesk.Core.Entities.Ticketing
{
    public class TicketTicketLabel : BaseEntity
    {
        public int TicketLabelId { get; set; }

        public virtual TicketLabel TicketLabel { get; set; }

        public Guid TicketId { get; set; }

        public virtual Ticket Ticket { get; set; }
    }
}
