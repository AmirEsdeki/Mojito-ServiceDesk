using AutoWrapper.Wrappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mojito.ServiceDesk.Application.Common.Constants.Messages;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Role.In;
using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Application.Common.Extensions;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.RoleService;
using Mojito.ServiceDesk.Web.Modules.AutoWrapper;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly ILogger<UsersController> logger;
        private readonly IRoleService roleService;

        public RolesController(ILogger<UsersController> logger, IRoleService roleService)
        {
            this.logger = logger;
            this.roleService = roleService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(GuidIdDTO), (int)HttpStatusCode.Created)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(AutoWrapperErrorSchema), (int)HttpStatusCode.InternalServerError)]
        public async Task<ApiResponse> Post([FromBody] GetRoleDTO arg)
        {
            try
            {
                var role = await roleService.CreateRole(arg.RoleName);
                return new ApiResponse(InfoMessages.RoleCreated, role, HttpStatusCode.Created.ToInt());

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
    }
}
