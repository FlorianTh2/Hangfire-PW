using System;
using System.Threading;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Server;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace hello_hangfire.Services
{
    // backgroundJobs
    // https://github.com/HangfireIO/Hangfire/blob/master/samples/NetCoreSample/Program.cs
    // https://stackoverflow.com/questions/53515314/what-is-an-correct-way-to-inject-db-context-to-hangfire-recurring-job
    // https://docs.hangfire.io/en/latest/background-methods/index.html
    // BackgroundJob.Enqueue<IEmailSender>(x => x.Send("hangfire@example.com"));
    // hangfireClient.Enqueue(() => Console.WriteLine("Hello world from Hangfire!"));

    // RecurringJob.AddOrUpdate<MessageService>(a => a.Send(), Cron.Minutely);
    public class RecurringJobsService : BackgroundService
    {
        // https://github.com/HangfireIO/Hangfire/blob/master/samples/NetCoreSample/Program.cs
        // https://stackoverflow.com/questions/48178792/where-am-i-supposed-to-start-persistent-background-tasks-in-asp-net-core
        // https://devblogs.microsoft.com/cesardelatorre/implementing-background-tasks-in-microservices-with-ihostedservice-and-the-backgroundservice-class-net-core-2-x/
        // we called these type of tasks as Hosted Services, because they are services/logic that you host within your host/application/microservice.
        // Note that in this case, the hosted service simply means a class with the background task logic
        // Host allows you to have a similar infrastructure than what you have with WebHost (dependency injection, hosted services, etc.),
        // but in this case, you just want to have a simple and lighter process as the host, with nothing related to MVC, Web API or Http server features
        // The way you add one or multiple IHostedServices into your WebHost or Host is by registering them up through the standard DI
        //    you have to register the hosted services within the familiar ConfigureServices() method of the Startup class
        // As a developer, you are responsible for handling the stopping action or your services when StopAsync() method is triggered by the host.
        // However, since most background tasks will need pretty similar needs in regard to the cancellation tokens management and other tipical operations,
        // .NET Core 2.1 is providing a very convenient abstract base class you can derive from, named BackgroundService.
        // By default, the cancellation token is set with a 5 second timeout, although you can change that value when building
        // your WebHost using the UseShutdownTimeout extension of the IWebHostBuilder.

        // keep in mind: if several containers: each container will use hangfire and its jobs & user interface
        // since that should only provide 1 instance of backend container

        private readonly IBackgroundJobClient _backgroundJobs;
        private readonly IRecurringJobManager _recurringJobs;

        // according to https://github.com/HangfireIO/Hangfire/blob/master/samples/NetCoreSample/Program.cs
        // you can configure custom CustomBackgroundJobFactory, CustomBackgroundJobPerformer, CustomBackgroundJobStateChanger
        // but if default is enough, its okay to use the default implementation of this services
        public RecurringJobsService(
            // hangfire servicef for fire-and-forget jobs
            IBackgroundJobClient backgroundJobs,
            // hangfire service for recurring jobs
            IRecurringJobManager recurringJobs)
        {
            _backgroundJobs = backgroundJobs;
            _recurringJobs = recurringJobs;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            AddStartingMessageJob();
            AddHelloSecondsJobs();
            return Task.CompletedTask;
        }

        private void AddStartingMessageJob()
        {
            // _backgroundJobs.Enqueue(() => Console.WriteLine("test"));
            _backgroundJobs.Enqueue<MessageService>(a => a.Send());
        }
        public void AddHelloSecondsJobs()
        {
            string jobId = "seconds";
            _recurringJobs.RemoveIfExists(jobId);
            _recurringJobs.AddOrUpdate<MessageService>(jobId,a => a.Send(), "*/1 * * * * *");
        }
    }
}