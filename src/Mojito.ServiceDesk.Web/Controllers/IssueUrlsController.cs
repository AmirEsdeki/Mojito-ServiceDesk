using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mojito.ServiceDesk.Application.Common.Constants.Messages;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.IssueUrl.In;
using Mojito.ServiceDesk.Application.Common.DTOs.IssueUrl.Out;
using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Application.Common.Extensions;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.IssueUrlService;
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
    public class IssueUrlsController : ControllerBase
    {
        #region ctor
        private readonly ILogger<IssueUrlsController> logger;
        private readonly IIssueUrlService issueUrlService;

        public IssueUrlsController(ILogger<IssueUrlsController> logger,
            IIssueUrlService issueUrlService)
        {
            this.logger = logger;
            this.issueUrlService = issueUrlService;
        }
        #endregion

        #region CRUD
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<GetIssueUrlDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Get(int id)
        {
            try
            {
                var data = await issueUrlService.GetAsync(id);
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

        [HttpGet]
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<PaginatedList<GetIssueUrlDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Get([FromQuery] IssueUrlsFilterParams arg)
        {
            try
            {
                var data = await issueUrlService.GetAllAsync(arg);
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
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<GetIssueUrlDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Post([FromBody] PostIssueUrlDTO arg)
        {
            try
            {
                var data = await issueUrlService.CreateAsync(arg);
                return new ApiResponse(InfoMessages.IssueUrlAdded, data, HttpStatusCode.OK.ToInt());
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
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Put(int id, [FromBody] PutIssueUrlDTO arg)
        {
            try
            {
                await issueUrlService.UpdateAsync(id, arg);
                return new ApiResponse(InfoMessages.IssueUrlUpdated, null, HttpStatusCode.OK.ToInt());
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
                await issueUrlService.DeleteAsync(id);
                return new ApiResponse(InfoMessages.IssueUrlRemoved, null, HttpStatusCode.OK.ToInt());
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
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<List<KeyValueDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Filter(string phrase)
        {
            ICollection< KeyValueDTO> users = await issueUrlService.FilterAsync(phrase);
            return new ApiResponse(users, HttpStatusCode.OK.ToInt());
        }
        #endregion
    }
}
