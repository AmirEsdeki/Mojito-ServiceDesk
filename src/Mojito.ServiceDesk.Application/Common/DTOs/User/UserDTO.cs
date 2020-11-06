using Mojito.ServiceDesk.Application.Common.Interfaces.DTOs;

namespace Mojito.ServiceDesk.Application.DTOs.User
{
    public class UserDTOIn : IBaseDTOIn
    {
    }

    public class UserDTOOut : IBaseDTOOut
    {
        public int Id { get; set; }
    }
}
