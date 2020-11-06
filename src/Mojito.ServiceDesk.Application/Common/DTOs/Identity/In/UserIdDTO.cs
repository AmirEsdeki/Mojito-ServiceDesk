using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;

namespace Mojito.ServiceDesk.Application.Common.DTOs.Identity.In
{
    public class UserIdDTO : IBaseDTOIn
    {
        public string UserId { get; set; }
    }
}
