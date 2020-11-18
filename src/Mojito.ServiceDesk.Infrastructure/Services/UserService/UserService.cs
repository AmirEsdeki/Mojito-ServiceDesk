using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity.Out;
using Mojito.ServiceDesk.Application.Common.DTOs.User.In;
using Mojito.ServiceDesk.Application.Common.DTOs.User.Out;
using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Application.Common.Extensions;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.SendMessagesService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.UserService;
using Mojito.ServiceDesk.Core.Common.Interfaces;
using Mojito.ServiceDesk.Core.Constant;
using Mojito.ServiceDesk.Core.Entities.Identity;
using Mojito.ServiceDesk.Core.Entities.Ticketing;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.UserService
{
    //because userService uses Identity provided service it does not implement IBaseService
    public class UserService : IUserService
    {
        #region ctor
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ISendEmailService emailService;
        private readonly IJwtService jwtService;
        private readonly ApplicationDBContext db;
        private readonly IMapper mapper;

        public UserService(UserManager<User> userManager,
            SignInManager<User> signInManager,
            ISendEmailService emailService,
            IJwtService jwtService,
            ApplicationDBContext db,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailService = emailService;
            this.jwtService = jwtService;
            this.db = db;
            this.mapper = mapper;
        }
        #endregion

        #region Authentication
        public async Task<GuidIdDTO> SignUpAsync(SignUpDTO arg)
        {
            User user = mapper.Map<User>(arg);
            try
            {
                var result = await userManager.CreateAsync(user, arg.Password);

                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user, Roles.User);
                    SendVerificationCodeAsync(user);
                    return new GuidIdDTO()
                    { Id = user.Id };
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

        public async Task<UserTokenDTO> VerifyUserAsync(VerifyUserDTO arg, string ip)
        {
            try
            {
                var user = await userManager.FindByIdAsync(arg.UserId.ToString());

                if (user == null)
                    throw new EntityNotFoundException();

                var result = await userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, arg.Code);

                if (result.Succeeded)
                {
                    //change last code to insure security.
                    var confirmationToken =
                        await userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);

                    var roles = await userManager.GetRolesAsync(user);
                    var refreshToken = jwtService.GenerateRefreshToken(ip);

                    if (user.RefreshTokens == null)
                        user.RefreshTokens = new List<RefreshToken>();

                    user.RefreshTokens.Add(refreshToken);
                    await db.SaveChangesAsync();

                    return new UserTokenDTO(jwtService.GenerateAuthorizationToken(user, roles),
                       refreshToken.Token);
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

        public async Task ResendVerificationCodeAsync(string userId)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId);

                if (user == null)
                    throw new EntityNotFoundException();
                if (!user.PhoneNumberConfirmed)
                    SendVerificationCodeAsync(user);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public async Task<UserTokenDTO> SignInAsync(SignInDTO arg, string ip)
        {
            try
            {
                var user = await userManager.FindByNameAsync(arg.Username);

                if (user != null && !user.PhoneNumberConfirmed)
                    throw new AccountNotVerifiedException();

                var result = await signInManager.CheckPasswordSignInAsync(user, arg.Password, true);

                if (result.Succeeded)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    var refreshToken = jwtService.GenerateRefreshToken(ip);
                    user.RefreshTokens.Add(refreshToken);
                    await db.SaveChangesAsync();

                    return new UserTokenDTO(jwtService.GenerateAuthorizationToken(user, roles),
                       refreshToken.Token);
                }
                else
                {
                    if (result.IsLockedOut)
                    {
                        throw new AccountLockedException();
                    }

                    throw new WrongCredentialsException();
                }
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> RevokeTokenAsync(string token, string ipAddress)
        {
            var user = db.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null) return false;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            // return false if token is not active
            if (!refreshToken.IsActive) return false;

            refreshToken.Revoked = DateTime.Now;
            refreshToken.RevokedByIp = ipAddress;
            db.Update(user);
            await db.SaveChangesAsync();

            return true;
        }

        public async Task<UserTokenDTO> RefreshTokenAsync(string token, string ipAddress)
        {
            var user = db.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null) return null;

            if (user.RefreshTokens == null) return null;

            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive) return null;

            var newRefreshToken = GenerateRefreshToken(ipAddress);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            user.RefreshTokens.Add(newRefreshToken);
            db.Update(user);
            db.SaveChanges();

            var roles = await userManager.GetRolesAsync(user);
            var jwtToken = jwtService.GenerateAuthorizationToken(user, roles);

            return new UserTokenDTO(jwtToken, newRefreshToken.Token);
        }
        #endregion

        #region CRUD
        public async Task<GetUserDTO> GetAsync(string id)
        {
            var user = await db.Users.FirstOrDefaultAsync(f => f.Id == id);

            if (user != null)
                return mapper.Map<GetUserDTO>(user);

            throw new EntityNotFoundException();
        }

        public async Task<PaginatedList<GetUserDTO>> GetAllAsync(UsersFilterParams arg)
        {
            try
            {
                IQueryable<User> query = db.Users;

                if (arg.GeneralName != null)
                    query = query.Where(w => w.UserName.StartsWith(arg.GeneralName)
                        || w.Email.StartsWith(arg.GeneralName)
                        || w.PhoneNumber.StartsWith(arg.GeneralName)
                        || w.FullName.Contains(arg.GeneralName));

                if (arg.GroupId != 0)
                    query = query.Where(w => w.Groups.Any(a => a.GroupId == arg.GroupId));

                if (arg.IssueUrlId != 0)
                    query = query.Where(w => w.IssueUrls.Any(a => a.IssueUrlId == arg.IssueUrlId));

                if (arg.CustomerOrganizationId != 0)
                    query = query.Where(w => w.CustomerOrganizationId == arg.CustomerOrganizationId);

                if (arg.PostId != 0)
                    query = query.Where(w => w.PostId == arg.PostId);

                if (arg.IsCompanyMember != null)
                    query = query.Where(w => w.IsCompanyMember == arg.IsCompanyMember);

                var list = await new PaginatedListBuilder<User, GetUserDTO>(mapper)
                    .CreateAsync(query, arg.PageNumber, arg.PageSize);

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<GuidIdDTO> CreateAsync(PostUserDTO arg)
        {
            var user = mapper.Map<User>(arg);
            try
            {
                var result = await userManager.CreateAsync(user, arg.Password);

                if (result.Succeeded)
                {
                    //as this method is only available for admins so below options are applied.
                    if (arg.CreateAsAdmin)
                        await userManager.AddToRoleAsync(user, Roles.Admin);
                    else
                        await userManager.AddToRoleAsync(user, Roles.Employee);

                    user.PhoneNumberConfirmed = true;
                    user.EmailConfirmed = true;
                    user.IsCompanyMember = true;

                    return new GuidIdDTO()
                    { Id = user.Id };
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

        public async Task UpdateAsync(string userId, PutUserDTO arg)
        {
            try
            {
                var user = await userManager.FindByIdAsync(userId);

                if (user == null)
                    throw new EntityNotFoundException();

                var IsEnteredDataAvailable = true;

                if (arg.Username != null)
                    IsEnteredDataAvailable =
                        !(await db.Users.AnyAsync(a => a.NormalizedUserName == arg.Username.ToUpper()
                            && a.Id != userId));

                if (arg.Email != null && IsEnteredDataAvailable)
                    IsEnteredDataAvailable =
                        !(await db.Users.AnyAsync(a => a.NormalizedEmail == arg.Email.ToUpper()
                            && a.Id != userId));

                if (IsEnteredDataAvailable)
                {
                    if (arg.Username != null)
                        await userManager.SetUserNameAsync(user, arg.Username);

                    if (arg.Email != null)
                        await userManager.SetEmailAsync(user, arg.Email);
                }
                else
                {
                    throw new UserNameOrEmailIsNotAvailableException();
                }

                mapper.Map(arg, user);

                db.Update(user);
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task DeleteAsync(string id)
        {
            try
            {
                var user = await userManager.FindByIdAsync(id);

                if (user != null)
                    throw new EntityNotFoundException();

                user.IsDeleted = true;

                db.Update(user);
                await db.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region RelationActions

        public async Task AddGroupAsync(string userId, int groupId)
        {
            try
            {
                var user = await ReturnUserIfBothExistsElseThrow<Group>(userId, groupId);

                (user.Groups ??= new List<UserGroup>()).Add(
                    new UserGroup
                    {
                        UserId = userId,
                        GroupId = groupId
                    });

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RemoveGroupAsync(string userId, int groupId)
        {
            try
            {
                var user = await ReturnUserIfBothExists<Group>(userId, groupId);

                if (user == null)
                    return;

                var group = user.Groups.FirstOrDefault(w => w.GroupId == groupId);

                if (group != null)
                {
                    user.Groups.Remove(group);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AddPostAsync(string userId, int postId)
        {
            try
            {
                var user = await ReturnUserIfBothExistsElseThrow<Post>(userId, postId);

                user.PostId = postId;
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RemovePostAsync(string userId, int postId)
        {
            try
            {
                var user = await ReturnUserIfBothExists<Post>(userId, postId);

                if (user == null)
                    return;

                user.PostId = null;
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AddCustomerOrganizationAsync(string userId, int customerOrganizationId)
        {
            try
            {
                var user = await ReturnUserIfBothExistsElseThrow<CustomerOrganization>(userId, customerOrganizationId);

                user.CustomerOrganizationId = customerOrganizationId;
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RemoveCustomerOrganizationAsync(string userId, int customerOrganizationId)
        {
            try
            {
                var user = await ReturnUserIfBothExists<CustomerOrganization>(userId, customerOrganizationId);

                if (user == null)
                    return;

                user.CustomerOrganizationId = null;
                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task AddIssueUrlAsync(string userId, int issueUrlId)
        {
            try
            {
                var user = await ReturnUserIfBothExistsElseThrow<IssueUrl>(userId, issueUrlId);

                (user.IssueUrls ??= new List<UserIssueUrl>()).Add(
                    new UserIssueUrl
                    {
                        UserId = userId,
                        IssueUrlId = issueUrlId
                    });

                await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task RemoveIssueUrlAsync(string userId, int issueUrlId)
        {
            try
            {
                var user = await ReturnUserIfBothExists<IssueUrl>(userId, issueUrlId);

                if (user == null)
                    return;

                var issueUrl = user.IssueUrls.FirstOrDefault(w => w.IssueUrlId == issueUrlId);

                if (issueUrl != null)
                {
                    user.IssueUrls.Remove(issueUrl);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<ICollection<FilteredUsersDTO>> GeneralFilterAsync(string phrase)
        {
            try
            {
                var user = await db.Users
                    .Where(w => w.UserName.StartsWith(phrase)
                        || w.FirstName.StartsWith(phrase)
                        || w.LastName.StartsWith(phrase)
                        || w.PhoneNumber.StartsWith(phrase)
                        || w.Email.StartsWith(phrase)
                        || w.FullName.Contains(phrase))
                    .ToListAsync();

                return mapper.Map<ICollection<FilteredUsersDTO>>(user);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region private
        private async void SendVerificationCodeAsync(User user)
        {
            try
            {
                var confirmationToken =
                        await userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);

                var emailMessage = new StringBuilder()
                    .Append($"Your Confirmation Key Is : {confirmationToken}").ToString();

                emailService.SendEmailAsync(user.Email, "Mojito-ServiceDesk Email Confirmation", emailMessage);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);

                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomBytes),
                    Expires = DateTime.Now.AddDays(7),
                    Created = DateTime.Now,
                    CreatedByIp = ipAddress
                };
            }
        }

        private async Task<User> ReturnUserIfBothExistsElseThrow<T>(string userId, int entityId)
            where T : class, IBaseEntity
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
                throw new EntityNotFoundException();

            if (!await db.Set<T>().AnyAsync(a => a.Id == entityId))
                throw new EntityNotFoundException();

            return user;
        }

        private async Task<User> ReturnUserIfBothExists<T>(string userId, int entityId)
            where T : class, IBaseEntity
        {
            var user = await userManager.FindByIdAsync(userId);

            if (user == null)
                return null;

            if (!await db.Set<T>().AnyAsync(a => a.Id == entityId))
                return null;

            return user;
        }
        #endregion
    }
}
