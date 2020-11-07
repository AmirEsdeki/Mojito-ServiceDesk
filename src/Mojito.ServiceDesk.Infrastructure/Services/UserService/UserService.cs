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
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
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

        public async Task<UserTokenDTO> VerifyUserAsync(VerifyUserDTO arg, string ip)
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
                    throw new EntityDoesNotExistException();
                if(!user.PhoneNumberConfirmed)
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


        public async Task<bool> RevokeToken(string token, string ipAddress)
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

        public async Task<UserTokenDTO> RefreshToken(string token, string ipAddress)
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
        #endregion
    }
}
