using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mojito.ServiceDesk.Application.Common.Constants.Messages;
using Mojito.ServiceDesk.Application.Common.DTOs.ProfileImage.In;
using Mojito.ServiceDesk.Application.Common.DTOs.ProfileImage.Out;
using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Application.Common.Extensions;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.ProfileImageService;
using Mojito.ServiceDesk.Web.Modules.AutoWrapper;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Web.Controllers
{
    //[Authorize(Roles ="user")]
    [ApiController]
    [Route("[controller]")]
    public class ProfileImagesController : ControllerBase
    {
        #region ctor
        private readonly ILogger<ProfileImagesController> logger;
        private readonly IProfileImageService profileImageService;

        public ProfileImagesController(ILogger<ProfileImagesController> logger,
            IProfileImageService profileImageService)
        {
            this.logger = logger;
            this.profileImageService = profileImageService;
        }
        #endregion

        #region CRUD
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<GetProfileImageDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Get(int id)
        {
            try
            {
                var data = await profileImageService.GetAsync(id);
                return new ApiResponse(data, HttpStatusCode.OK.ToInt());
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
        [Route("{userId}/set-image")]
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<GetProfileImageDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Post(string userId, [FromBody] IFormFile arg)
        {
            try
            {
                byte[] image;
                using (var ms = new MemoryStream())
                {
                    arg.CopyTo(ms);
                    image = ms.ToArray();
                }

                var data = await profileImageService.CreateAsync(new PostProfileImageDTO
                {
                    ContentType = arg.ContentType,
                    UserId = userId,
                    Image = image
                });
                return new ApiResponse(InfoMessages.ProfileImageAdded, data, HttpStatusCode.OK.ToInt());
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
        public async Task<ApiResponse> Delete(int id)
        {
            try
            {
                await profileImageService.DeleteAsync(id);
                return new ApiResponse(InfoMessages.ProfileImageRemoved, null, HttpStatusCode.OK.ToInt());
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

    }
}
