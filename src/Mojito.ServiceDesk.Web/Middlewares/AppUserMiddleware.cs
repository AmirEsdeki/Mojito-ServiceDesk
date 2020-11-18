using Microsoft.AspNetCore.Http;
using Mojito.ServiceDesk.Application.Common.Extensions;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService;
using System.Threading.Tasks;

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
            appUser.IdToString = httpContext.User.Identity.Name;
            await next(httpContext);
        }
    }
}
