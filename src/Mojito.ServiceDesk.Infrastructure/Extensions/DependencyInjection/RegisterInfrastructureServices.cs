using Mojito.ServiceDesk.Application.Common.Interfaces;
using Mojito.ServiceDesk.Infrastructure.Constant;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using Mojito.ServiceDesk.Infrastructure.Interfaces;
using Mojito.ServiceDesk.Infrastructure.RemoteServices;
using Mojito.ServiceDesk.Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using Mojito.ServiceDesk.Infrastructure.Modules;
using Mojito.ServiceDesk.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Mojito.ServiceDesk.Infrastructure.Extensions.DependencyInjection
{
    public static class RegisterInfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            #region security
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            #endregion

            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDBContext>(options =>
                    options.UseInMemoryDatabase("TicketInMemoryDb"));
            }
            else if (configuration.GetValue<bool>("UseSqliteDatabase"))
            {
                services.AddDbContext<ApplicationDBContext>(options =>
                    options.UseSqlite("TicketSqliteDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDBContext>(options =>
                    options.UseSqlServer(
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


            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IEndPointAddresses, ExternalServiceEndPoints>();

            return services;
        }
    }
}
