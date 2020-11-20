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
            //get
            //{
            //    roles.ForEach(f => f.ToLower());
            //    return roles;
            //}
            get
            {
                return new string[] { "user" };
            }
            //set
            //{
            //    roles = value;
            //}
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

        //public bool IsCompanyMember { get; set; }
        public bool IsCompanyMember => false;

        public int[] Groups { get; set; }

        //public int CustomerOrganizationId { get; set; }
        public int CustomerOrganizationId => 1;

        //public string IdToString { get; set; }
        public string IdToString => "75683f59-286b-4cb9-9b41-905da2c3e135";
    }
}
