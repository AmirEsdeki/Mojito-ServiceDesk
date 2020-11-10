using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mojito.ServiceDesk.Application.Common.Constants.Messages;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.User.In;
using Mojito.ServiceDesk.Application.Common.DTOs.User.Out;
using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Application.Common.Extensions;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.Common;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.UserService;
using Mojito.ServiceDesk.Web.Modules.AutoWrapper;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Web.Controllers
{
    //[Authorize(Roles ="user")]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        #region ctor
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
        #endregion

        #region Authentication
        [AllowAnonymous]
        [HttpPost]
        [Route("[action]")]
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<GuidIdDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Register([FromBody] SignUpDTO arg)
        {
            try
            {
                var userId = await userService.SignUpAsync(arg);
                return new ApiResponse(InfoMessages.UserCreated, userId, HttpStatusCode.OK.ToInt());
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
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<UserTokenDTO>), (int)HttpStatusCode.OK)]
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
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<UserTokenDTO>), (int)HttpStatusCode.OK)]
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
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
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
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<UserTokenDTO>), (int)HttpStatusCode.OK)]
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
        #endregion

        #region CRUD
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<GetUserDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Get(string id)
        {
            try
            {
                var user = await userService.Get(id);
                return new ApiResponse(user, HttpStatusCode.OK.ToInt());
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

        [HttpGet]
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<PaginatedList<GetUserDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Get([FromQuery] GetAllUserParams arg)
        {
            try
            {
                var users = await userService.GetAll(arg);
                return new ApiResponse(users, HttpStatusCode.OK.ToInt());
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

        [HttpPost]
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<GuidIdDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Post([FromBody] PostUserDTO arg)
        {
            try
            {
                var userId = await userService.Create(arg);
                return new ApiResponse(InfoMessages.UserCreatedByAdmin, userId, HttpStatusCode.OK.ToInt());
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

        [HttpPut]
        [Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Put(string id, [FromBody] PutUserDTO arg)
        {
            try
            {
                await userService.Update(id, arg);
                return new ApiResponse(InfoMessages.UserUpdated, null, HttpStatusCode.OK.ToInt());
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

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Put(string id)
        {
            try
            {
                await userService.Delete(id);
                return new ApiResponse(InfoMessages.UserDeleted, null, HttpStatusCode.OK.ToInt());
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
        #endregion


        #region OtherActions

        [HttpPost]
        [Route("{phrase}/filter")]
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<FilteredUsersDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Filter(string phrase)
        {
                var users = await userService.GeneralFilter(phrase);
                return new ApiResponse(users, HttpStatusCode.OK.ToInt());
        }

        [HttpPut]
        [Route("{userId}/add-group/{groupId}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> AddGroup(string userId, int groupId)
        {
            try
            {
                await userService.AddGroup(userId, groupId);
                return new ApiResponse(InfoMessages.GroupAdded, null, HttpStatusCode.OK.ToInt());
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


        [HttpDelete]
        [Route("{userId}/remove-group/{groupId}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> RemoveGroup(string userId, int groupId)
        {
            try
            {
                await userService.RemoveGroup(userId, groupId);
                return new ApiResponse(InfoMessages.GroupRemoved, null, HttpStatusCode.OK.ToInt());
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        [HttpPut]
        [Route("{userId}/add-post/{postId}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> AddPost(string userId, int postId)
        {
            try
            {
                await userService.AddPost(userId, postId);
                return new ApiResponse(InfoMessages.PostAdded, null, HttpStatusCode.OK.ToInt());
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


        [HttpDelete]
        [Route("{userId}/remove-post/{postId}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> RemovePost(string userId, int postId)
        {
            try
            {
                await userService.RemovePost(userId, postId);
                return new ApiResponse(InfoMessages.PostRemoved, null, HttpStatusCode.OK.ToInt());
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        [HttpPut]
        [Route("{userId}/add-customerorganization/{customerOrganizationId}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> AddCustomerOrganization(string userId, int customerOrganizationId)
        {
            try
            {
                await userService.AddCustomerOrganization(userId, customerOrganizationId);
                return new ApiResponse(InfoMessages.CustomerOrganizationAdded, null, HttpStatusCode.OK.ToInt());
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


        [HttpDelete]
        [Route("{userId}/remove-customerorganization/{customerOrganizationId}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> RemoveCustomerOrganization(string userId, int customerOrganizationId)
        {
            try
            {
                await userService.RemoveCustomerOrganization(userId, customerOrganizationId);
                return new ApiResponse(InfoMessages.CustomerOrganizationRemoved, null, HttpStatusCode.OK.ToInt());
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }

        [HttpPut]
        [Route("{userId}/add-issueurl/{issueUrlId}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> AddIssueUrl(string userId, int issueUrlId)
        {
            try
            {
                await userService.AddIssueUrl(userId, issueUrlId);
                return new ApiResponse(InfoMessages.IssueUrlAdded, null, HttpStatusCode.OK.ToInt());
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


        [HttpDelete]
        [Route("{userId}/remove-issueurl/{issueUrlId}")]
        [ProducesResponseType(typeof(ApiResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> RemoveIssueUrl(string userId, int issueUrlId)
        {
            try
            {
                await userService.RemoveIssueUrl(userId, issueUrlId);
                return new ApiResponse(InfoMessages.IssueUrlRemoved, null, HttpStatusCode.OK.ToInt());
            }
            catch (Exception ex)
            {
                throw new ApiException(ex);
            }
        }
        #endregion
    }
}
