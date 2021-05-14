using Microsoft.Extensions.DependencyInjection;

namespace hello_hangfire.Installers
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