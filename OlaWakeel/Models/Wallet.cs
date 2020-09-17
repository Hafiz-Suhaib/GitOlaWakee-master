using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class Wallet
    {
        [Key]
        public int WalletId { get; set; }
        public int UserId { get; set; }
        public string WalletType { get; set; }
        public float WalletAmount { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}
