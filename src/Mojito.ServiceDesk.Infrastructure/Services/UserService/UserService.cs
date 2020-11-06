using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Mojito.ServiceDesk.Application.Common.DTOs.Common;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity.In;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity.Out;
using Mojito.ServiceDesk.Application.Common.Exceptions;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.SendMessagesService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.UserService;
using Mojito.ServiceDesk.Core.Constant;
using Mojito.ServiceDesk.Core.Entities.Identity;
using System.Text;
using System.Threading.Tasks;

namespace Mojito.ServiceDesk.Infrastructure.Services.UserService
{
    public class UserService : IUserService
    {
        #region Ctor
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ISendEmailService emailService;
        private readonly IJwtService jwtService;
        private readonly IMapper mapper;

        public UserService(UserManager<User> userManager,
            SignInManager<User> signInManager, ISendEmailService emailService, IJwtService jwtService, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailService = emailService;
            this.jwtService = jwtService;
            this.mapper = mapper;
        }
        #endregion

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
                        { Id = user.Id};
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

        public async Task<UserTokenDTO> VerifyUserAsync(VerifyUserDTO arg)
        {
            try
            {
                var user = await userManager.FindByIdAsync(arg.UserId);

                if (user == null)
                    throw new EntityDoesNotExistException();

                var result = await userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, arg.Code);

                if (result.Succeeded)
                {
                    //change last code to insure security.
                    var confirmationToken =
                        await userManager.GenerateChangePhoneNumberTokenAsync(user, user.PhoneNumber);

                    var roles = await userManager.GetRolesAsync(user);
                    return new UserTokenDTO()
                    { Token = jwtService.GenerateAuthorizationToken(user, roles) };
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
                    throw new EntityDoesNotExistException();

                SendVerificationCodeAsync(user);
            }
            catch (System.Exception ex)
            {
                throw;
            }
        }

        public async Task<UserTokenDTO> SignInAsync(SignInDTO arg)
        {
            try
            {
                var user = await userManager.FindByNameAsync(arg.Username);

                if (user != null && !user.PhoneNumberConfirmed)
                    throw new AccountNotVerifiedException();

                var result = await signInManager.PasswordSignInAsync(arg.Username, arg.Password, arg.RememberMe, true);

                if (result.Succeeded)
                {
                    var roles = await userManager.GetRolesAsync(user);
                    return new UserTokenDTO() 
                        { Token = jwtService.GenerateAuthorizationToken(user, roles) };
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

        #endregion
    }
}
