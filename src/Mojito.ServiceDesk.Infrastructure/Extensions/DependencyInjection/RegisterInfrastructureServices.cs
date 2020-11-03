using Mojito.ServiceDesk.Application.Common.Interfaces;
using Mojito.ServiceDesk.Infrastructure.Constant;
using Mojito.ServiceDesk.Infrastructure.Data.EF;
using Mojito.ServiceDesk.Infrastructure.Interfaces;
using Mojito.ServiceDesk.Infrastructure.RemoteServices;
using Mojito.ServiceDesk.Infrastructure.Services.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddScoped<IApplicationDBContext>(provider => provider.GetService<ApplicationDBContext>());

            services.AddTransient<IDateTime, DateTimeService>();
            services.AddTransient<IEndPointAddresses, ExternalServiceEndPoints>();

            return services;
        }
    }
}
