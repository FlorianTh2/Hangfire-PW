using System;

namespace hello_hangfire.Services
{
    public interface IDateTimeService
    {
        public DateTime Now { get; }
    }
}