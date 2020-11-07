using Mojito.ServiceDesk.Core.Entities.BaseEntities;

namespace Mojito.ServiceDesk.Core.Entities.Identity
{
    public class ProfileImage : BaseEntity
    {
        public byte[] Image { get; set; }

        #region relation
        public virtual User User { get; set; }
        #endregion
    }
}
