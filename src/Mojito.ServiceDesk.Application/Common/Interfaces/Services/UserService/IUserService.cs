using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.User.In;
using Mojito.ServiceDesk.Application.Common.DTOs.User.Out;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Application.Common.Interfaces.Services.UserService
{
    public interface IUserService
    {
        #region Authentication
        Task<GuidIdDTO> SignUpAsync(SignUpDTO arg);

        Task<UserTokenDTO> VerifyUserAsync(VerifyUserDTO arg, string ip);

        Task<UserTokenDTO> VerifyUserWithIdentityAsync(VerifyUserWithIdentityDTO arg, string ip);

        Task<string> ResendVerificationCodeAsync(string identity);

        Task<UserTokenDTO> ChangePasswordAsync(ChangePasswordDTO arg, string ip);

        Task<UserTokenDTO> SignInAsync(SignInDTO arg, string ip);

        Task SetRole(string role, string userId);

        Task<UserTokenDTO> RefreshTokenAsync(string token, string ipAddress);

        Task<bool> RevokeTokenAsync(string token, string ipAddress);
        #endregion

        #region CRUD
        Task<PaginatedList<GetUserDTO>> GetAllAsync(UsersFilterParams arg);

        Task<GetUserDTO> GetAsync(string id);

        Task<GuidIdDTO> CreateAsync(PostUserDTO arg);

        Task UpdateAsync(string id, PutUserDTO arg);

        Task DeleteAsync(string id);

        Task<ICollection<FilteredUsersDTO>> GeneralFilterAsync(string phrase);
        #endregion

        #region RelationActions
        Task AddGroupAsync(string userId, int groupId);

        Task RemoveGroupAsync(string userId, int groupId);

        Task AddPostAsync(string userId, int postId);

        Task RemovePostAsync(string userId, int postId);

        Task AddCustomerOrganizationAsync(string userId, int customerOrganizationId);

        Task RemoveCustomerOrganizationAsync(string userId, int customerOrganizationId);

        Task AddIssueUrlAsync(string userId, int issueUrlId);

        Task RemoveIssueUrlAsync(string userId, int issueUrlId);

        #endregion






    }
}
