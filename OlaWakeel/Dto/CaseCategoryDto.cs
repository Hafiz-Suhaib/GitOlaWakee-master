using Microsoft.AspNetCore.Http;
using OlaWakeel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Dto
{
    public class CaseCategoryDto
    {
        public int CaseCategoryId { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        // public virtual CaseCategory Parent { get; set; }
        public string Description { get; set; }

      
        /// public virtual List<CaseCategory> Children { get; set; }
    }
}
