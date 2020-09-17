using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.VisualBasic.CompilerServices;
using OlaWakeel.Models;
using OlaWakeel.Services.DegreeService;
using OlaWakeel.Services.DegreeTypeService;

namespace OlaWakeel.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class DegreeController : Controller
    {
        private readonly IDegreeService _service;
        private readonly IDegreeTypeService _serviceDegreeType;

        public DegreeController(IDegreeService service, IDegreeTypeService serviceDegreeType)
        {
            _service = service;
            _serviceDegreeType = serviceDegreeType;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.degreeList = await _service.GetAllDegrees();
            var degreeType = await _serviceDegreeType.GetAllDegrees();
            ViewBag.DegreeType = new SelectList(degreeType, "DegreeTypeId", "TypeName");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Degree addDegree)
        {
            bool dup = false;
            var list = await _service.GetAllDegrees();
            foreach (var check in list)
            {
                if (check.Name == addDegree.Name)
                {
                    dup = true;
                    break;
                }
            }
            if (dup)
            {
                ViewBag.error = "This name already exists";
                ViewBag.degreeList = await _service.GetAllDegrees();
                return View();
            }
            else
            {
                await _service.AddDegree(addDegree);
                return RedirectToAction("Index");
            }

            return RedirectToAction("Create");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var details = await _service.GetAllDegrees();
            ViewBag.DegreeType = await _serviceDegreeType.GetAllDegrees();
            return View(details);
        }
        public async Task<JsonResult> DegreeList()
        {
            var degreeList = await _service.GetAllDegrees();
            return Json(degreeList);
        }

        [HttpGet]
        public async Task<IActionResult> edit(int id)
        {
            var degreeById = await _service.SingleOrDefaultDG(id);
            ViewBag.DegreeTypesList = await _serviceDegreeType.GetAllDegrees();
            ViewBag.degreeList = await _service.GetAllDegrees();
            //var value = new Degree
            //{
            //    Description = degreeById.Description,
            //    EligibleAfter = degreeById.EligibleAfter,
            //    Name = degreeById.Name,
            //    PreRequisite = degreeById.PreRequisite,
            //    DegreeTypeId = degreeById.DegreeTypeId,
            //    DegreeTypes = degreeById.DegreeTypes
            //};
           // ViewBag.Value = DegreeType.TypeName;
            ViewData["old"] = degreeById;
            return View(degreeById);
        }

        [HttpPost]
        public async Task<IActionResult> edit(int Id, Degree editDegree, string old)
        {
            bool dup = false;
            var list = await _service.GetAllDegrees();
            Degree temp = new Degree();
            foreach (var check in list)
            {
                if (check.Name == old)
                {
                    temp = check;
                }

                if (check.Name == editDegree.Name && check.Name != old)
                {
                    dup = true;
                }
            }
            if (dup)
            {
                ViewBag.error = "This name already exists";
                ViewBag.degreeList = await _service.GetAllDegrees();
                ViewBag.DegreeTypesList = await _serviceDegreeType.GetAllDegrees();
                return View(temp);
            }
            else
            {
                await _service.EditDegree(Id, editDegree);
                return RedirectToAction("index");
            }

            return RedirectToAction("index");
        }


        [HttpGet]
        public async Task<IActionResult> delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction("index");
        }
    }
}