using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class DegreeType
    {
        [Key]
        public int DegreeTypeId { get; set; }
        public string TypeName { get; set; }
        //public virtual List<Degree> Degrees { get; set; }
    }
}
