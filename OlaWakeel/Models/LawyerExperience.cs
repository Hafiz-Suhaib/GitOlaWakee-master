using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class LawyerExperience
    {
        [Key]
        public int LawyerExperienceId { get; set; }
        public int CaseCategoryId { get; set; }
        public virtual CaseCategory CaseCategory { get; set; }
        public int ExperienceYears { get; set; }
        public int LawyerId { get; set; }
        public virtual Lawyer Lawyer { get; set; }
    }
}
