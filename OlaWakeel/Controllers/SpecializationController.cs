using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using OlaWakeel.Data;
using OlaWakeel.Models;
using OlaWakeel.Services.ISpecializationService.cs;

namespace OlaWakeel.Controllers
{
    //[Authorize(Roles = "Admin")]
    public class SpecializationController : Controller
    {
        private readonly ISpecializationService _service;
        public SpecializationController(ISpecializationService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Specialization addSpecilization)
        {
            bool dup = false;
            var list = await _service.GetSpecializations();
            foreach (var check in list)
            {
               if(check.SpecializationName == addSpecilization.SpecializationName)
                {
                    dup = true;
                    break;
                    
                }
               
            }
            if(!dup)
            {
                await _service.AddSpecialization(addSpecilization);
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.error = "This name already exist";
                return View();

            }
            return RedirectToAction("Create");
            
        }
        [HttpGet]
        public async Task <IActionResult> Index()
        {
           var details =  await _service.GetSpecializations();
           return View(details);
        }
        public async Task<JsonResult> SpecializationList() 
        {
            var allSpecialization = await _service.GetSpecializations();
            return Json(allSpecialization);
        }

        [HttpGet]
        public async Task<IActionResult> edit(int id)
        {
            var detailid = await _service.GetById(id);

            return View(detailid);
        }

        [HttpPost]

        public async Task<IActionResult> edit(Specialization editSpecialization,string old)
        {
            bool dup = false;
            var list = await _service.GetSpecializations();
            Specialization temp = new Specialization();
            foreach (var check in list)
            {
                if(check.SpecializationName == old)
                {
                    temp = check;
                }
                if (check.SpecializationName == editSpecialization.SpecializationName && check.SpecializationName != old)
                {
                    dup = true;
                    
                }
            }
            if(dup)
            {
                ViewBag.error = "This name already exist";
                return View(temp);
            }
            else
            {
                await _service.EditSpecialization(editSpecialization);
                return RedirectToAction("Index");
            }
            
           // return RedirectToAction("edit");

           
        }


        [HttpGet]

        public async Task<IActionResult> delete(int id)
        {
            await _service.Delete(id);
            return RedirectToAction("index");
        }
    }
}