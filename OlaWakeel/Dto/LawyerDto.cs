using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Dto
{
    public class LawyerDto
    {
        public string UserName { get; set; }
        public string Email{ get; set; }
        public int LawyerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Contact { get; set; }
        public string Cnic { get; set; }
        public string ProfilePic { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public float Rating { get; set; }
        public float VirtualChargesPkr { get; set; }
        public float VirtualChargesUs { get; set; }
        public int TotalExperience { get; set; }
        public string OnlineStatus { get; set; }
        public int AppUserId { get; set; }
    }
}
