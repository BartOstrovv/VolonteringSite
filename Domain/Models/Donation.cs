using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Donation
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? AdvertisementId { get; set; }
        public string UserId { get; set; }
        public double Sum { get; set; }
        public DateTime DateTime { get; set; }
        public string Comment { get; set; }
    }
}
