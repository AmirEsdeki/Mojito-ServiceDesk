using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.RoleService;
using Mojito.ServiceDesk.Core.Entities.Identity;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.RoleService
{
    public class RoleService : IRoleService
    {
        #region Ctor
        private readonly UserManager<User> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IMapper mapper;

        public RoleService(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, IMapper mapper)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.mapper = mapper;
        }

        public async Task<GuidIdDTO> CreateRole(string roleName)
        {
            try
            {
                var role = new IdentityRole(roleName);
                var result = await roleManager.CreateAsync(role);

                if (result.Succeeded)
                {
                    return new GuidIdDTO()
                    { Id = role.Id };
                }
                else
                {
                    throw new ValidationException(result.Errors);
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
