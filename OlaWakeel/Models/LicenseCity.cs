using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class LicenseCity
    {
        [Key]
        public int LicenseCityId { get; set; }
        public string CityName { get; set; }
        public bool LicenseExist { get; set; }
        public int LicenseDistrictId { get; set; }
        public virtual LicenseDistrict LicenseDistrict { get; set; }
    }
}
