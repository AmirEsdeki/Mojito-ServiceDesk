using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Hosting;
using System.IO;
using System.Linq;

namespace Mojito.ServiceDesk.Infrastructure.Extensions.Hosting
{
    public static class HostBuilderExtensions
    {
        /// <summary>
        /// Sets up the multi environment configuration inside the Config folder. 
        /// Enumerates all the config files inside the Config folder and loads them if they are relative to the environment.
        /// Also adds environment variables and CommandLine args
        /// ex. : 
        ///     Config/db.Production.json
        ///     Config/db.Development.json
        ///     Config/db.Staging.json
        /// </summary>
        /// <param name="hostBuilder">The <see cref="IHostBuilder" /> to configure.</param>
        /// <param name="args"></param>
        /// <returns>The same instance of the <see cref="IHostBuilder"/> for chaining.</returns>
        public static IHostBuilder ConfigureAppConfiguration(this IHostBuilder hostBuilder, string[] args = null)
        {
            hostBuilder.ConfigureAppConfiguration((hostingContext, builder) =>
            {
                var env = hostingContext.HostingEnvironment;


                //env.IsStaging()
                foreach (var jsonFilename in Directory.EnumerateFiles("Config", "*.json", SearchOption.AllDirectories).OrderBy(f => f.Length))
                {
                    if (env.IsStaging() && (jsonFilename.IndexOf(".Development") >= 0 || jsonFilename.IndexOf(".Production") >= 0))
                        continue;
                    if (env.IsProduction() && (jsonFilename.IndexOf(".Development") >= 0 || jsonFilename.IndexOf(".Staging") >= 0))
                        continue;
                    if (env.IsDevelopment() && (jsonFilename.IndexOf(".Staging") >= 0 || jsonFilename.IndexOf(".Production") >= 0))
                        continue;

                    builder.AddJsonFile(jsonFilename);
                }

                if (env.IsDevelopment())
                {
                    // We reload secrets.json over our development configurations
                    var secretFilePath = builder.Sources.OfType<JsonConfigurationSource>()
                      .Where(s => s.Path == "secrets.json")
                      .Select(s => s.FileProvider.GetFileInfo("secrets.json").PhysicalPath)
                      .FirstOrDefault();

                    if (secretFilePath != null)
                        builder.AddJsonFile(secretFilePath);
                }

                builder.AddEnvironmentVariables();
                if (args != null) builder.AddCommandLine(args);

            });

            return hostBuilder;

        }
    }
}
