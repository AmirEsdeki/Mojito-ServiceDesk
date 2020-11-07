using AutoWrapper;
using Mojito.ServiceDesk.Application.Common.Extensions.DependencyInjection;
using Mojito.ServiceDesk.Infrastructure.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Mojito.ServiceDesk.Web.Modules;

namespace Mojito.ServiceDesk.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            ConfigureSwaggerServices(services);

            services.AddControllers()
                .AddJsonOptions(x => x.JsonSerializerOptions.IgnoreNullValues = true);

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);
            var appSettings = appSettingsSection.Get<AppSettings>();

            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddInfrastructureServices(Configuration);
            services.AddApplicationServices();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            ConfigureSwagger(app);

            //Enable AutoWrapper
            app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions
            {
                ShowStatusCode = true,
                ShowIsErrorFlagForSuccessfulResponse = true,
                IsDebug = true,
                UseApiProblemDetailsException = false
            });

            app.UseCors("CorsPolicy");

            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        protected virtual void ConfigureSwagger(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                var domainName = Configuration.GetSection("EnvironmentDomain").GetSection("Domain").Value;
                c.SwaggerEndpoint(domainName + "/swagger/v1/swagger.json", "Ticketing API");
            });
        }

        protected virtual void ConfigureSwaggerServices(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.OperationFilter<SwaggerOperationFilter>();

                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Authorization API",
                        Version = "v1",
                        Contact = new OpenApiContact { Name = "Amir Esdeki" }
                    });

                //var appEnv = PlatformServices.Default.Application;

                //c.IncludeXmlComments(Path.Combine(appEnv.ApplicationBasePath, $"{appEnv.ApplicationName}.xml"));

            });
        }
    }
}
