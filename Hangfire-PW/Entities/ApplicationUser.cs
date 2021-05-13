using Microsoft.AspNetCore.Identity;

namespace Hangfire_PW.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string City { get; set; }
    }
}