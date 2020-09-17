using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class ContactUs
    {
        [Key]
        public int ContactUs_Id { get; set; }
        public int User_Id { get; set; }
        public string User_Type { get; set; }
        public string ContactUs_Name { get; set; }
        public string ContactUs_Email { get; set; }
        public string ContactUs_Massage { get; set; }
        public string ContactUs_PhoneNo { get; set; }
        public bool Status { get; set; }
        public DateTime Date { get; set; }
    }
}
