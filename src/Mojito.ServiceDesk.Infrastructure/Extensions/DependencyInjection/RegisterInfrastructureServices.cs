using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mojito.ServiceDesk.Application.Common.Interfaces.Common;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.BaseService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.Common;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.PostService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.RoleService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.SendMessagesService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.UserService;
using Mojito.ServiceDesk.Core.Entities.Identity;
using Mojito.ServiceDesk.Infrastructure.Constant;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using Mojito.ServiceDesk.Infrastructure.Entities;
using Mojito.ServiceDesk.Infrastructure.Interfaces;
using Mojito.ServiceDesk.Infrastructure.Modules;
using Mojito.ServiceDesk.Infrastructure.Services.BaseService;
using Mojito.ServiceDesk.Infrastructure.Services.Common;
using Mojito.ServiceDesk.Infrastructure.Services.JWTService;
using Mojito.ServiceDesk.Infrastructure.Services.PostService;
using Mojito.ServiceDesk.Infrastructure.Services.RoleService;
using Mojito.ServiceDesk.Infrastructure.Services.SendMessagesService;
using Mojito.ServiceDesk.Infrastructure.Services.UserService;
using System;

namespace Mojito.ServiceDesk.Infrastructure.Extensions.DependencyInjection
{
    public static class RegisterInfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDBContext>(options =>
                    options.UseLazyLoadingProxies().UseInMemoryDatabase("TicketInMemoryDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDBContext>(options =>
                options.UseLazyLoadingProxies()
                    .UseSqlServer(
                        configuration.GetConnectionString("DefaultConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDBContext).Assembly.FullName)));
            }

            services.AddIdentity<User, IdentityRole>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDBContext>()
                .AddDefaultTokenProviders()
                .AddErrorDescriber<PersianIdentityErrorDescriber>();

            services.AddScoped<ISendEmailService, SendEmailService>();
            services.AddScoped<IRandomService, RandomNumberGeneratorService>();
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IHttpService, HttpService>();
            services.AddScoped<IAppUser, AppUser>();

            //services.AddTransient(typeof(IBaseService<,,,,>), typeof(BaseService<,,,,>));
            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEndPointAddresses, ExternalServiceEndPoints>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IPostService, PostService>();


            return services;
        }
    }
}
