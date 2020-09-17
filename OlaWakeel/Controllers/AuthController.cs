using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using OlaWakeel.Data.ApplicationUser;
using OlaWakeel.Dto.Auth;
using OlaWakeel.Dto.Role;

namespace OlaWakeel.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public AuthController(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        //[Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(loginDto.username, loginDto.password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ViewBag.Result = result.ToString();
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult Register()
        {
            ViewBag.Rolelist = new SelectList(_roleManager.Roles, "Name", "Name");
            // ViewBag.ShopLogo = _scheduleService.GetSchedule().ShopLogo;
            return View();
        }
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                 AppUser user = await _userManager.FindByNameAsync(registerDto.userName);
                if (user == null)
                {
                    user = new AppUser
                    {
                        UserName = registerDto.userName,
                        Email = registerDto.email,
                        FirstName = registerDto.firstName,
                        LastName = registerDto.lastName,
                        PhoneNumber = registerDto.phoneNumber,
                        City = registerDto.city,
                        Address = registerDto.address
                    };

                    IdentityResult result = await _userManager.CreateAsync(user, registerDto.password);
                    if (result.Succeeded)

                        ViewBag.Message = "User successfully created!";
                    {
                        await _userManager.AddToRoleAsync(user, registerDto.RoleName);

                        return RedirectToAction("AllAdminUsers", "Auth");
                    }
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return View();
        }
        [HttpGet]

        public async Task<ActionResult> AllAdminUsers()
        {
            // ViewBag.Role = new SelectList(_roleManager.Roles, "Name", "Name");
            var users = await _userManager.GetUsersInRoleAsync("Admin");
            return View(users);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateRole(RoleDto roleDto)
        {
            if (ModelState.IsValid)
            {
                AppRole role = new AppRole { Name = roleDto.RoleName };
                IdentityResult result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Roles", "Auth");
                }
            }

            return View(roleDto);
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult Roles()
        {
            RoleDto roleList = new RoleDto();
            roleList.RoleList = _roleManager.Roles;

            return View(roleList);
        }

        [Authorize(Roles = "Admin")]
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
                    RedirectToAction("AllAdminUsers");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return RedirectToAction("AllAdminUsers");
            }
        }
        [HttpGet]
        [Authorize]
        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> ChangePassword(ChangePasswordDto changePasswordDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return RedirectToAction("Login");
                }
                var result = await _userManager.ChangePasswordAsync(user,
                    changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                        ViewBag.Error = error.Description;
                    }
                    return View();
                }
                await _signInManager.RefreshSignInAsync(user);
                return View("ChangePasswordConfirmation");
            }
            return View(changePasswordDto);
        }

        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public async Task<JsonResult> CheckUserAvailabilty(IFormCollection form, string old)
        {
            var appUser = JsonConvert.DeserializeObject<AppUser>(form["appUser"]);

            AppUser user = await _userManager.FindByNameAsync(appUser.UserName);
            if (user != null && user.UserName == old)
            {
                return Json("Success");
            }
            else if (user != null && user.UserName != old)
            {
                return Json("Unsuccess");
            }
            return Json("Success");
        }
        //for lawyer edit
        [HttpPost]
        public async Task<JsonResult> CheckUserAvailabilty2(string userName, string old)
        {
            //var appUser = JsonConvert.DeserializeObject<AppUser>(form["appUser"]);

            AppUser user = await _userManager.FindByNameAsync(userName);
            if (user != null && user.UserName == old)
            {
                return Json("Success");
            }
            else if (user != null && user.UserName != old)
            {
                return Json("Unsuccess");
            }
            return Json("Success");
        }
    }
}