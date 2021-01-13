using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OlaWakeel.Services.LawyerService;

namespace OlaWakeel.Controllers
{
   // [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ILawyerService _lawyerService;
        public DashboardController(ILawyerService lawyerService)
        {
            _lawyerService = lawyerService;
        }
        public async Task<IActionResult> Index()
        {
            ViewBag.TotalLawyersCount = await _lawyerService.TotalLawyersCount();
            ViewBag.OnlineLawyersCount = await _lawyerService.OnlineLawyersCount();
            ViewBag.OfflineLawyersCount = await _lawyerService.OfflineLawyersCount();
            return View();
        }
    }
}