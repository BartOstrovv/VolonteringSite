using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class PersonData
    {
        public int Id { get; set; }
        public Photo? Photo { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age { get; set; }
        public string Mobile { get; set; }
        public Address Address { get; set; }
        public double Money { get; set; }
    }
}
