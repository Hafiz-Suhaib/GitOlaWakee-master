using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class LawyerLanguage
    {
        [Key]
        public int LawyerLanguageId { get; set; }
        public string Language { get; set; }
        public int LanguageNo { get; set; }
        public int LawyerId { get; set; }
        public virtual Lawyer Lawyer { get; set; }
    }
}
