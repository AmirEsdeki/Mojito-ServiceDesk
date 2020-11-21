using System;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Identity.In
{
    public class VerifyUserDTO
    {
        public Guid UserId { get; set; }
        public string Code { get; set; }
    }

    public class VerifyUserWithIdentityDTO
    {
        public string Identity { get; set; }
        public string Code { get; set; }
    }
}
