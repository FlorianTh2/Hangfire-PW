using Microsoft.EntityFrameworkCore;

namespace hello_hangfire.Data
{
    public class HangfireDbContext : DbContext
    {
        public HangfireDbContext(DbContextOptions<HangfireDbContext> options) : base(options)
        {
            
        }
    }
}