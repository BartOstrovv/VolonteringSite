using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Models
{
    public class Transfer
    {
        public int Id { get; set; }
        public PersonData Sender { get; set; }
        public double Sum { get; set; }
        public DateTime DateTime { get; set; }
        public string Comment { get; set; }
    }
    public class TransferHistory
    {
        public int Id { get; set; }
        public List<Transfer> Transfers { get; set; }
    }
}
