using Microsoft.EntityFrameworkCore;

namespace Hangfire_PW.Data
{
    public class DataContext
    {
        public DbSet<Project> Projects { get; set; }

        public DbSet<Document> Documents { get; set; }
    }
}