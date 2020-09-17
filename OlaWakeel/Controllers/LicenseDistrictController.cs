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
    public class LicenseDistrictController : Controller
    {
        private readonly ILicenseDistrictService _service;
        private readonly ILicenseCityService _licenseCityService;
        public LicenseDistrictController(ILicenseDistrictService service, ILicenseCityService licenseCityService)
        {
            _service = service;
            _licenseCityService = licenseCityService;
        }
        public async Task<IActionResult> Index()
        {
            DistrictCityDto dto = new DistrictCityDto();
            dto.LicenseDistricts = await _service.GetAllLicenseDistrict();
            dto.LicenseCities = await _licenseCityService.GetAllLicenseCity();
            return View(dto);
        }
        public async Task<IActionResult> GetAllLicenseDistrict()
        {

            return View();
        }
        public async Task<IActionResult> Create()
        {
            ViewBag.getAllDistrict = await _service.GetAllLicenseDistrict();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LicenseDistrict licenseDistrict)
        {
            if (ModelState.IsValid)
            {
                await _service.AddLicenseDistrict(licenseDistrict);
            }
            return RedirectToAction("Index", "LicenseDistrict");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var district = await _service.GetById(id);
            return View(district);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(LicenseDistrict licenseDistrict)
        {
            await _service.EditLicenseDistrict(licenseDistrict);
            return RedirectToAction("Index", "LicenseDistrict");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction("Index", "LicenseDistrict");
        }
    }
}