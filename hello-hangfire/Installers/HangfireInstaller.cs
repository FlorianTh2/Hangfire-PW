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
                        QueuePollInterval = TimeSpan.FromSeconds(15.0),
                        InvisibilityTimeout = TimeSpan.FromMinutes(30.0),
                        DistributedLockTimeout = TimeSpan.FromMinutes(10.0),
                        TransactionSynchronisationTimeout = TimeSpan.FromMilliseconds(500.0),
                        SchemaName = "hangfire",
                        UseNativeDatabaseTransactions = true,
                        PrepareSchemaIfNecessary = true,
                    })
            );

            // Add the processing server as IHostedService
            services.AddHangfireServer();
        }
    }
}