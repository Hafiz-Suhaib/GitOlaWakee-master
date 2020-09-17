using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Dto.Auth
{
    public class RegisterDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string email { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public string phoneNumber { get; set; }
        public string RoleName { get; set; }
    }
}
