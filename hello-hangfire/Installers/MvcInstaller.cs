using hello_hangfire.Services;
using Microsoft.Extensions.DependencyInjection;

namespace hello_hangfire.Installers
{
    public static class MvcInstaller
    {
        public static void InstallMvc(this IServiceCollection services)
        {
            services.AddControllers();
            
            services.AddTransient<IDateTimeService, DateTimeService>();

            services.AddScoped<IMessageService, MessageService>();
        }
    }
}