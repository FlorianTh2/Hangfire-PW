using Microsoft.AspNetCore.Identity;

namespace hello_hangfire.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
    }
}