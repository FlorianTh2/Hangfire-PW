using System;

namespace hello_hangfire.Entities
{
    public class AuditableEntity
    {
        public DateTime CreatedOn { get; set; }

        public string CreatorId { get; set; }

        public DateTime? UpdatedOn { get; set; }

        public string UpdaterId { get; set; }
    }
}