using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using System.Collections.Generic;

namespace Mojito.ServiceDesk.Core.Entities.Ticketing
{
    public class Conversation : BaseEntity
    {
        public string Message { get; set; }

        public bool IsPublic { get; set; }

        #region relations
        public long? TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }

        public virtual ICollection<TicketAttachment> Attachment { get; set; }
        #endregion
    }
}
