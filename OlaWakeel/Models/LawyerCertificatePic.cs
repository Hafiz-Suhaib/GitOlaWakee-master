using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class LawyerCertificatePic
    {
        [Key]
        public int LawyerCertificatePicId { get; set; }
        public string CertificatePic { get; set; }
        public int LawyerId { get; set; }
        public virtual Lawyer Lawyer { get; set; }
    }
}
