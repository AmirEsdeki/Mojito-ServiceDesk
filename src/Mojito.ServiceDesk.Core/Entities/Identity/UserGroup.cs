using Mojito.ServiceDesk.Core.Entities.BaseEntities;

namespace Mojito.ServiceDesk.Core.Entities.Identity
{
    public class UserGroup : BaseEntity
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int GroupId { get; set; }

        public Group Group { get; set; }
    }
}
