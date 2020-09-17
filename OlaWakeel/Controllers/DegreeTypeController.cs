using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OlaWakeel.Dto.DegreeDto;
using OlaWakeel.Models;
using OlaWakeel.Services.DegreeTypeService;

namespace OlaWakeel.Controllers
{
    public class DegreeTypeController : Controller
    {
        private readonly IDegreeTypeService _service;

        public DegreeTypeController(IDegreeTypeService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {
            var value = await _service.GetAllDegrees();
            return View(value);
        }
        public async Task<JsonResult> GetAllDegreeTypes()
        {
            var allType = await _service.GetAllDegrees();
            return Json(allType);
        }
        [HttpGet]
        [ActionName("Create")]
        public async Task<IActionResult> CreateDegreeType()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateDegreeType(DegreeType addDegree)
        {
            await _service.AddDegreeType(addDegree);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var value = await _service.SingleOrDefaultDT(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(DegreeType degreeType)
        {
            _service.EditDegree(degreeType);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
