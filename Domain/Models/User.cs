using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        public List<Advertisement> Advertisements { get; set; }
        public PersonData PersonData { get; set; }
    }
}
