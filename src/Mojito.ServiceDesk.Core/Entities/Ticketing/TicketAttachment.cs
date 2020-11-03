using Mojito.ServiceDesk.Core.Entities.BaseEntities;

namespace Mojito.ServiceDesk.Core.Entities.Ticketing
{
    public class TicketAttachment : BaseEntity
    {
        new public long Id { get; set; }
        public string Location { get; set; }

        #region relation
        public int ConversationId { get; set; }
        public Conversation Conversation  { get; set; }
        #endregion
    }
}
