using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mojito.ServiceDesk.Application.Common.Constants.Messages;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity.Out;
using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Application.Common.Extensions;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.Common;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.UserService;
using Mojito.ServiceDesk.Web.Modules.AutoWrapper;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Web.Controllers
{
    //[Authorize(Roles ="user")]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> logger;
        private readonly IUserService userService;
        private readonly IHttpService httpService;

        public UsersController(ILogger<UsersController> logger,
            IUserService userService,
            IHttpService ipService)
        {
            this.logger = logger;
            this.userService = userService;
            this.httpService = ipService;
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(GuidIdDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Register([FromBody] SignUpDTO arg)
        {
            try
            {
                var userId = await userService.SignUpAsync(arg);
                return new ApiResponse(InfoMessages.UserCreated, userId, HttpStatusCode.Created.ToInt());
            }
            catch (ValidationException ex)
            {
                throw new ApiException(ex.Errors, ex.StatusCode);
            }
            catch (CustomException ex)
            {
                throw new ApiException(ex, ex.StatusCode);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(UserTokenDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> VerifyUser([FromBody] VerifyUserDTO arg)
        {
            try
            {
                var ip = httpService.IpAddress(Request, HttpContext);

                var token = await userService.VerifyUserAsync(arg, ip);

                httpService.SetCookie("refreshToken", token.RefreshToken, Response);
                return new ApiResponse(InfoMessages.UserVerified, token, HttpStatusCode.OK.ToInt());
            }
            catch (ValidationException ex)
            {
                throw new ApiException(ex.Errors, ex.StatusCode);
            }
            catch (CustomException ex)
            {
                throw new ApiException(ex, ex.StatusCode);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> ResendVerificationCode([FromBody] GuidIdDTO arg)
        {
            try
            {
                await userService.ResendVerificationCodeAsync(arg.Id);
                return new ApiResponse(InfoMessages.CodeHasSent, null, HttpStatusCode.OK.ToInt());
            }
            catch (CustomException ex)
            {
                throw new ApiException(ex, ex.StatusCode);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(UserTokenDTO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> SignIn([FromBody] SignInDTO arg)
        {
            try
            {
                var ip = httpService.IpAddress(Request, HttpContext);
                var token = await userService.SignInAsync(arg, ip);
                return new ApiResponse(InfoMessages.SuccesfullySignedIn, token, HttpStatusCode.OK.ToInt());
            }
            catch (CustomException ex)
            {
                throw new ApiException(ex, ex.StatusCode);
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        // Accepts token from request body or cookie
        [AllowAnonymous]
        [HttpPost]
        [Route("revoke-token")]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.NotFound)]
        public async Task<ApiResponse> RevokeToken([FromBody] RevokeTokenRequestDTO arg)
        {
            var token = arg.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                throw new ApiException(ErrorMessages.TokenIsEmpty, HttpStatusCode.BadRequest.ToInt());

            var response = await userService.RevokeToken(token, httpService.IpAddress(Request, HttpContext));

            if (!response)
                throw new ApiException(ErrorMessages.TokenNotFound, HttpStatusCode.NotFound.ToInt());

            return new ApiResponse(InfoMessages.RefreshTokenRevokedSuccessfully, null, HttpStatusCode.OK.ToInt());
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.Unauthorized)]
        public async Task<ApiResponse> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var token = await userService.RefreshToken(refreshToken, httpService.IpAddress(Request, HttpContext));

            if (token == null)
                throw new ApiException(ErrorMessages.InvalidToken, HttpStatusCode.Unauthorized.ToInt());

            httpService.SetCookie("refreshToken", token.RefreshToken, Response);

            return new ApiResponse(token, HttpStatusCode.OK.ToInt());
        }

        [HttpGet]
        public IActionResult Get()
        {
            userService.Get("1");
            return Content("hello");
        }
    }
}
