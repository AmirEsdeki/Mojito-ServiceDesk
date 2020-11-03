using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Net;


namespace Mojito.ServiceDesk.Web.ActionFilters
{

	public class AuthenticationFilter : ActionFilterAttribute
	{

		public AuthenticationFilter()
		{
		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			try
			{
				var authenticateService = context.HttpContext.RequestServices.GetService<IAuthenticationService>();
				string token = context.HttpContext.Request.Headers["AuthToken"].ToString();
				var res = authenticateService.LoadUserIdentityAsync(token).Result;
			}
			catch (System.AggregateException ex)
			{
				throw new CustomException(message: ex.InnerException.Message, statusCode: (int)HttpStatusCode.Unauthorized);
			}
			catch (System.Exception)
			{
				throw;
			}
		}
	}
}
