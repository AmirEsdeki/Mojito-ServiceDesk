using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mojito.ServiceDesk.Application.Common.Interfaces.Common;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.Common;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.CustomerOrganizationService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.GroupService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.GroupTypeService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.IssueUrlService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.JWTService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.PostService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.PriorityService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.ProductService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.ProfileImageService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.RoleService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.SendMessagesService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.TicketIssueService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.TicketLabelService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.TicketStatusService;
using Mojito.ServiceDesk.Application.Common.Interfaces.Services.UserService;
using Mojito.ServiceDesk.Core.Entities.Identity;
using Mojito.ServiceDesk.Infrastructure.Constant;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using Mojito.ServiceDesk.Infrastructure.Entities;
using Mojito.ServiceDesk.Infrastructure.Interfaces;
using Mojito.ServiceDesk.Infrastructure.Modules;
using Mojito.ServiceDesk.Infrastructure.Services.Common;
using Mojito.ServiceDesk.Infrastructure.Services.CustomerOrganizationService;
using Mojito.ServiceDesk.Infrastructure.Services.GroupService;
using Mojito.ServiceDesk.Infrastructure.Services.GroupTypeService;
using Mojito.ServiceDesk.Infrastructure.Services.IssueUrlService;
using Mojito.ServiceDesk.Infrastructure.Services.JWTService;
using Mojito.ServiceDesk.Infrastructure.Services.PostService;
using Mojito.ServiceDesk.Infrastructure.Services.PriorityService;
using Mojito.ServiceDesk.Infrastructure.Services.ProductService;
using Mojito.ServiceDesk.Infrastructure.Services.ProfileImageService;
using Mojito.ServiceDesk.Infrastructure.Services.RoleService;
using Mojito.ServiceDesk.Infrastructure.Services.SendMessagesService;
using Mojito.ServiceDesk.Infrastructure.Services.TicketIssueService;
using Mojito.ServiceDesk.Infrastructure.Services.TicketLabelService;
using Mojito.ServiceDesk.Infrastructure.Services.TicketStatusService;
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
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+!";
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
            services.AddScoped<IImageService, ImageService>();

            services.AddTransient<IDateTimeService, DateTimeService>();
            services.AddTransient<IEndPointAddresses, ExternalServiceEndPoints>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IRoleService, RoleService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<ICustomerOrganizationService, CustomerOrganizationService>();
            services.AddTransient<IGroupService, GroupService>();
            services.AddTransient<IGroupTypeService, GroupTypeService>();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<IIssueUrlService, IssueUrlService>();
            services.AddTransient<IPriorityService, PriorityService>();
            services.AddTransient<ITicketIssueService, TicketIssueService>();
            services.AddTransient<ITicketLabelService, TicketLabelService>();
            services.AddTransient<ITicketStatusService, TicketStatusService>();
            services.AddTransient<IProfileImageService, ProfileImageService>();


            return services;
        }
    }
}
