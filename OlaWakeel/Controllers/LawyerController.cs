using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OlaWakeel.Data;
//using OlaWakeel.Constant;
using OlaWakeel.Data.ApplicationUser;
using OlaWakeel.Dto;
using OlaWakeel.Models;
using OlaWakeel.Services.CaseCategoryService;
using OlaWakeel.Services.DegreeService;
using OlaWakeel.Services.ISpecializationService.cs;
using OlaWakeel.Services.LawyerService;
using OlaWakeel.ViewModels;

namespace OlaWakeel.Controllers
{
    // [Authorize(Roles = "Admin")]
    public class LawyerController : Controller
    {
        [BindProperty]
        public AppointmentVM AVM { get; set; }
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public readonly ILawyerService _lawyerService;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IDegreeService _degreeService;
        private readonly ISpecializationService _specializationService;
        private readonly ICaseCategoryService _caseCategoryService;
        public LawyerController(ApplicationDbContext context, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, ILawyerService lawyerService, IHostingEnvironment hostingEnvironment, IDegreeService degreeService, ISpecializationService specializationService, ICaseCategoryService caseCategoryService)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _lawyerService = lawyerService;
            _hostingEnvironment = hostingEnvironment;
            _degreeService = degreeService;
            _specializationService = specializationService;
            _caseCategoryService = caseCategoryService;
            AVM = new AppointmentVM();

        }
        public IActionResult Index()
        {
            var f = _context.Lawyers.ToList();
            return View(f);
        }
        public IActionResult CreateLawyer()
        {
            return View();
        }
        public async Task<IActionResult> ActiveLawyer()
        {
            try

            {
                // var j = 0;
                var a = _context.Lawyers.Where(a => a.OnlineStatus == "Online").ToList();
                // var b= a.Count();
                return View(a);

            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }
        }
        public async Task<IActionResult> BlockedLawyer()
        {
            try

            {
                // var j = 0;
                var a = _context.Lawyers.Where(a => a.Status == false).ToList();
                // var b= a.Count();
                return View(a);

            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }
        }
        [HttpPost]
        public async Task<JsonResult> CheckUserAvailabilty(IFormCollection form)
        {
            var appUser = JsonConvert.DeserializeObject<AppUser>(form["appUser"]);
            AppUser user = await _userManager.FindByNameAsync(appUser.UserName);
            if (user != null)
            {
                return Json("Unsuccess");
            }
            return Json("Success");
        }

        [HttpPost]
        public async Task<JsonResult> CreateLawyer2()
        {
            var appUser = JsonConvert.DeserializeObject<AppUser>(Request.Form["appUser"]);
            var lawyer = JsonConvert.DeserializeObject<Lawyer>(Request.Form["lawyer"]);
            var lawyerLanguage = JsonConvert.DeserializeObject<List<LawyerLanguage>>(Request.Form["lawyerLanguages"]);
            var lawyerQualification = JsonConvert.DeserializeObject<List<LawyerQualification>>(Request.Form["lawyerQualifications"]);
            //var lawyerSpecializations = JsonConvert.DeserializeObject<List<LawyerSpecialization>>(Request.Form["lawyerSpecializations"]);
            var lawyerExperiences = JsonConvert.DeserializeObject<List<LawyerExperience>>(Request.Form["lawyerExperiences"]);
            //try {
            //    var lawyerTimings2 = JsonConvert.DeserializeObject<List<LawyerTiming>>(Request.Form["lawyerTimings"]);
            //}
            //catch(Exception ex)
            //{

            //}
            var lawyerTimings = JsonConvert.DeserializeObject<List<LawyerTiming>>(Request.Form["lawyerTimings"]);
            var lawyerCaseCategory = JsonConvert.DeserializeObject<List<LawyerCaseCategory>>(Request.Form["lawyerCaseCategory"]);
            var lawyerClient = JsonConvert.DeserializeObject<List<LawyerClient>>(Request.Form["lawyerClient"]);
            var lawyerLicense = JsonConvert.DeserializeObject<List<LawyerLicense>>(Request.Form["lawyerLicenses"]);
            var lawyerAddress = JsonConvert.DeserializeObject<List<LawyerAddress>>(Request.Form["lawyerAddress"]);
            var AddressesTemp = JsonConvert.DeserializeObject<List<string>>(Request.Form["addressesTemp"]);
            var file = Request.Form.Files[0];
            // var appUser =               JsonConvert.DeserializeObject<AppUser>(form["appUser"]);
            // var lawyer =                JsonConvert.DeserializeObject<Lawyer>(form["lawyer"]);
            // var lawyerVirtualFee =      JsonConvert.DeserializeObject<LawyerVirtualFee>(form["lawyerVirtualFee"]);
            // var lawyerQualification =   JsonConvert.DeserializeObject<List<LawyerQualification>>(form["lawyerQualifications"]);
            // var lawyerSpecializations = JsonConvert.DeserializeObject<List<LawyerSpecialization>>(form["lawyerSpecializations"]);
            // var lawyerExperiences =     JsonConvert.DeserializeObject<List<LawyerExperience>>(form["lawyerExperiences"]);
            // var lawyerTimings =         JsonConvert.DeserializeObject<List<LawyerTiming>>(form["lawyerTimings"]);

            lawyer.LawyerQualifications = lawyerQualification;
            // lawyer.LawyerSpecializations = lawyerSpecializations;
            lawyer.LawyerExperiences = lawyerExperiences;
            //lawyer.LawerTimings = lawyerTimings;
            lawyer.LawyerCaseCategories = lawyerCaseCategory;
            lawyer.LawyerClients = lawyerClient;
            lawyer.lawyerLanguages = lawyerLanguage;
            lawyer.LawyerLicenses = lawyerLicense;
            // lawyer.LawyerAddresses = lawyerAddress;
            try
            {
                AppUser user = await _userManager.FindByNameAsync(appUser.UserName);
                if (user == null)
                {
                    user = new AppUser
                    {
                        UserName = appUser.UserName,
                        Email = appUser.Email

                    };

                    IdentityResult result = await _userManager.CreateAsync(user, appUser.PasswordHash);

                    ViewBag.Message = "User successfully created!";

                    if (result.Succeeded)
                    {
                        lawyer.AppUserId = user.Id;
                        await _userManager.AddToRoleAsync(user, "Lawyer");

                        string UniqueFilename;
                        if (file != null)
                        {
                            string UploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");

                            if (!Directory.Exists(UploadFolder))
                            {
                                Directory.CreateDirectory(UploadFolder);
                            }

                            UniqueFilename = Guid.NewGuid().ToString() + "_" + file.FileName;
                            string filePath = Path.Combine(UploadFolder, UniqueFilename);
                            file.CopyTo(new FileStream(filePath, FileMode.Create));
                            lawyer.ProfilePic = UniqueFilename;
                            lawyer.OnlineStatus = "Online";
                            //category.Image = UniqueFilename;
                            //await _service.AddDishCategory(category);
                        }

                        await _lawyerService.AddLawyer(lawyer, lawyerAddress, lawyerTimings, AddressesTemp);
                        return Json("Success");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return Json("success");
        }
        [HttpPost]
        public async Task<JsonResult> EditLawyer()
        {

            var appUser = JsonConvert.DeserializeObject<AppUser>(Request.Form["appUser"]);
            var lawyer = JsonConvert.DeserializeObject<Lawyer>(Request.Form["lawyer"]);
            var lawyerLanguage = JsonConvert.DeserializeObject<List<LawyerLanguage>>(Request.Form["lawyerLanguages"]);
            var lawyerQualification = JsonConvert.DeserializeObject<List<LawyerQualification>>(Request.Form["lawyerQualifications"]);
            //var lawyerSpecializations = JsonConvert.DeserializeObject<List<LawyerSpecialization>>(Request.Form["lawyerSpecializations"]);
            var lawyerExperiences = JsonConvert.DeserializeObject<List<LawyerExperience>>(Request.Form["lawyerExperiences"]);
            var lawyerTimings = JsonConvert.DeserializeObject<List<LawyerTiming>>(Request.Form["lawyerTimings"]);
            var lawyerCaseCategory = JsonConvert.DeserializeObject<List<LawyerCaseCategory>>(Request.Form["lawyerCaseCategory"]);
            var lawyerClient = JsonConvert.DeserializeObject<List<LawyerClient>>(Request.Form["lawyerClient"]);
            var lawyerLicense = JsonConvert.DeserializeObject<List<LawyerLicense>>(Request.Form["lawyerLicenses"]);
            var lawyerAddress = JsonConvert.DeserializeObject<List<LawyerAddress>>(Request.Form["lawyerAddress"]);



            //  lawyer.LawyerQualifications = lawyerQualification;
            // lawyer.LawyerSpecializations = lawyerSpecializations;
            //  lawyer.LawyerExperiences = lawyerExperiences;
            //   lawyer.LawerTimings = lawyerTimings;
            //  lawyer.LawyerCaseCategories = lawyerCaseCategory;
            //   lawyer.LawyerClients = lawyerClient;
            //    lawyer.lawyerLanguages = null;
            //    lawyer.LawyerLicenses = lawyerLicense;
            //     lawyer.LawyerAddresses = lawyerAddress;

            try
            {
                string userId = lawyer.AppUserId.ToString();
                var user = await _userManager.FindByIdAsync(userId);
                //AppUser user = await _userManager.FindByNameAsync(appUser.UserName);
                if (user != null)
                {

                    user.UserName = appUser.UserName;
                    user.Email = appUser.Email;


                    IdentityResult result = await _userManager.UpdateAsync(user);
                    //IdentityResult result = await _userManager.CreateAsync(user, appUser.PasswordHash);

                    ViewBag.Message = "User successfully edited!";

                    if (result.Succeeded)
                    {
                        //lawyer.AppUserId = user.Id;
                        //await _userManager.AddToRoleAsync(user, "Lawyer");

                        string UniqueFilename = null;
                        if (Request.Form.Files.Count > 0)
                        {
                            var file = Request.Form.Files[0];
                            string UploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "Uploads");

                            if (!Directory.Exists(UploadFolder))
                            {
                                Directory.CreateDirectory(UploadFolder);
                            }

                            UniqueFilename = Guid.NewGuid().ToString() + "_" + file.FileName;
                            string filePath = Path.Combine(UploadFolder, UniqueFilename);
                            file.CopyTo(new FileStream(filePath, FileMode.Create));

                            //var delpath = Path.Combine(UploadFolder, lawyer.ProfilePic);
                            //if (System.IO.File.Exists(delpath))
                            //{
                            //    System.IO.File.Delete(delpath);
                            //}
                            //lawyer.ProfilePic = UniqueFilename;
                            //lawyer.OnlineStatus = "Online";
                            //category.Image = UniqueFilename;
                            //await _service.AddDishCategory(category);
                        }

                        await _lawyerService.LawyerEdit(lawyer, lawyerQualification, lawyerLanguage, lawyerExperiences, lawyerLicense, lawyerClient, lawyerAddress, lawyerTimings, lawyerCaseCategory, UniqueFilename);
                        return Json("Success");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return Json("Success");
        }
        public async Task<IActionResult> EditLawyer(int id)
        {
            var lawyer = await _lawyerService.GetLawyerId(id);
            //    var getProfileAccount = await _lawyerService.GetAccountProfile(id);
            //    ViewBag.degreeList = await _degreeService.GetAllDegrees();
            //    ViewBag.SpecializationList = await _specializationService.GetSpecializations();
            //    ViewBag.CaseCatList = await _caseCategoryService.GetAllNonRecursive();
            //    List<TimeList> timeLists = new List<TimeList>
            //{
            //    new TimeList   { Id= "12:00 AM", Time= "12:00 AM" },
            //    new TimeList   { Id= "01:00 AM", Time= "01:00 AM" },
            //    new TimeList   { Id= "02:00 AM", Time= "02:00 AM" },
            //    new TimeList   { Id= "03:00 AM", Time= "03:00 AM" },
            //    new TimeList   { Id= "04:00 AM", Time= "04:00 AM" },
            //    new TimeList   { Id= "05:00 AM", Time= "05:00 AM" },
            //    new TimeList   { Id= "06:00 AM", Time= "06:00 AM" },
            //    new TimeList   { Id= "07:00 AM", Time= "07:00 AM" },
            //    new TimeList   { Id= "08:00 AM", Time= "08:00 AM" },
            //    new TimeList   { Id= "09:00 AM", Time= "09:00 AM" },
            //    new TimeList   { Id= "10:00 AM", Time= "10:00 AM" },
            //    new TimeList   { Id= "11:00 AM", Time= "11:00 AM" },
            //    new TimeList   { Id= "12:00 PM", Time= "12:00 PM" },
            //    new TimeList   { Id= "01:00 PM", Time= "01:00 PM" },
            //    new TimeList   { Id= "02:00 PM", Time= "02:00 PM" },
            //    new TimeList   { Id= "03:00 PM", Time= "03:00 PM" },
            //    new TimeList   { Id= "04:00 PM", Time= "04:00 PM" },
            //    new TimeList   { Id= "05:00 PM", Time= "05:00 PM" },
            //    new TimeList   { Id= "06:00 PM", Time= "06:00 PM" },
            //    new TimeList   { Id= "07:00 PM", Time= "07:00 PM" },
            //    new TimeList   { Id= "08:00 PM", Time= "08:00 PM" },
            //    new TimeList   { Id= "09:00 PM", Time= "09:00 PM" },
            //    new TimeList   { Id= "10:00 PM", Time= "10:00 PM" },
            //    new TimeList   { Id= "11:00 PM", Time= "11:00 PM" }
            //};
            //    ViewBag.TimeList = timeLists;
            ViewBag.CaseCatList = await _caseCategoryService.GetAllNonRecursive();
            //ViewBag.LawyerCaseCat = await _lawyerService.GetCaseCatIds(lawyer.LawyerId);
            return View(lawyer);
        }
        [HttpPost]
        public async Task<JsonResult> AddLawyerAddress(IFormCollection form)
        {
            var lawyerAddresses = JsonConvert.DeserializeObject<List<LawyerAddress>>(form["lawyerAddresses"]);
            try
            {
                await _lawyerService.AddLawyerOffice(lawyerAddresses);
                return Json("Success");
            }
            catch (Exception ex)
            {

            }
            return Json("UnSuccess");
        }
        [HttpPost]
        public async Task<IActionResult> EditLawyerAccount(LawyerDto lawyerDto, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string userId = lawyerDto.AppUserId.ToString();
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with Id =  { userId } can not be found";
                    return View("Not Found");
                }
                else
                {
                    user.Email = lawyerDto.Email;
                    user.UserName = lawyerDto.UserName;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        await _lawyerService.EditLawyerAccount(lawyerDto, file);
                        return RedirectToAction("EditLawyer", "Lawyer", new { @id = lawyerDto.LawyerId });

                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }


            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddOrEditLawyerQualification(LawyerQualification lawyerQualification)
        {
            if (ModelState.IsValid)
            {
                if (lawyerQualification.LawyerQualificationId != 0)
                {
                    await _lawyerService.EditLawyerQualification(lawyerQualification);
                    return RedirectToAction("EditLawyer", "Lawyer", new { @id = lawyerQualification.LawyerId });
                }
                else
                {
                    await _lawyerService.AddLawyerQualification(lawyerQualification);
                    return RedirectToAction("EditLawyer", "Lawyer", new { @id = lawyerQualification.LawyerId });
                }


            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddOrEditLawyerSpecialization(LawyerSpecialization lawyerSpecialization)
        {
            if (ModelState.IsValid)
            {
                if (lawyerSpecialization.LawyerSpecializationId != 0)
                {
                    await _lawyerService.EditLawyerSpcecialization(lawyerSpecialization);
                    return RedirectToAction("EditLawyer", "Lawyer", new { @id = lawyerSpecialization.LawyerId });

                }
                else
                {
                    await _lawyerService.AddLawyerSpcecialization(lawyerSpecialization);
                    return RedirectToAction("EditLawyer", "Lawyer", new { @id = lawyerSpecialization.LawyerId });
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEditLawyerExperience(LawyerExperience lawyerExperience)
        {
            if (ModelState.IsValid)
            {
                if (lawyerExperience.LawyerExperienceId != 0)
                {
                    await _lawyerService.EditLawyerExperience(lawyerExperience);
                    return RedirectToAction("EditLawyer", "Lawyer", new { @id = lawyerExperience.LawyerId });

                }
                else
                {
                    await _lawyerService.AddLawyerExperience(lawyerExperience);
                    return RedirectToAction("EditLawyer", "Lawyer", new { @id = lawyerExperience.LawyerId });
                }
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEditLawyerTiming(LawyerTiming lawyerTiming)
        {
            if (ModelState.IsValid)
            {
                if (lawyerTiming.LawyerTimingId != 0)
                {
                    await _lawyerService.EditLawyerTiming(lawyerTiming);
                    return RedirectToAction("EditLawyer", "Lawyer", new { @id = lawyerTiming.LawyerId });

                }
                else
                {
                    await _lawyerService.AddLawyerTiming(lawyerTiming);
                    return RedirectToAction("EditLawyer", "Lawyer", new { @id = lawyerTiming.LawyerId });
                }
            }
            return View();
        }
        [HttpPost]
        public async Task<JsonResult> EditLawyerCaseCategory(IFormCollection form)
        {
            var lawyerCaseCat = JsonConvert.DeserializeObject<List<LawyerCaseCategory>>(form["lawyerCaseCategory"]);
            var lawyerId = JsonConvert.DeserializeObject<int>(form["lawyerId"]);
            if (lawyerCaseCat != null)
            {
                await _lawyerService.EditLawyerCasecategory(lawyerCaseCat);
                return Json("Success");
            }

            return Json("");
        }
        [HttpPost]
        public async Task<JsonResult> GetCaseCatIds(int id)
        {
            var caseCatIds = await _lawyerService.GetCaseCatIds(id);
            return Json(caseCatIds);
        }
        public async Task<JsonResult> DeleteLawyerQualification(int id)
        {
            await _lawyerService.DeleteLawyerQualification(id);
            return Json("Success");
        }
        public async Task<JsonResult> DeleteLawyerLicense(int id)
        {
            await _lawyerService.DeleteLawyerLicense(id);
            return Json("Success");
        }
        public async Task<JsonResult> DeleteLawyerClient(int id)
        {
            await _lawyerService.DeleteLawyerClient(id);
            return Json("Success");
        }
        public async Task<JsonResult> DeleteLawyerAddress(int id)
        {
            await _lawyerService.DeleteLawyerAddress(id);
            return Json("Success");
        }
        public async Task<JsonResult> DeleteLawyerExperience(int id)
        {
            await _lawyerService.DeleteLawyerExperience(id);
            return Json("Success");
        }
        public async Task<IActionResult> DeleteLawyerTiming(int id)
        {
            await _lawyerService.DeleteLawyerTiming(id);
            return Json("Success");
        }
        public async Task<IActionResult> GetAllLawyers()
        {
            var lawyers = await _lawyerService.GetAllLawyers();
            return View(lawyers);
        }
        public async Task<IActionResult> LawyerProfile(int id)
        {
            var lawyerProfile = await _lawyerService.LawyerProfile(id);
            return View(lawyerProfile);
        }
        public async Task<IActionResult> LawyerProfile1(int id)
        {
            // AVM.AppointmentList = _context.Appointments.Where(a=>a.LawyerId==id).ToList();
            //var lawyerProfile = await _lawyerService.LawyerProfile(id); 

            AVM.AppointmentList = await _context.Appointments.Include(x => x.Lawyer).Where(l => l.LawyerId == id).ToListAsync();
            AVM.LawyerList = await _lawyerService.LawyerProfile1(id);

            // var lawyerProfile = await _context.Appointments.Include(x=>x.Lawyer).Where(l=>l.LawyerId == id).ToListAsync();
            return View(AVM);
        }
        public async Task<IActionResult> LawyerProfile2(int id)
        {
            var lawyerProfile = await _lawyerService.LawyerProfile2(id);
            return View(lawyerProfile);
        }

        // for edit Lawyer
        public async Task<JsonResult> UpdateLawyer(int id)
        {
            var getLawyer = await _lawyerService.GetAccountProfile(id);
            return Json(getLawyer);
        }
        public async Task<IActionResult> Delete(int Id)
        {
            string userId = Id.ToString();

            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                ViewBag.ErrorMessage = $"User with Id =  { userId } can not be found";
                return View("Not Found");
            }
            else
            {
                var result = await _userManager.DeleteAsync(user);

                if (result.Succeeded)
                {
                    RedirectToAction("GetAllLawyers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return RedirectToAction("GetAllLawyers");
            }
        }
        public async Task<IActionResult> SearchLawyer(string searchapp)
        {

            ViewData["Lawyersearch"] = searchapp;
            var searchap = from x in _context.Lawyers select x;
            if (!string.IsNullOrEmpty(searchapp))
            {
                searchap = _context.Lawyers;
                searchap = searchap.Include(a => a.LawyerLicenses).Where(x => x.FirstName.Contains(searchapp));
                return View(await searchap.AsNoTracking().ToListAsync());
            }
            var applicationDbContext = _context.Lawyers;
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> SearchLawyers(string filter)

        {
            if (filter == "TotalLawyers" || filter == null)
            {
                var Lawyers = await _lawyerService.GetAllLawyers();
                ViewBag.Filter = "All Lawyers";
                return View(Lawyers);
            }
            else if (filter == "OnlineLawyers")
            {
                var Lawyers = await _lawyerService.GetOnlineLawyers();
                ViewBag.Filter = "Online Lawyers";
                return View(Lawyers);
            }
            else if (filter == "OfflineLawyers")
            {
                var Lawyers = await _lawyerService.GetOfflineLawyers();
                ViewBag.Filter = "Offline Lawyers";
                return View(Lawyers);
            }
            return View();
        }
        public async Task<IActionResult> TrackLawyers(string searchapp)
        {

            ViewData["Lawyersearch"] = searchapp;
            var searchap = from x in _context.Lawyers select x;
            if (!string.IsNullOrEmpty(searchapp))
            {
                searchap = _context.Lawyers;
                searchap = searchap.Include(a=>a.LawyerLicenses).Where(x => x.FirstName.Contains(searchapp));
                return View(await searchap.AsNoTracking().ToListAsync());
            }
            var applicationDbContext = _context.Lawyers;
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> TrackLawyer(string searchapp)
        {

            ViewData["Lawyersearch"] = searchapp;
            var searchap = from x in _context.Lawyers select x;
            if (!string.IsNullOrEmpty(searchapp))
            {
                searchap = _context.Lawyers;
                searchap = searchap.Where(x => x.FirstName.Contains(searchapp));
                return View(await searchap.AsNoTracking().ToListAsync());
            }
            var applicationDbContext = _context.Lawyers;
            return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> TotalLawyers()
        {
            var applicationDbContext = _context.Lawyers.Include(a => a.AppUser);
            return View(await applicationDbContext.ToListAsync());
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lw = await _context.Lawyers
                .Include(a => a.AppUser)
                .FirstOrDefaultAsync(m => m.LawyerId == id);
            if (lw == null)
            {
                return NotFound();
            }

            return View(lw);
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var lw = await _context.Lawyers
                .Include(a => a.AppUser)
                .FirstOrDefaultAsync(m => m.LawyerId == id);
            if (lw == null)
            {
                return NotFound();
            }

            return View(lw);
        }

        public async Task<JsonResult> ChangeLawyerStatus(int id)
        {
            await _lawyerService.ChangeLawyerStatus(id);
            return Json("Success");
        }

        public async Task<IActionResult> TopLawyer()
        {
            var a = await _context.Lawyers.OrderByDescending(x => x.TotalExperience).FirstOrDefaultAsync();
            return View(a);
        }
        public async Task<IActionResult> TopLawyerProfile()
        {
            var a = await _context.Lawyers.OrderByDescending(x => x.TotalExperience).FirstOrDefaultAsync();
            return View(a);
        }
        public async Task<IActionResult> LawyerDetailProfile(int id)
        {
            try
            {
                //var a = _context.Lawyers.Where(a => _context.Lawyers.Any(x => x.LawyerId == a.LawyerId)).ToList();
                var lawyerProfile = await _lawyerService.LawyerProfile(id);
                return View(lawyerProfile);
                //return Json(a);
               // return View(a);
            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }
        }
        public async Task<IActionResult> LawyerWithAppoint()
        {
            try
            {
               
                var a = _context.Lawyers.Include(a=>a.AppUser).Where(a => _context.Lawyers.Any(x => x.LawyerId == a.LawyerId)).ToList();

                //return Json(a);
                return View(a);
            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }
        }
        public async Task<IActionResult> LawyerWithoutAppoint()
        {
            try
            {
                var a = _context.Lawyers.Where(a => !_context.Lawyers.Any(x => x.LawyerId == a.LawyerId)).ToList();

                //return Json(a);
                return View(a);
            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }
        }
        public async Task<IActionResult> Cancel()
        {
            try

            {
                var a = _context.Appointments.Include(a => a.Lawyer).Where(a=>a.AppoinmentStatus == "Cancel");
                return View(a);
            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }
        }
        public async Task<IActionResult> LawyersNotApproved()
        {
            try

            {
                var a = _context.Lawyers.Where(a => a.ProfileVerified == false);
                return View(a);
            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }
        }


    }
}
