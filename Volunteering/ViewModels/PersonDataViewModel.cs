using Domain.Models;

namespace Volunteering.ViewModels
{
    public class PersonDataViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public IFormFile Photo { get; set; }
        public string Mobile { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public int Build { get; set; }

        public bool IsEmpty() =>
            string.IsNullOrEmpty(Name) &&
            string.IsNullOrEmpty(Surname) &&
            ((Age == 0) && (Photo == null)) &&
            string.IsNullOrEmpty(Mobile) &&
            string.IsNullOrEmpty(Country) &&
            string.IsNullOrEmpty(City) &&
            string.IsNullOrEmpty(Street) &&
            Build == 0;

        public void Init(PersonData? data)
        {
            if (data == null)
                return;
            this.Street = data.Address.Street;
            this.City = data.Address.City;
            this.Country = data.Address.Country;
            this.Build = data.Address.Build;
            this.Name = data.Name;
            this.Age = data.Age;
            this.Surname = data.Surname;
            this.Mobile = data.Mobile;
        }
    }
}
