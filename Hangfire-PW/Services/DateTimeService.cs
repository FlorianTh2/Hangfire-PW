using System;

namespace Hangfire_PW.Services
{
    public class DateTimeService : IDateTimeService
    {
        DateTime IDateTimeService.Now => DateTime.UtcNow;
    }
}