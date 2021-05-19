using System;

namespace hello_hangfire.Services
{
    public class MessageService
    {
        private readonly IDateTimeService _dateTimeService;

        public MessageService(IDateTimeService dateTimeService)
        {
            _dateTimeService = dateTimeService;
        }

        public void Send()
        {
            Console.Write("hello hangfire");
        }
    }
}