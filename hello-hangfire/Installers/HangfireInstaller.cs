using System;
using Hangfire;
using Hangfire.PostgreSql;
using Hangfire.SqlServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace hello_hangfire.Installers
{
    public static class HangfireInstaller
    {
        public static void InstallHangfire(this IServiceCollection services, IConfiguration Configuration)
        {
            // Add Hangfire services.
            services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UsePostgreSqlStorage(Configuration.GetConnectionString("HelloHangfireConnectionString"),
                    new PostgreSqlStorageOptions()
                    {
                        QueuePollInterval = TimeSpan.Zero,
                    })
            );
            
            // Add the processing server as IHostedService
            services.AddHangfireServer();
        }
    }
}