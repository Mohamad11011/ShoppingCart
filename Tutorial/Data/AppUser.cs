using Microsoft.AspNetCore.Identity;

namespace Tutorial.Data
{
    public class AppUser : IdentityUser
    {
        public string Occupation { get; set; }
    }
}
