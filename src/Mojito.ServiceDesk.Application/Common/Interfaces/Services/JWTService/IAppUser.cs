using System;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService
{
    public interface IAppUser
    {
        public string[] Roles { get; set; }

        public int[] Groups { get; set; }

        public Guid? Id { get; }

        public string IdToString { get; set; }

        public string IsVerified { get; set; }

        public bool IsCompanyMember { get; set; }

        public int CustomerOrganizationId { get; set; }
    }
}
