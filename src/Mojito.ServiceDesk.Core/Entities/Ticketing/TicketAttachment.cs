using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using System;

namespace Mojito.ServiceDesk.Core.Entities.Ticketing
{
    public class TicketAttachment : BaseEntity
    {
        new public long Id { get; set; }
        public string Location { get; set; }

        #region relation
        public Guid ConversationId { get; set; }
        public virtual Conversation Conversation  { get; set; }
        #endregion
    }
}
