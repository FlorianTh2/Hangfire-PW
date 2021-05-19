using System;

namespace hello_hangfire.Services
{
    public class MessageService : IMessageService
    {
        private readonly IDateTimeService _dateTimeService;

        public MessageService(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
        }

        ~MessageService()
        {
            Console.WriteLine("message service got destroyed");
        }

        public void Send()
        {
            Console.WriteLine("hello hangfire");
        }
    }
}