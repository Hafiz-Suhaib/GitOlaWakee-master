using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class Degree
    {
        [Key]
        public int DegreeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PreRequisite { get; set; }
        public string EligibleAfter { get; set; }
        public int DegreeTypeId { get; set; }
        // public List<LawyerQualification> LawyerQualifications { get; set; }
        public virtual DegreeType DegreeTypes { get; set; }

        public bool DegreeStatus { get; set; }
    }
}
