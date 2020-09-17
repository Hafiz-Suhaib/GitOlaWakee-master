using OlaWakeel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Dto
{
    public class DistrictCityDto
    {
        public string CityName { get; set; }
        public bool LicenseExist { get; set; }
        public int LicenseDistrictId { get; set; }
        public string DistrictName { get; set; }
        public List<LicenseDistrict> LicenseDistricts { get; set; }
        public List<LicenseCity> LicenseCities  { get; set; }
    }
}
