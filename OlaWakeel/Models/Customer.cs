using OlaWakeel.Data.ApplicationUser;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        //public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public string FirbaseToken { get; set; }
        public string Contact { get; set; }
        public string ProfilePic { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }

        [DataType(DataType.Date)]
        [Column(TypeName = "date")]
        public DateTime Date { get; set; }
        public bool Status { get; set; }
        public int AppUserId { get; set; }
        public virtual AppUser AppUser { get; set; }
        
    }
}
