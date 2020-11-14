namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService
{
    public interface IAppUser
    {
        public string[] Roles { get; set; }

        public string Id { get; set; }

        public string IsVerified { get; set; }

        public bool IsEmployee { get; set; }
    }
}
