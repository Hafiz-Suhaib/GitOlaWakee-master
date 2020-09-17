using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OlaWakeel.Data;
using OlaWakeel.Models;

namespace OlaWakeel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LawyerApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public LawyerApiController(ApplicationDbContext context)
        {
            _context = context;
        }


        //[HttpGet]
        //[Route("GetLawyers")]
        //public ActionResult<List<Lawyer>> GetLawyers()
        //{
        //    var casecat = _context.Lawyers.ToList().Select(x => new {
        //        LawyerName = x.FirstName+ x.LastName,
        //        Rating = x.Rating,  
        //        OnlineStatus = x.OnlineStatus,
        //        TotalExperience = x.TotalExperience,
        //        ProfilePic = x.ProfilePic,
        //        categories = x.LawyerCaseCategories.ToList()


        //    });
        //    return Ok(casecat);
        //}
    }
}