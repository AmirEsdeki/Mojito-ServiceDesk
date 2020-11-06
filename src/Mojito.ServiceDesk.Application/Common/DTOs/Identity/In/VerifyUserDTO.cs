using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;
using System;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Identity.In
{
    public class VerifyUserDTO : IBaseDTOIn
    {
        public string UserId { get; set; }
        public string Code { get; set; }
    }
}
