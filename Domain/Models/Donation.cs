using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Donation
    {
        public int Id { get; set; }
        public User Sender { get; set; }
        public double Sum { get; set; }
        public DateTime DateTime { get; set; }
        public string Comment { get; set; }
    }
}
