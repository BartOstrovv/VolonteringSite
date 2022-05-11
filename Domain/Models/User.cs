using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class User
    {
        public List<Advertisement> Advertisements { get; set; }
        public PersonData PersonData { get; set; }
    }
}
