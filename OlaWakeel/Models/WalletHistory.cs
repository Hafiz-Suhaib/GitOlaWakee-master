using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class WalletHistory
    {
        [Key]
        public int WalletHistoryId { get; set; }
        public int WalletId { get; set; }
        public virtual Wallet WalletType { get; set; }
        public float WalletHistoryAmount { get; set; }
        public string WalletHistoryDisc { get; set; }
        public DateTime Date { get; set; }
        public bool Status { get; set; }
    }
}
