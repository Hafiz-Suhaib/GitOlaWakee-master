using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class Withdraw
    {
        [Key]
        public int Withdraw_Id { get; set; }
        public int Amount { get; set; }
        public string IBAN_Number { get; set; }
        public int UserId { get; set; }
        public string UserType { get; set; }
        public DateTime Date  { get; set; }
        public bool Status { get; set; }
    }
}
