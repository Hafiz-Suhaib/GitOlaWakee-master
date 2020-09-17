using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Models
{
    public class CaseCategory
    {
        //[Key]
        public int CaseCategoryId { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        // public virtual CaseCategory Parent { get; set; }
        public string Description { get; set; }

        public string VectorIcon { get; set; }

        public virtual List<CaseCategory> Children { get; set; }
       // public virtual IEnumerable<CaseCategoryType> CaseCategories { get; set; }
    }
}
