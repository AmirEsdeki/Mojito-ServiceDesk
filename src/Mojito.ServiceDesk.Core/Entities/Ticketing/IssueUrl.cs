﻿using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using Mojito.ServiceDesk.Core.Entities.Identity;
using Mojito.ServiceDesk.Core.Entities.Proprietary;
using System.Collections.Generic;

namespace Mojito.ServiceDesk.Core.Entities.Ticketing
{
    public class IssueUrl : BaseEntity
    {
        public string Url { get; set; }

        #region relations
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public virtual ICollection<UserIssueUrl> Users { get; set; }
        #endregion
    }
}
