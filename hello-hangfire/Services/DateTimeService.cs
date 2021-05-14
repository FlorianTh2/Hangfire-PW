using System;

namespace hello_hangfire.Services
{
    public class DateTimeService : IDateTimeService
    {
        DateTime IDateTimeService.Now => DateTime.UtcNow;
    }
}