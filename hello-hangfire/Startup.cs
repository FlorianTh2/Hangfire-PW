using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Client;
using Hangfire.Common;
using Hangfire.Dashboard;
using Hangfire.Server;
using Hangfire.States;
using hello_hangfire.Filter;
using hello_hangfire.Installers;
using hello_hangfire.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace hello_hangfire
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.InstallMvc();

            services.InstallHangfire(Configuration);

            services.InstallSwagger();

            services.InstallCors();
            
            services.AddHostedService<RecurringJobsService>();
        }

        public void Configure(IApplicationBuilder app, IBackgroundJobClient hangfireClient, IWebHostEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(a =>
            {
                a.SwaggerEndpoint("/swagger/v1/swagger.json", "Hangfire_PW v1");
                a.RoutePrefix = "swagger_hangfire";
            });

            // localhost/hangfire
            // if disabling maybe hangfire will not work since no activity
            // see https://stackoverflow.com/questions/44073911/hangfire-does-not-process-recurring-jobs-unless-dashboard-is-open
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                IsReadOnlyFunc = (DashboardContext context) => true
                // Authorization = new[] {new HangfireAuthorizationFilter()}
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                // endpoints.MapHangfireDashboard();
            });
        }
    }
}