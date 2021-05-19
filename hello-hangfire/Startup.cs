using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using hello_hangfire.Filter;
using hello_hangfire.Installers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] {new HangfireAuthorizationFilter()}
            });

            // backgroundJobs
            // https://github.com/HangfireIO/Hangfire/blob/master/samples/NetCoreSample/Program.cs
            // https://stackoverflow.com/questions/53515314/what-is-an-correct-way-to-inject-db-context-to-hangfire-recurring-job
            // https://docs.hangfire.io/en/latest/background-methods/index.html
            // BackgroundJob.Enqueue<IEmailSender>(x => x.Send("hangfire@example.com"));
            hangfireClient.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));

            // RecurringJob.AddOrUpdate(() => new MessageService(context).Send(), Cron.Daily);


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