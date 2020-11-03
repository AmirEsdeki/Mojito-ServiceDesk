using Mojito.ServiceDesk.Core.Entities.BaseEntities;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using System;
using System.Collections.Generic;

namespace Mojito.ServiceDesk.Core.Entities.Identity
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string Email { get; set; }

        public string MobileNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int? VerificationCode { get; set; }

        public DateTime? VerificationExpDate { get; set; }

        public bool IsVerified { get; set; }

        public DateTime? VerficationDate { get; set; }

        public string ForgottenPassCode { get; set; }

        public string RememberToken { get; set; }

        public bool IsActive { get; set; }

        public int LogInAttempt { get; set; }

        public DateTime LockExpTime { get; set; }

        #region relations
        public int? RoleId { get; set; }
        public Role Role { get; set; }

        public int? PostId { get; set; }
        public Post Post { get; set; }

        public int ProfileImageId { get; set; }
        public ProfileImage ProfileImage { get; set; }

        public ICollection<UserGroup> Groups { get; set; }

        public ICollection<UserIssueUrl> IssueUrls { get; set; }

        #endregion
    }
}
