using Hangfire_PW.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Hangfire_PW.Installers
{
    public static class MvcInstaller
    {
        public static void InstallMvc(this IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddTransient<IDateTimeService, DateTimeService>();
        }
    }
}