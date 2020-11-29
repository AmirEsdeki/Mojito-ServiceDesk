using Microsoft.AspNetCore.Http;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService;
using System.Threading.Tasks;
using Mojito.ServiceDesk.Web.Modules.Extension;

namespace Mojito.ServiceDesk.Web.Middlewares
{
    public class AppUserMiddleware
    {
        private readonly RequestDelegate next;

        public AppUserMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext httpContext, IAppUser appUser)
        {
            var user = httpContext.User;

            appUser.IdToString = user.Identity.Name;
            appUser.Roles = new string[] { user.GetRole() };
            appUser.Groups =  user.GetGroups();
            appUser.IsCompanyMember = user.IsCompanyMember();
            appUser.CustomerOrganizationId = user.GetCustomerOrganizationId();


            await next(httpContext);
        }
    }
}
