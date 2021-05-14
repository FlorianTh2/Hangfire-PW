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
            services.AddHangfire(config =>
                config.UsePostgreSqlStorage(Configuration.GetConnectionString("HelloHangfireConnectionString")));

            
            // Add the processing server as IHostedService
            services.AddHangfireServer();
        }
    }
}