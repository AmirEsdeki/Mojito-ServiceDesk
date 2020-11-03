using Mojito.ServiceDesk.Core.Entities.BaseEntities;

namespace Mojito.ServiceDesk.Core.Entities.Ticketing
{
    public class TicketTicketLabel : BaseEntity
    {
        public int TicketLabelId { get; set; }

        public TicketLabel TicketLabel { get; set; }

        public long TicketId { get; set; }

        public Ticket Ticket { get; set; }
    }
}
