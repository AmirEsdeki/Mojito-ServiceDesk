using Microsoft.AspNetCore.Http;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.Common
{
    public interface IHttpService
    {
        public string IpAddress(HttpRequest request, HttpContext httpContext);
        void SetCookie(string key, string value, HttpResponse response);
    }
}
