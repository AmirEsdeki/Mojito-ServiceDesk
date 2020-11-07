using Microsoft.AspNetCore.Http;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.Common;
using System;

namespace Mojito.ServiceDesk.Infrastructure.Services.Common
{
    public class HttpService : IHttpService
    {
        public string IpAddress(HttpRequest request, HttpContext httpContext)
        {
            if (request.Headers.ContainsKey("X-Forwarded-For"))
                return request.Headers["X-Forwarded-For"];
            else
                return httpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }

        public void SetCookie(string key, string value, HttpResponse response)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7)
            };
            response.Cookies.Append(key, value, cookieOptions);
        }
    }
}
