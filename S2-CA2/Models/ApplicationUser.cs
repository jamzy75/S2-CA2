using Microsoft.AspNetCore.Identity;

namespace S2_CA2.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Role { get; set; }
    }
}
