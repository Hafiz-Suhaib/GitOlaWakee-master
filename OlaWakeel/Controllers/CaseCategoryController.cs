using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OlaWakeel.Dto;
using OlaWakeel.Services.CaseCategoryService;

namespace OlaWakeel.Controllers
{
    public class CaseCategoryController : Controller
    {
        private readonly ICaseCategoryService _service;
        public CaseCategoryController(ICaseCategoryService service)
        {
            _service = service;
        }
        public async Task<IActionResult> Index()
        {

            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCaseList()
        {
            CatDto catDto = new CatDto();
            catDto.CaseCategories = _service.GetAllCaseCategory();
            catDto.CaseCategoryList = await _service.GetAllNonRecursive();
            if (TempData["name"] != null)
            {
                var name = TempData["name"];
                string Name = Convert.ToString(name);
                ModelState.AddModelError(string.Empty, Name + " Category already exist");
            }
            return View(catDto);
        }
        // caseCategory for dropdown list 

        public async Task<JsonResult> GetAllCaseCategoryList()
        {
            var caseList = await _service.GetAllNonRecursive();
            return Json(caseList);
        }

        [HttpPost]
        public async Task<IActionResult> AddCaseCategory(CatDto addCaseCategory)
        {
            if (ModelState.IsValid)
            {
                var result = await _service.AddCaseCategory(addCaseCategory);

                if (result == false)
                {
                    TempData["name"] = addCaseCategory.Name;
                }
                return RedirectToAction("GetAllCaseList");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> EditCaseCategory(CatDto editCaseCategory)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateCaseCategory(editCaseCategory);
                return RedirectToAction("GetAllCaseList");
            }
            return View();
        }
        public async Task<ActionResult> EditCaseCategory(int id)
        {
            CatDto catDto = new CatDto();

            var catlist = await _service.CatGetById(id);
            //ViewBag.CatList =new SelectList(await _service.GetAllNonRecursive(), "CaseCategoryId", "Name");
            catDto.CaseCategoryList = await _service.GetAllNonRecursive();
            catDto.CaseCategoryId = catlist.CaseCategoryId;
            catDto.Name = catlist.Name;
            catDto.Description = catlist.Description;
            catDto.ParentId = catlist.ParentId;
            catDto.VectorIcon = catlist.VectorIcon;
            return View(catDto);


        }

        public async Task<ActionResult> Delete(int id)
        {
            if (ModelState.IsValid)
            {
                await _service.DeleteCaseCategory(id);
                return RedirectToAction("GetAllCaseList");
            }
            return View();
        }
    }
}