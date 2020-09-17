using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class LawyerLicense
    {
        [Key]
        public int LawyerLicenseId { get; set; }
        public int DistrictBar { get; set; }
        public int CityBar { get; set; }
        //Check will be used in edit mode enable disable
        public bool Check { get; set; }
        public int LicenseCityId { get; set; }
        public virtual LicenseCity LicenseCity { get; set; }
        public int LawyerId { get; set; }
        public virtual Lawyer Lawyer { get; set; }
    }
}
