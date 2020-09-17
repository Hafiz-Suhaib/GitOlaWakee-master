using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class FavouriteLawyer
    {
        [Key]
        public int FavouriteLawyerId { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }

        public int LawyerId { get; set; }
        public virtual Lawyer Lawyer { get; set; }
    }
}
