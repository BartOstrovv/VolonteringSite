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
        public List<Photo> Images { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public double CurrentMoney { get; set; }
        public double NeedMoney { get; set; }
        public User Author { get; set; }
        public List<Donation> Donations { get; set; }
        public Address DeliveryAddress { get; set; }
        public List<Comment> Comments { get; set; }
        public bool Close { get; set; }
    }
}
