using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OlaWakeel.Dto;
using OlaWakeel.Models;
using OlaWakeel.Services.LicenseCityService;
using OlaWakeel.Services.LicenseDistrictService;

namespace OlaWakeel.Controllers
{
    public class LicenseCityController : Controller
    {
        private readonly ILicenseCityService _service;
        private readonly ILicenseDistrictService _districtService;
        public LicenseCityController(ILicenseCityService service, ILicenseDistrictService districtService)
        {
            _service = service;
            _districtService = districtService;
        }
        public async Task<IActionResult> Index()
        {
            DistrictCityDto dto = new DistrictCityDto();
            dto.LicenseCities = await _service.GetAllLicenseCity();
            return View(dto);
        }
        public async Task<JsonResult> GetAllLicenseCity()
        {

            var all = await _service.GetAllLicenseCity();
            return Json(all);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.getAllDistrict = await _districtService.GetAllLicenseDistrict();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(LicenseCity licenseCity)
        {
            if (ModelState.IsValid)
            {
                await _service.AddLicenseCity(licenseCity);
            }
            return RedirectToAction("Index", "LicenseDistrict");

        }
        public async Task<IActionResult> Edit(int id) 
        {
            ViewBag.getAllDistrict = await _districtService.GetAllLicenseDistrict();
            var city = await _service.GetById(id);
            return View(city);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(LicenseCity licenseCity)
        {
            await _service.EditLicenseCity(licenseCity);
            return RedirectToAction("Index", "LicenseDistrict");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction("Index", "LicenseDistrict");
        }
    }
}