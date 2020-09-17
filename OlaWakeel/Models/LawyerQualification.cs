using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class LawyerQualification
    {
        [Key]
        public int LawyerQualificationId { get; set; }
        public string CompletionYear { get; set; }
        public int LawyerId { get; set; }
        public virtual Lawyer Lawyer { get; set; }
        //DegreeTypeId will be used in edit mode dropdown get selected,nothing else
        public int DegreeTypeId { get; set; }
        public int DegreeId { get; set; }
        public virtual Degree Degree { get; set; }
        //Check will be used in edit mode enable disable
        public bool Check { get; set; }
        public int? SpecializationId { get; set; }
        public virtual Specialization Specialization { get; set; }
    }
}
