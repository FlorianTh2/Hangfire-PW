using System;

namespace Hangfire_PW.Services
{
    public interface IDateTimeService
    {
        public DateTime Now { get; }
    }
}