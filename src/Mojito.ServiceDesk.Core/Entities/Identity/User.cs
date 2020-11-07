using Microsoft.AspNetCore.Identity;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using System;
using System.Collections.Generic;


namespace Mojito.ServiceDesk.Core.Entities.Identity
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsEmployee { get; set; }

        public DateTime Created { get; set; }

        public DateTime? LastModified { get; set; }

        public bool IsDeleted { get; set; }


        #region relations
        public int? PostId { get; set; }
        public virtual Post Post { get; set; }

        public int? CustomerOrganizationId { get; set; }
        public virtual CustomerOrganization CustomerOrganization { get; set; }

        public int? ProfileImageId { get; set; }
        public virtual ProfileImage ProfileImage { get; set; }

        public virtual ICollection<UserGroup> Groups { get; set; }

        public virtual ICollection<UserIssueUrl> IssueUrls { get; set; }

        public virtual ICollection<RefreshToken> RefreshTokens { get; set; }

        public Guid? LastModifiedById { get; set; }

        public Guid? CreatedById { get; set; }

        #endregion
    }
}
