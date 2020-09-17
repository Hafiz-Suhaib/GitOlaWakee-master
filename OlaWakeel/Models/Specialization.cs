using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class Specialization
    {
        [Key]
        public int SpecializationId { get; set; }
        public string SpecializationName { get; set; }
        public string Description { get; set; }
    }
}
