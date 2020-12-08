using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using OlaWakeel.Data.ApplicationUser;
using OlaWakeel.Models;
using OlaWakeel.Services.CustomerService;

namespace OlaWakeel.Controllers
{
    public class CustomerController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ICustomerService _customerService;
        private readonly IHostingEnvironment _hostingEnvironment;
        public CustomerController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, IHostingEnvironment hostingEnvironment, ICustomerService customerService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _customerService = customerService;
            _hostingEnvironment = hostingEnvironment;
        }

        //Dashboard for Customer
        public IActionResult IndexDashboard()
        {
            return View();
        }
        public IActionResult Index()
        {
            var custList = _customerService.GetAllCustomers();
            return View(custList);
        }
        public async Task<JsonResult> GetCustomerById(int id)
        {
            var custById = await _customerService.GetCustomerById(id);
            return Json(custById);
        }
        public IActionResult AddCustomer()
        {
            return View();
        }
        public async Task<IActionResult> EditCustomer(int id)
        {
            var custById = await _customerService.GetCustomerById(id);
            return View(custById);
        }
        [HttpPost]
        public async Task<JsonResult> AddCustomerPost()
        {
            // var appUser = JsonConvert.DeserializeObject<AppUser>(form["appUser"]);
            // var customer = JsonConvert.DeserializeObject<Customer>(form["customer"]);
            var appUser = JsonConvert.DeserializeObject<AppUser>(Request.Form["appUser"]);
            var customer = JsonConvert.DeserializeObject<Customer>(Request.Form["customer"]);
            var file = Request.Form.Files[0];
            try
            {
                AppUser user = await _userManager.FindByNameAsync(appUser.UserName);
                if (user == null)
                {
                    user = new AppUser
                    {
                        UserName = appUser.UserName,
                        Email = appUser.Email
                        //FirstName = registerDto.firstName,
                        //LastName = registerDto.lastName,
                        //PhoneNumber = registerDto.phoneNumber,
                        //City = registerDto.city,
                        //Address = registerDto.address
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, appUser.PasswordHash);
                    ViewBag.Message = "User successfully created!";

                    if (result.Succeeded)
                    {
                        customer.AppUserId = user.Id;
                        await _userManager.AddToRoleAsync(user, "Customer");

                        string UniqueFilename;
                        if (file != null)
                        {
                            string UploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "CustomerImages");

                            if (!Directory.Exists(UploadFolder))
                            {
                                Directory.CreateDirectory(UploadFolder);
                            }

                            UniqueFilename = Guid.NewGuid().ToString() + "_" + file.FileName;
                            string filePath = Path.Combine(UploadFolder, UniqueFilename);
                            file.CopyTo(new FileStream(filePath, FileMode.Create));
                            customer.ProfilePic = UniqueFilename;
                            customer.Status = true;
                            customer.Date = DateTime.Now;
                            //customer.OnlineStatus = "Online";
                            //category.Image = UniqueFilename;
                            //await _service.AddDishCategory(category);
                        }

                        await _customerService.AddCustomer(customer);
                        return Json("Success");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            return Json("");
        }

        [HttpPost]
        public async Task<JsonResult> EditCustomer()
        {
            //var appUser = JsonConvert.DeserializeObject<AppUser>(form["appUser"]);
            //var customer = JsonConvert.DeserializeObject<Customer>(form["customer"]);
            var appUser = JsonConvert.DeserializeObject<AppUser>(Request.Form["appUser"]);
            var customer = JsonConvert.DeserializeObject<Customer>(Request.Form["customer"]);

            if (ModelState.IsValid)
            {
                string userId = customer.AppUserId.ToString();
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    ViewBag.ErrorMessage = $"User with Id =  { userId } can not be found";
                    return Json("Not Found");
                }
                else
                {
                    user.Email = appUser.Email;
                    user.UserName = appUser.UserName;

                    var result = await _userManager.UpdateAsync(user);

                    if (result.Succeeded)
                    {
                        string UniqueFilename = null;
                        if (Request.Form.Files.Count > 0)
                        {
                            

                            var file = Request.Form.Files[0];
                            string UploadFolder = Path.Combine(_hostingEnvironment.WebRootPath, "CustomerImages");

                            if (!Directory.Exists(UploadFolder))
                            {
                                Directory.CreateDirectory(UploadFolder);
                            }
                            /*Guid.NewGuid().ToString() + "_" +*/
                            UniqueFilename =  file.FileName;
                            string filePath = Path.Combine(UploadFolder, UniqueFilename);
                            file.CopyTo(new FileStream(filePath, FileMode.Create));
                            //customer.ProfilePic = UniqueFilename;
                            
                        }
                        await _customerService.EditCustomer(customer, UniqueFilename);
                        return Json("Success");


                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }


            }
            return Json("");
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
                    RedirectToAction("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return RedirectToAction("Index");
            }
        }
    }
}