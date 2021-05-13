using Microsoft.Extensions.DependencyInjection;

namespace Hangfire_PW.Installers
{
    public static class CorsInstaller
    {
        public static void InstallCors(this IServiceCollection services)
        {
            services.AddCors(a => a.AddDefaultPolicy(builder =>
                    builder.AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed(origin => true)
                        .AllowCredentials())
            );
        }

    }
}