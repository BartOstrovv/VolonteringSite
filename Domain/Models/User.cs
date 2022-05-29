using Microsoft.AspNetCore.Identity;

namespace Domain.Models
{
    public class User : IdentityUser
    {
        public List<Advertisement>? Advertisements { get; set; }
        public int? PersonDataId { get; set; }
        public PersonData? PersonData { get; set; }
        public List<Comment>? Comments { get; set; }
        public List<Donation>? Donations { get; set; }
    }
}
