namespace Mojito.ServiceDesk.Application.Common.Interfaces.DTOs.User
{
    public class UserDTOIn : IBaseDTOIn
    {
    }

    public class UserDTOOut : IBaseDTOOut
    {
        public int Id { get; set; }
    }
}
