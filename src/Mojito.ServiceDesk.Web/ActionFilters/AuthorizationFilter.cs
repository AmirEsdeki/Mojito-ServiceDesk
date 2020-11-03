using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Application.Common.Interfaces;
using Mojito.ServiceDesk.Core.Constant;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace Mojito.ServiceDesk.Api.Modules
{
	public class AuthorizationFilter : ActionFilterAttribute
	{
		#region Ctor&Fields
		private readonly Roles role;
		private readonly bool selfAuth;

		public AuthorizationFilter(Roles role, bool selfAuth = false)
		{
			this.role = role;
			this.selfAuth = selfAuth;
		}
		#endregion


		public override void OnActionExecuting(ActionExecutingContext context)
		{
			var authenticateService = context.HttpContext.RequestServices.GetService<IAuthenticationService>();
			string token = string.Empty;

			try
			{
				token = context.HttpContext.Request.Headers["AuthToken"].ToString();
				var res = authenticateService.LoadUserIdentityAsync(token).Result;
			}
			catch (System.AggregateException ex)
			{
				throw new InvalidTokenException(ex);
			}
			catch (System.Exception ex)
			{
				throw;
			}


			var userId = authenticateService.Identity.UserId;
			var userRole = authenticateService.Identity.Role;

			if (context.HttpContext.Request.Method.ToUpper() != "OPTIONS")
			{
				if (selfAuth)
				{

					if (userId.ToString() != context.RouteData.Values["id"].ToString())
					{
						throw new UnauthorizedException();
					}
				}

				var hasAccess = userRole == role;
				if (!hasAccess)
					throw new UnauthorizedException();
			}

			base.OnActionExecuting(context);
		}
	}
}
