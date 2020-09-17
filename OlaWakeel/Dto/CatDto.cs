using Microsoft.AspNetCore.Http;
using OlaWakeel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Dto
{
    public class CatDto
    {
        public int CaseCategoryId { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }

        public string Description { get; set; }
        public string VectorIcon { get; set; }
        public IFormFile Image { get; set; }
        public virtual IEnumerable<CaseCategory> CaseCategories { get; set; }
        public virtual List<CaseCategory> CaseCategoryList { get; set; }
    }
}
