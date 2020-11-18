using Mojito.ServiceDesk.Application.Common.Extensions;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService;
using System;

namespace Mojito.ServiceDesk.Infrastructure.Services.JWTService
{
    public class AppUser : IAppUser
    {
        private string[] roles;
        public string[] Roles
        {
            get
            {
                roles.ForEach(f => f.ToLower());
                return roles;
            }
            set
            {
                roles = value;
            }
        }

        public Guid? Id
        {
            get
            {
                if (IdToString != null)
                {
                    return Guid.Parse(IdToString);
                }
                else
                {
                    return null;
                }

            }
        }


        public string IsVerified { get; set; }

        public bool IsCompanyMember { get; set; }

        public int[] Groups { get; set; }

        public int CustomerOrganizationId { get; set; }

        public string IdToString { get; set; }
    }
}
