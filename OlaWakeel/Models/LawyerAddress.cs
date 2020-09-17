using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class LawyerAddress
    {
        [Key]
        public int LawyerAddressId { get; set; }
        public string Address { get; set; }
        public double? Xcoordinate { get; set; }
        public double? Ycoordinate { get; set; }
        public int LawyerId { get; set; }
        public virtual Lawyer Lawyer { get; set; }
    }
}
