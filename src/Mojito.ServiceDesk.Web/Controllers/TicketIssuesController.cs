using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mojito.ServiceDesk.Application.Common.Constants.Messages;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.TicketIssue.In;
using Mojito.ServiceDesk.Application.Common.DTOs.TicketIssue.Out;
using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Application.Common.Extensions;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.TicketIssueService;
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
    public class TicketIssuesController : ControllerBase
    {
        #region ctor
        private readonly ILogger<TicketIssuesController> logger;
        private readonly ITicketIssueService ticketIssueService;

        public TicketIssuesController(ILogger<TicketIssuesController> logger,
            ITicketIssueService ticketIssueService)
        {
            this.logger = logger;
            this.ticketIssueService = ticketIssueService;
        }
        #endregion

        #region CRUD
        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<GetTicketIssueDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Get(int id)
        {
            try
            {
                var data = await ticketIssueService.GetAsync(id);
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
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<PaginatedList<GetTicketIssueDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Get([FromQuery] TicketIssuesFilterParams arg)
        {
            try
            {
                var data = await ticketIssueService.GetAllAsync(arg);
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
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<GetTicketIssueDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Post([FromBody] PostTicketIssueDTO arg)
        {
            try
            {
                var data = await ticketIssueService.CreateAsync(arg);
                return new ApiResponse(InfoMessages.TicketIssueAdded, data, HttpStatusCode.OK.ToInt());
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
        public async Task<ApiResponse> Put(int id, [FromBody] PutTicketIssueDTO arg)
        {
            try
            {
                await ticketIssueService.UpdateAsync(id, arg);
                return new ApiResponse(InfoMessages.TicketIssueUpdated, null, HttpStatusCode.OK.ToInt());
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
                await ticketIssueService.DeleteAsync(id);
                return new ApiResponse(InfoMessages.TicketIssueRemoved, null, HttpStatusCode.OK.ToInt());
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
            ICollection<KeyValueDTO> users = await ticketIssueService.FilterAsync(phrase);
            return new ApiResponse(users, HttpStatusCode.OK.ToInt());
        }
        #endregion
    }
}
