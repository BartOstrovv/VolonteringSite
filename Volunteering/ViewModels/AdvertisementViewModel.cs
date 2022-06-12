namespace Volunteering.ViewModels
{
    public class AdvertisementViewModel
    {
        public List<IFormFile> Photos { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public double NeedMoney { get; set; }
        public string DeliveryCountry { get; set; }
        public string DeliveryCity { get; set; }
        public string DeliveryStreet { get; set; }
        public int DeliveryBuild { get; set; }

        public bool IsEmpty() =>
            Photos == null                     &&
            CreatedDate == DateTime.MinValue        &&
            string.IsNullOrEmpty(Title)             &&
            string.IsNullOrEmpty(Body)              &&
            NeedMoney <= 0                          &&
            string.IsNullOrEmpty(DeliveryCountry)   &&
            string.IsNullOrEmpty(DeliveryCity)      &&
            string.IsNullOrEmpty(DeliveryStreet)    &&
            DeliveryBuild <= 0;
    }
}
