using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mojito.ServiceDesk.Application.Common.Constants.Messages;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Conversation.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Conversation.Out;
using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Application.Common.Extensions;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.ConversationService;
using Mojito.ServiceDesk.Web.Modules.AutoWrapper;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Web.Controllers
{
    //[Authorize(Roles ="user,admin")]
    [ApiController]
    [Route("[controller]")]
    public class ConversationsController : ControllerBase
    {
        #region ctor
        private readonly ILogger<ConversationsController> logger;
        private readonly IConversationService conversationService;

        public ConversationsController(ILogger<ConversationsController> logger,
            IConversationService conversationService)
        {
            this.logger = logger;
            this.conversationService = conversationService;
        }
        #endregion

        #region CRUD
        [HttpGet]
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<PaginatedList<GetConversationDTO>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Get([FromQuery] ConversationsFilterParams arg)
        {
            try
            {
                var data = await conversationService.GetAllAsync(arg);
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
        [ProducesResponseType(typeof(AutoWrapperResponseSchema<GetConversationDTO>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Post([FromBody] PostConversationDTO arg)
        {
            try
            {
                var data = await conversationService.CreateAsync(arg);
                return new ApiResponse(InfoMessages.ConversationAdded, data, HttpStatusCode.OK.ToInt());
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
        public async Task<ApiResponse> Delete(string id)
        {
            try
            {
                await conversationService.DeleteAsync(id);
                return new ApiResponse(InfoMessages.ConversationRemoved, null, HttpStatusCode.OK.ToInt());
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
