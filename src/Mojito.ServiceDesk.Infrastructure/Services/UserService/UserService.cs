using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Mojito.ServiceDesk.Application.Common.DTOs.Identity.In;
using Mojito.ServiceDesk.Application.Common.Exceptions;
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
        private readonly IMapper mapper;

        public UserService(UserManager<User> userManager,
            SignInManager<User> signInManager, ISendEmailService emailService, IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailService = emailService;
            this.mapper = mapper;
        }
        #endregion

        public async Task RegisterAsync(SignupDTO arg)
        {
            User user = mapper.Map<User>(arg);
            try
            {
                var result = await userManager.CreateAsync(user, arg.Password);

                if (result.Succeeded)
                {
                    result = await userManager.AddToRoleAsync(user, Roles.User);
                    SendVerificationCodeAsync(user);
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

        public async Task VerifyUserAsync(VerifyUserDTO arg)
        {
            try
            {
                var user = await userManager.FindByIdAsync(arg.UserId);

                if (user == null)
                    throw new EntityDoesNotExistException();

                var result = await userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, arg.Code);

                if (result.Succeeded)
                {
                    if (user != null)
                    {
                        //todo
                        //await SignInAsync(user, isPersistent: false);
                    }
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

                var result = await userManager.ChangePhoneNumberAsync(user, user.PhoneNumber, arg.Code);

                if (result.Succeeded)
                {
                    if (user != null)
                    {
                        //await SignInAsync(user, isPersistent: false);
                    }
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
