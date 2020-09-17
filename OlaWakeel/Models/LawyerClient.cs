using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class LawyerClient
    {
        [Key]
        public int LawyerClientId { get; set; }
        public string ClientName { get; set; }
        public int LawyerId { get; set; }
        public virtual Lawyer Lawyer { get; set; }
    }
}
