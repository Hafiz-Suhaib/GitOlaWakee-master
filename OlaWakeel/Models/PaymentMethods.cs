using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class PaymentMethods
    {
        [Key]
        public int PaymentMethod_Id { get; set; }
        public string Payment_Method_Name { get; set; }
        public string Account_Holder_Name { get; set; }
        public string Card_Number { get; set; }
        public string CV_Number { get; set; }
        public int Entered_Amount { get; set; }
        public DateTime Expiry_Date { get; set; }
        public int UserId { get; set; }
        public string UserType { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }


    }
}
