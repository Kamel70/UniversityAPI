using Microsoft.AspNetCore.Identity;

namespace UniversityAPI.Models
{
    public class ApplicationUser:IdentityUser
    {
        public int Age { get; set; }
        public string Address { get; set; }
        public string Image { get; set; }
    }
}
