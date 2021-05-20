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
        
        public void Send()
        {
            Console.WriteLine("hello hangfire with timestamp: " + _dateTimeService.Now);
        }
    }
}