using System;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Hangfire_PW.Installers
{
    public static class SwaggerInstaller
    {
        public static void InstallSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(a =>
            {
                a.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Hangfire_PW",
                    Version = "v1",
                    Description = "Hangfire background processing for personal website",
                    Contact = new OpenApiContact
                    {
                        Name = "Florian Thom",
                        Email = "thom.florian@yahoo.de",
                    },
                });
                
                a.CustomSchemaIds(schemaIdStrategy);
                
                a.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                
                a.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }
        
        private static string schemaIdStrategy(Type currentClass)
        {
            string customSuffix = "Response";
            var tmpDisplayName = currentClass.ShortDisplayName().Replace("<", "").Replace(">", "");
            var removedSuffix = tmpDisplayName.EndsWith(customSuffix) ? tmpDisplayName.Substring(0, tmpDisplayName.Length - customSuffix.Length) : tmpDisplayName;
            return removedSuffix;
        }
    }
}