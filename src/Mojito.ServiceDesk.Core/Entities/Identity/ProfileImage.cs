using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using System;

namespace Mojito.ServiceDesk.Core.Entities.Identity
{
    public class ProfileImage : BaseEntity
    {
        public byte[] Image { get; set; }

        public byte[] ImageThumbnail { get; set; }

        public string ContentType { get; set; }

        #region relation
        public string UserId { get; set; }
        public virtual User User { get; set; }
        #endregion
    }
}
