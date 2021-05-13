using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Hangfire_PW.Installers
{
    public static class DbInstaller
    {
        public static void InstallDb(this IServiceCollection services, IConfiguration configuration)
        {
            string ConnectionString = configuration.GetConnectionString("PersonalWebsiteBackendContextPostgre"); 
            // services.AddDbContext<DataContext>(options =>
            //     options.UseNpgsql(ConnectionString));
            //
            // services.AddDatabaseDeveloperPageExceptionFilter();
        }
    }
}