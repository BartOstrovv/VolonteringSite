using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Advertisement
    {
        public int Id { get; set; }
        public List<PhotoPath> Images { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public TransferHistory History { get; set; }
        public double CurrentMoney { get; set; }
        public double NeedMoney { get; set; }
        public PersonData Author { get; set; }
        public List<PersonData> Volonters { get; set; }
        public bool NeedDelivery { get; set; }
        public PersonData Supplier { get; set; }
        public Address DeliveryAddress { get; set; }
        public bool Close { get; set; }
    }
}
