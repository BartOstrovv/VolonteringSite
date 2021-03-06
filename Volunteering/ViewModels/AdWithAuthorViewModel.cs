using Domain.Models;

namespace Volunteering.ViewModels
{
    public class AdWithAuthorViewModel
    {
        public int AdId { get; set; }
        public List<Photo>? Images { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public double? CurrentMoney { get; set; }
        public double? NeedMoney { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserSurname { get; set; }
        public string? UserPhotoPath { get; set; }
        public List<Donation> Donations { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
