using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class LicenseDistrict
    {
        [Key]
        public int LicenseDistrictId { get; set; }
        public string DistrictName { get; set; }
    }
}
