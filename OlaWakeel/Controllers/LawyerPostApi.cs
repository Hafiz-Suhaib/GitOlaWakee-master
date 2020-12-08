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
using OlaWakeel.Data;
using OlaWakeel.Data.ApplicationUser;
using OlaWakeel.Models;
using OlaWakeel.Services.CustomerService;
using OlaWakeel.Services.LawyerService;

namespace OlaWakeel.Controllers
{
    public class obj
    {
        public string ImageName;
        public string Email;
    }
    public class obj2
    {
        public string CnicFrontImageName;
        public string CnicBackImageName;
        public string RecentDegreeImageName;
    }
    public class obj4
    {
        public string type;
    }
    public class obj3
    {
        public string CertificateImageName;
    }
    public class LawyerPostApi : Controller
    {
        private const string Lawyer = "Lawyer";
        private readonly ApplicationDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        public readonly ILawyerService _lawyerService;
        private readonly ICustomerService _customerService;
       
        private readonly IWebHostEnvironment _env;

        public LawyerPostApi(IWebHostEnvironment env, RoleManager<AppRole> roleManager, UserManager<AppUser> userManager, ILawyerService lawyerService, ApplicationDbContext context, ICustomerService customerService)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _lawyerService = lawyerService;
            _context = context;
            _customerService = customerService;
            _env = env;
        }
        /// <summary>
        /// thtiffddf dfgere
        /// </summary>
        /// <param name="dateOfBirth">date</param>
        /// <returns></returns>
        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }
        [HttpGet]
        public async Task<JsonResult> UpdateLawyer(int userId, string number)
        {
            var lawyer = _context.Lawyers.Find(userId);
            var user = _context.Users.SingleOrDefault(s => s.Id == lawyer.AppUserId);
            lawyer.Contact = number;
            user.PhoneNumber = number;
            IdentityResult result = await _userManager.UpdateAsync(user);
            _context.Lawyers.Update(lawyer);
            _context.SaveChanges();


            return Json("Success");

        }
      
        //For Create Lawyer'
        [HttpGet]
        //[Route("CreateLawyer")]
        public async Task<JsonResult> CreateLawyer(string PhoneNo, string FirstName, string LastName, string FirbaseToken)
        {   
            try
            {
                var user = new AppUser { PhoneNumber = PhoneNo };
                //  user.Email = "abc@gmail.com";
                Random rndm = new Random();
                user.UserName = (FirstName.ToLower().Replace(" ", "")) + (LastName.ToLower().Replace(" ", "")) + rndm.Next(0000, 9999);
                user.Email = user.UserName + "@exemple.com";

                IdentityResult result = await _userManager.CreateAsync(user);
                Lawyer lawyer = new Lawyer();

                if (result.Succeeded)
                {
                    lawyer.AppUserId = user.Id;
                    await _userManager.AddToRoleAsync(user, Lawyer);
                    lawyer.FirstName = FirstName;
                    lawyer.LastName = LastName;
                    lawyer.Contact = PhoneNo;
                    //ye application user me add krna ha
                    lawyer.FirbaseToken = FirbaseToken;
                    lawyer.OnlineStatus = "Online";
                    await _lawyerService.AddLawyer(lawyer);

                    var wallet = new Wallet();
                    wallet.WalletAmount = 100;
                    wallet.UserId = lawyer.LawyerId;
                    wallet.WalletType = "Lawyer";
                    wallet.Date = DateTime.Now;
                    wallet.Status = true;
                    await _context.Wallets.AddAsync(wallet);
                    await _context.SaveChangesAsync();

                    var History = new WalletHistory();
                    History.WalletHistoryAmount = 100;
                    History.WalletHistoryDisc = "During Creation of Lawyer";
                    History.Status = true;
                    History.Date = DateTime.Now;
                    History.WalletId = wallet.WalletId;
                    await _context.WalletHistories.AddAsync(History);
                    await _context.SaveChangesAsync();

                    var not = new Notification();
                    not.Date = DateTime.Now;
                    not.NotificationSeen = false;
                    not.Usertype = "Lawyer";
                    not.Status = true;
                    not.UserId = lawyer.LawyerId;
                    not.NotificationTypeId = wallet.WalletId;
                    not.NotificationType = "Wallet";
                    not.NotificationMessage = History.WalletHistoryDisc;
                    //not.NotificationSubject = "";
                    _context.Notifications.Add(not);
                    _context.SaveChanges();

                    var LawyerData = new
                    {
                        LawyerId = lawyer.LawyerId,
                        lawyerName = lawyer.FirstName + " " + lawyer.LastName,
                        LawyerPic = lawyer.ProfilePic,
                        OnlineStatus = lawyer.OnlineStatus,
                        //IntroStatus = _context.LawyerLanguages.Any(a => a.LawyerId == lawyer.LawyerId),
                        //EducationStatus = _context.LawyerQualifications.Any(a => a.LawyerId == lawyer.LawyerId),
                        //LawFieldStatus = _context.LawyerExperiences.Any(a => a.LawyerId == lawyer.LawyerId),
                        //AboutStatus = _context.LawyerClients.Any(a => a.LawyerId == lawyer.LawyerId),
                        //AddOfficeStatus = _context.LawyerAddresses.Any(a => a.LawyerId == lawyer.LawyerId),
                        //PackageStatus = _context.LawyerTimings.Any(a => a.LawyerId == lawyer.LawyerId),
                        //DocumentStatus = false,

                        //Notifications = "...",

                    };
                    if(LawyerData!=null)

                        return Json(LawyerData);
                }
                var configs = new { Lawyer = "Not Exist" };

                return Json(configs);



            }
            catch (Exception ex)
            {

                return Json("Invalid Data");
            }

            var lawyerExist = JsonConvert.SerializeObject(new { Data = "UnSuccessful" });
            return Json(lawyerExist);

        }


       
        // for Lawyer Intro Data
        private string CreateAppointmentCode()
        {
            string code = "";
            var year = DateTime.Now.Year;
            var month = DateTime.Now.ToString("MM");
            var appointcode = "";
                
            if (_context.Appointments.Any())
            {
                appointcode= _context.Appointments.OrderByDescending(p => p.AppoinmentId).FirstOrDefault().AppointmentCode;
                var lastdigits = appointcode.Substring(7);
                if (lastdigits.Length > 4)
                {
                    code = "#" + month + year + 1;
                    return code;
                }
                int a = Convert.ToInt32(lastdigits);

                code = "#" + month + year + ++a;

                return code;
            }
            else
            {
                code = "#" + month + year + 1;
                return code;
            }
            //int month = DateTime.Now.Month;
            //month++;
            //string monthStr = month.ToString("00");

        }
        public async Task<JsonResult> LawyerIntro(IFormCollection form)
         {
          
            try
            { 
                var lawyerData = JsonConvert.DeserializeObject<Lawyer>(Request.Form["Lawyer"]);
                //  var appUser = JsonConvert.DeserializeObject<AppUser>(Request.Form["appUser"]);
                obj o = JsonConvert.DeserializeObject<obj>(form["OtherInfo"]);
                string images = o.ImageName;

                // var lawyerLanguage = JsonConvert.DeserializeObject<List<LawyerLanguage>>(Request.Form["Languages"]);
                List<LawyerLanguage> lawyerLanguage = JsonConvert.DeserializeObject<LawyerLanguage[]>(form["Languages"].ToString()).ToList();
               


                var lawyer =await _context.Lawyers.FindAsync(lawyerData.LawyerId);
                //lawyer.ProfilePic = law.ProfilePic; // ye krna ha
                var date = lawyerData.DateOfBirth.ToShortDateString();
                var date1 = Convert.ToDateTime(date);
                lawyer.DateOfBirth = date1;
                lawyer.Age = CalculateAge(lawyer.DateOfBirth);
                lawyer.Gender = lawyerData.Gender;
                lawyer.Cnic = lawyerData.Cnic;
                lawyer.City = lawyerData.City;
                //string email = JsonConvert.DeserializeObject<string>(form["Email"]);
                var user = _context.Users.Where(s => s.Id == lawyer.AppUserId).SingleOrDefault();
                user.Email = o.Email;
                IdentityResult result =  await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    if (images != "0")
                    {
                        //var path = Path.Combine(_env.ContentRootPath, "App_Data/Files");
                        var path = Path.Combine(_env.ContentRootPath, "wwwroot/Uploads");
                        //String path = Path.Combine("~/wwwroot/Uploads"); //Path
                        if (!System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                        }
                        string Imagename = images + ".jpg";
                        string imgPath = Path.Combine(path, Imagename);

                        // var bytes = Convert.FromBase64String(lawyerData.CnicFrontImageName);

                        byte[] imageBytes = Convert.FromBase64String(lawyerData.ProfilePic);
                        System.IO.File.WriteAllBytes(imgPath, imageBytes);

                        lawyer.ProfilePic = Imagename;

                    }
                    _context.Lawyers.Update(lawyer);
                    await _context.SaveChangesAsync();

                    foreach (var language in lawyerLanguage)
                    {
                        await _context.LawyerLanguages.AddAsync(language);
                    }
                    await _context.SaveChangesAsync();


                }
                var LawyerData = new
                {
                    //LawyerId = lawyer.LawyerId,
                    //lawyerName = lawyer.FirstName + lawyer.LastName,
                    //LawyerPic = lawyer.ProfilePic,
                    //OnlineStatus = lawyer.OnlineStatus,
                    IntroStatus = _context.LawyerLanguages.Any(a => a.LawyerId == lawyer.LawyerId),
                    EducationStatus = _context.LawyerQualifications.Any(a => a.LawyerId == lawyer.LawyerId),
                    LawFieldStatus = _context.LawyerExperiences.Any(a => a.LawyerId == lawyer.LawyerId),
                    AboutStatus = _context.LawyerClients.Any(a => a.LawyerId == lawyer.LawyerId),
                    AddOfficeStatus = _context.LawyerAddresses.Any(a => a.LawyerId == lawyer.LawyerId),
                    PackageStatus = _context.LawyerTimings.Any(a => a.LawyerId == lawyer.LawyerId),
                    DocumentStatus = _context.LawyerCertificatePics.Any(a => a.LawyerId == lawyer.LawyerId),

                };


                return Json(LawyerData);

            }
            catch (Exception ex)
            {

                return Json("Invalid Data");
            }

            var lawyerExist = JsonConvert.SerializeObject(new { Data = "UnSuccessful" });
            return Json(lawyerExist);

        }
// For Lawyer Education Data
        public async Task<JsonResult> LawyerEducations(IFormCollection form)
        {
            try
             {
                List<LawyerQualification> lawyerQualification = JsonConvert.DeserializeObject<LawyerQualification[]>(form["Education"].ToString()).ToList();

                foreach (var education in lawyerQualification)
                    {
                        if (education.SpecializationId == 0 || education.SpecializationId == null)
                        {
                            education.Check = true;
                            await _context.LawyerQualifications.AddAsync(education);
                            continue;
                        }
                        education.Check = false;
                            await _context.LawyerQualifications.AddAsync(education);
                    }
                    await _context.SaveChangesAsync();


                var lawyer = await _context.Lawyers.FindAsync(lawyerQualification[0].LawyerId);
                var LawyerData = new
                {
                    IntroStatus = _context.LawyerLanguages.Any(a => a.LawyerId == lawyer.LawyerId),
                    EducationStatus = _context.LawyerQualifications.Any(a => a.LawyerId == lawyer.LawyerId),
                    LawFieldStatus = _context.LawyerExperiences.Any(a => a.LawyerId == lawyer.LawyerId),
                    AboutStatus = _context.LawyerClients.Any(a => a.LawyerId == lawyer.LawyerId),
                    AddOfficeStatus = _context.LawyerAddresses.Any(a => a.LawyerId == lawyer.LawyerId),
                    PackageStatus = _context.LawyerTimings.Any(a => a.LawyerId == lawyer.LawyerId),
                    DocumentStatus = _context.LawyerCertificatePics.Any(a => a.LawyerId == lawyer.LawyerId),

                };


                return Json(LawyerData);

            }
            catch (Exception ex)
            {

                return Json("Invalid Data");
            }

            var lawyerExist = JsonConvert.SerializeObject(new { Data = "UnSuccessful" });
            return Json(lawyerExist);
        }

        //screen# 9
        [HttpGet]
        public async Task<JsonResult> GetDegreeData()
        {
            try
            {

                var DegreeData = _context.Degrees.Select(x => new
                {
                    DegreeId = x.DegreeId,
                    DegreetypeId = x.DegreeTypeId,
                    DegreeName = x.Name,
                    DegreeYear = x.EligibleAfter,
                    Check = x.DegreeStatus
                   
                    }).ToList();
                var DegreeTypeData = _context.DegreeTypes.ToList().Select(a=> new {
                    DegreeTypeId=a.DegreeTypeId,
                    TypeName=a.TypeName,
                });
                var Specialization = _context.Specializations.ToList().Select(a=> new {
                    SpecializationId=a.SpecializationId,
                    SpecializationName=a.SpecializationName
                });
                var data = new
                {
                    DegreeData,
                    DegreeTypeData,
                    Specialization
                };

                return Json(data);
            }
            catch(Exception ex)
            {
                return Json("Invalid Data");
            }
        }

        //screen # 10 
        [HttpGet]
        public async Task<JsonResult> GetLicenseData()
        {

            try
            {
               
                //var License_City = _context.LicenseCities.ToList().Select(a=> new {
                //    LicenseDistrictId=a.LicenseDistrictId,
                //    LicenseCityId=a.LicenseCityId,
                //    CityName=a.CityName,
                //    LicenseExist=a.LicenseExist
                //});

                //var casecat = _context.CaseCategories.ToList().Select(x => new {
                //    ServiceName = x.Name,
                //    ServiceId = x.CaseCategoryId,
                //    ServiceIcon = x.VectorIcon
                //});

                var Data = new
                {
                    LicenseData = _context.LicenseCities.ToList().Select(a => new {
                        LicenseDistrictId = a.LicenseDistrictId,
                        LicenseCityId = a.LicenseCityId,
                        CityName = a.CityName,
                       DistrictName = _context.LicenseDistricts.Where(d=>d.LicenseDistrictId == a.LicenseDistrictId).SingleOrDefault().DistrictName,
                        LicenseExist = a.LicenseExist
                    }),
                    ServiceData = _context.CaseCategories.ToList().Select(x => new {
                        ServiceName = x.Name,
                        ServiceId = x.CaseCategoryId,
                        ServiceIcon = x.VectorIcon
                    })
                };

                return Json(Data);

                //var LicenseData = new HttpResponseMessage(HttpStatusCode.OK);

                //var LicenseCityData = _context.LicenseCities.ToList();
                //var LicenseDistrictData = _context.LicenseDistricts.ToList();

                //LicenseData.Content = new StringContent(JsonConvert.SerializeObject(new
                //{
                //    LicenseCityData = LicenseCityData,
                //    LicenseDistrictData = LicenseDistrictData

                //}));
                //LicenseData.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //return LicenseData;
            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }
        }

        // for Lawyer LawField Data
        public async Task<JsonResult> LawyerLawField(IFormCollection form)
        {
            try
            {
                var Services = JsonConvert.DeserializeObject<LawyerCaseCategory[]>(form["Service"].ToString()).ToList();
                List<LawyerExperience> Experiences = JsonConvert.DeserializeObject<LawyerExperience[]>(form["Experience"].ToString()).ToList();

                List<LawyerLicense> lawyerLicense = JsonConvert.DeserializeObject<LawyerLicense[]>(form["License"].ToString()).ToList();
                var lawyerData = JsonConvert.DeserializeObject<Lawyer>(Request.Form["Lawyer"]);

                var lawyer = await _context.Lawyers.FindAsync(lawyerData.LawyerId);
               
                lawyer.TotalExperience = lawyerData.TotalExperience;

                _context.Lawyers.Update(lawyer);
                await _context.SaveChangesAsync();
               
                foreach (var service in Services)
                {
                    await _context.LawyerCaseCategories.AddAsync(service);
                }
                await _context.SaveChangesAsync();

                foreach (var experience in Experiences)
                {
                    await _context.LawyerExperiences.AddAsync(experience);
                }
                await _context.SaveChangesAsync();

                foreach (var license in lawyerLicense)
                    {
                        await _context.LawyerLicenses.AddAsync(license);
                    }
                    await _context.SaveChangesAsync();


                
                var LawyerData = new
                {
                    IntroStatus = _context.LawyerLanguages.Any(a => a.LawyerId == lawyer.LawyerId),
                    EducationStatus = _context.LawyerQualifications.Any(a => a.LawyerId == lawyer.LawyerId),
                    LawFieldStatus = _context.LawyerExperiences.Any(a => a.LawyerId == lawyer.LawyerId),
                    AboutStatus = _context.LawyerClients.Any(a => a.LawyerId == lawyer.LawyerId),
                    AddOfficeStatus = _context.LawyerAddresses.Any(a => a.LawyerId == lawyer.LawyerId),
                    PackageStatus = _context.LawyerTimings.Any(a => a.LawyerId == lawyer.LawyerId),
                    DocumentStatus = _context.LawyerCertificatePics.Any(a => a.LawyerId == lawyer.LawyerId),

                };


                return Json(LawyerData);

            }
            catch (Exception ex)
            {

                return Json("Invalid Data");
            }

            var lawyerExist = JsonConvert.SerializeObject(new { Data = "UnSuccessful" });
            return Json(lawyerExist);
        }

        // for Lawyer About Data
        public async Task<JsonResult> LawyerAbout(IFormCollection form)
        {

            try
            {
                List<LawyerClient> lawyerClient = JsonConvert.DeserializeObject<LawyerClient[]>(form["RenownedClient"].ToString()).ToList();
                var lawyerData = JsonConvert.DeserializeObject<Lawyer>(Request.Form["Lawyer"]);

                var lawyer = await _context.Lawyers.FindAsync(lawyerData.LawyerId);

                lawyer.Biography = lawyerData.Biography;

                _context.Lawyers.Update(lawyer);
                await _context.SaveChangesAsync();

                foreach (var client in lawyerClient)
                {
                    await _context.LawyerClients.AddAsync(client);
                }
                await _context.SaveChangesAsync();

                var LawyerData = new
                {
                    IntroStatus = _context.LawyerLanguages.Any(a => a.LawyerId == lawyer.LawyerId),
                    EducationStatus = _context.LawyerQualifications.Any(a => a.LawyerId == lawyer.LawyerId),
                    LawFieldStatus = _context.LawyerExperiences.Any(a => a.LawyerId == lawyer.LawyerId),
                    AboutStatus = _context.LawyerClients.Any(a => a.LawyerId == lawyer.LawyerId),
                    AddOfficeStatus = _context.LawyerAddresses.Any(a => a.LawyerId == lawyer.LawyerId),
                    PackageStatus = _context.LawyerTimings.Any(a => a.LawyerId == lawyer.LawyerId),
                    DocumentStatus = _context.LawyerCertificatePics.Any(a => a.LawyerId == lawyer.LawyerId),

                };


                return Json(LawyerData);

            }
            catch (Exception ex)
            {

                return Json("Invalid Data");
            }

            var lawyerExist = JsonConvert.SerializeObject(new { Data = "UnSuccessful" });
            return Json(lawyerExist);
        }

        public async Task<JsonResult> LawyerAddress(IFormCollection form)
        {

            try
            {
                List<LawyerAddress> lawyerAddress = JsonConvert.DeserializeObject<LawyerAddress[]>(form["OfficeAddresses"].ToString()).ToList();


                var lawyer = await _context.Lawyers.FindAsync(lawyerAddress[0].LawyerId);
                foreach (var address in lawyerAddress)
                {
                    await _context.LawyerAddresses.AddAsync(address);
                }
                await _context.SaveChangesAsync();

                var LawyerData = new
                {
                    IntroStatus = _context.LawyerLanguages.Any(a => a.LawyerId == lawyer.LawyerId),
                    EducationStatus = _context.LawyerQualifications.Any(a => a.LawyerId == lawyer.LawyerId),
                    LawFieldStatus = _context.LawyerExperiences.Any(a => a.LawyerId == lawyer.LawyerId),
                    AboutStatus = _context.LawyerClients.Any(a => a.LawyerId == lawyer.LawyerId),
                    AddOfficeStatus = _context.LawyerAddresses.Any(a => a.LawyerId == lawyer.LawyerId),
                    PackageStatus = _context.LawyerTimings.Any(a => a.LawyerId == lawyer.LawyerId),
                    DocumentStatus = _context.LawyerCertificatePics.Any(a => a.LawyerId == lawyer.LawyerId),

                };


                return Json(LawyerData);

            }
            catch (Exception ex)
            {

                return Json("Invalid Data");
            }

            var lawyerExist = JsonConvert.SerializeObject(new { Data = "UnSuccessful" });
            return Json(lawyerExist);
        }

        public async Task<JsonResult> LawyerPackages(IFormCollection form)
        {

            try
            {
                List<LawyerTiming> lawyerPackage = JsonConvert.DeserializeObject<LawyerTiming[]>(form["Packages"].ToString()).ToList();


                var lawyer = await _context.Lawyers.FindAsync(lawyerPackage[0].LawyerId);
                foreach (var packages in lawyerPackage)
                {
                    if(packages.SlotType.ToLower() == "inperson")
                    {
                        packages.Check2 = false;
                    }
                    else
                    {
                        packages.Check2 = true;
                        packages.Location = "Online Video Consultation (Online)";
                    }

                    if (packages.AppoinmentFee == "Fee")
                    {
                        packages.Check = false;
                    }
                    else
                    {
                        packages.Check = true;
                    }
                    packages.Status = true;
                    packages.Date = DateTime.Now;
                    await _context.LawyerTimings.AddAsync(packages);
                }
                await _context.SaveChangesAsync();

                var LawyerData = new
                {
                    IntroStatus = _context.LawyerLanguages.Any(a => a.LawyerId == lawyer.LawyerId),
                    EducationStatus = _context.LawyerQualifications.Any(a => a.LawyerId == lawyer.LawyerId),
                    LawFieldStatus = _context.LawyerExperiences.Any(a => a.LawyerId == lawyer.LawyerId),
                    AboutStatus = _context.LawyerClients.Any(a => a.LawyerId == lawyer.LawyerId),
                    AddOfficeStatus = _context.LawyerAddresses.Any(a => a.LawyerId == lawyer.LawyerId),
                    PackageStatus = _context.LawyerTimings.Any(a => a.LawyerId == lawyer.LawyerId),
                    DocumentStatus = _context.LawyerCertificatePics.Any(a=>a.LawyerId == lawyer.LawyerId),

                };


                return Json(LawyerData);

            }
            catch (Exception ex)
            {

                return Json("Invalid Data");
            }

            var lawyerExist = JsonConvert.SerializeObject(new { Data = "UnSuccessful" });
            return Json(lawyerExist);
        }
        // for Lawyer Documents Data
        public async Task<JsonResult> LawyerDocuments(IFormCollection form)
        {

            try
            {
                List<LawyerCertificatePic> Certificates = JsonConvert.DeserializeObject<LawyerCertificatePic[]>(form["Certificates"].ToString()).ToList();

                var lawyerData = JsonConvert.DeserializeObject<Lawyer>(Request.Form["Lawyer"]);

                obj2 obj2 = JsonConvert.DeserializeObject<obj2>(form["MainImages"]);

                List<obj3> obj3 = JsonConvert.DeserializeObject<List<obj3>>(form["CertificateImages"]);
                
                var path = Path.Combine(_env.ContentRootPath, "wwwroot/Uploads");
                string Imagename ;
                string imgPath ;
                byte[] imageBytes;
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                }

                var i = 0;
                foreach (var certificate in Certificates)
                {
                    if (obj3[i].CertificateImageName != "0")
                    {
                        Imagename = obj3[i].CertificateImageName + ".jpg";
                        imgPath = Path.Combine(path, Imagename);
                        imageBytes = Convert.FromBase64String(certificate.CertificatePic);
                        System.IO.File.WriteAllBytes(imgPath, imageBytes);
                        certificate.CertificatePic = Imagename;
                        

                    }
                    i++;
                    await _context.LawyerCertificatePics.AddAsync(certificate);
                }
                await _context.SaveChangesAsync();

                var lawyer = _context.Lawyers.Where(a => a.LawyerId == lawyerData.LawyerId).SingleOrDefault();
                   
                if (obj2.CnicFrontImageName != "0")
                {
                    Imagename = obj2.CnicFrontImageName + ".jpg";
                    imgPath = Path.Combine(path, Imagename);
                    imageBytes = Convert.FromBase64String(lawyerData.CnicFrontPic);
                    System.IO.File.WriteAllBytes(imgPath, imageBytes);
                    lawyer.CnicFrontPic = Imagename;
                }
                if (obj2.CnicBackImageName != "0")
                {
                    Imagename = obj2.CnicBackImageName + ".jpg";
                    imgPath = Path.Combine(path, Imagename);
                    imageBytes = Convert.FromBase64String(lawyerData.CnicBackPic);
                    System.IO.File.WriteAllBytes(imgPath, imageBytes);
                    lawyer.CnicBackPic = Imagename;
                }

                if (obj2.RecentDegreeImageName != "0")
                {
                    Imagename = obj2.RecentDegreeImageName + ".jpg";
                    imgPath = Path.Combine(path, Imagename);
                    imageBytes = Convert.FromBase64String(lawyerData.RecentDegreePic);
                    System.IO.File.WriteAllBytes(imgPath, imageBytes);
                    lawyer.RecentDegreePic = Imagename;
                }
                
               
                _context.Lawyers.Update(lawyer);
                await _context.SaveChangesAsync();

                var LawyerData = new
                {
                    IntroStatus = _context.LawyerLanguages.Any(a => a.LawyerId == lawyer.LawyerId),
                    EducationStatus = _context.LawyerQualifications.Any(a => a.LawyerId == lawyer.LawyerId),
                    LawFieldStatus = _context.LawyerExperiences.Any(a => a.LawyerId == lawyer.LawyerId),
                    AboutStatus = _context.LawyerClients.Any(a => a.LawyerId == lawyer.LawyerId),
                    AddOfficeStatus = _context.LawyerAddresses.Any(a => a.LawyerId == lawyer.LawyerId),
                    PackageStatus = _context.LawyerTimings.Any(a => a.LawyerId == lawyer.LawyerId),
                    DocumentStatus = _context.LawyerCertificatePics.Any(a=>a.LawyerId == lawyer.LawyerId),

                };


                return Json(LawyerData);

            }
            catch (Exception ex)
            {

                return Json("Invalid Data");
            }

        }

        // for user post api 
        // For first time Create Client Profile Data
        [HttpGet]
        public async Task<JsonResult> CreateClient(string PhoneNo,string Country, string FirstName, string LastName, string FirbaseToken)
        {

            try
            {
                var user = new AppUser { PhoneNumber = PhoneNo };
                //  user.Email = "abc@gmail.com";
                Random rndm = new Random();
                user.UserName = (FirstName.ToLower().Replace(" ", "")) + (LastName.ToLower().Replace(" ", "")) + rndm.Next(0000, 9999);
                user.Email = user.UserName + "2@exemple.com";

                IdentityResult result = await _userManager.CreateAsync(user);
                Customer customer = new Customer();

                if (result.Succeeded)
                {
                    customer.AppUserId = user.Id;
                    await _userManager.AddToRoleAsync(user, "Customer");
                    customer.FirstName = FirstName;
                    customer.LastName = LastName;
                    customer.Contact = PhoneNo;
                    customer.Country = Country;
                    customer.Date = DateTime.Now;
                    customer.Status = true;
                    //ye application user me add krna ha
                    customer.FirbaseToken = FirbaseToken;
                    await _customerService.AddCustomer(customer);

                    var wallet = new Wallet();
                    wallet.WalletAmount = 100;
                    wallet.UserId = customer.CustomerId;
                    wallet.WalletType = "Client";
                    wallet.Date = DateTime.Now;
                    wallet.Status = true;
                    await _context.Wallets.AddAsync(wallet);
                    await _context.SaveChangesAsync();

                    var History = new WalletHistory();
                    History.WalletHistoryAmount = 100;
                    History.WalletHistoryDisc = "During Creation of Client";
                    History.Status = true;
                    History.Date = DateTime.Now;
                    History.WalletId = wallet.WalletId;
                    await _context.WalletHistories.AddAsync(History);
                    await _context.SaveChangesAsync();

                    var not = new Notification();
                    not.Date = DateTime.Now;
                    not.NotificationSeen = false;
                    not.Usertype = "Client";
                    not.Status = true;
                    not.NotificationTypeId = wallet.WalletId;
                    not.NotificationType = "Wallet";
                    not.UserId = customer.CustomerId;
                    not.NotificationMessage = History.WalletHistoryDisc;
                    //not.NotificationSubject = "";
                    _context.Notifications.Add(not);
                    _context.SaveChanges();

                    var Customer = new
                    {
                        CustomerId = customer.CustomerId,
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        ProfilePic = customer.ProfilePic,
                        AppUserId = customer.AppUserId
                    };



                    return Json(Customer);
                }
                

            }
            catch (Exception ex)
            {

                return Json("Invalid Data");
            }

            var lawyerExist = JsonConvert.SerializeObject(new { Data = "UnSuccessful" });
            return Json(lawyerExist);


            //return Json("UnSuccessful", new Newtonsoft.Json.JsonSerializerSettings());

        }
        //For Client Profile Edit
        public async Task<JsonResult> EditClientProfile(IFormCollection form)
        {

            try
            {
                obj o = JsonConvert.DeserializeObject<obj>(form["OtherImage"]);
                string images = o.ImageName;
                Customer customerData = JsonConvert.DeserializeObject<Customer>(Request.Form["Customer"]);
                var customer = _context.Customers.Where(a => a.CustomerId == customerData.CustomerId).SingleOrDefault();
                var user = _context.Users.Where(s => s.Id == customerData.AppUserId).SingleOrDefault();
               // var user2 = _context.Users.SingleOrDefault(s =>s.Id== )
                user.PhoneNumber = customerData.Contact;
                Random rndm = new Random();
                user.UserName = (customerData.FirstName.ToLower().Replace(" ", "")) + (customerData.LastName.ToLower().Replace(" ", "")) + rndm.Next(0000, 9999);
                 
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {

                    customer.Contact = customerData.Contact;
                    customer.Gender = customerData.Gender;
                    customer.DateOfBirth = customerData.DateOfBirth;
                    customer.Age = CalculateAge(customer.DateOfBirth);
                    customer.City = customerData.City;
                    if (images != "0")
                    {
                        // var path = Path.Combine(_env.ContentRootPath, "App_Data/Files");
                        String path = Path.Combine("~/wwwroot/Uploads"); //Path
                        if (!System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                        }
                        string Imagename = images + ".jpg";
                        string imgPath = Path.Combine(path, Imagename);
                        try
                        {
                            var bytes = Convert.FromBase64String(customerData.ProfilePic);
                        }
                        catch (Exception ex)
                        {

                            
                        }
                        

                        byte[] imageBytes = Convert.FromBase64String(customerData.ProfilePic);
                        System.IO.File.WriteAllBytes(imgPath, imageBytes);



                        customer.ProfilePic = Imagename;

                    }
                    // ProfilePic krna ha
                    //ye application user me add krna ha
                    //lawyer.FirbaseToken = FirbaseToken;

                }
                _context.Customers.Update(customer);
                await _context.SaveChangesAsync();
                var Customer = new
                {
                    CustomerId = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    ProfilePic = customer.ProfilePic,
                    AppUserId = customer.AppUserId
                };


                return Json(Customer);

            }
            catch (Exception ex)
            {

                return Json("Invalid Data");
            }

            var lawyerExist = JsonConvert.SerializeObject(new { Data = "UnSuccessful" });
            return Json(lawyerExist);

        }
        //for Appoinment notification
        public async Task<JsonResult> Appointment(IFormCollection form)
        {

            try
            {
                Appointment appointmentData = JsonConvert.DeserializeObject<Appointment>(Request.Form["Appointment"]);
                appointmentData.AppoinmentStatus = "Pending";
                appointmentData.Date = DateTime.Now;
                appointmentData.AppointmentCode = CreateAppointmentCode();
                await _context.Appointments.AddAsync(appointmentData);
                await _context.SaveChangesAsync();

                var log = new Log();
                log.Appointment_Id = appointmentData.AppoinmentId;
                log.LogDate = DateTime.Now;
                log.Status = true;
                log.User_id = appointmentData.CustomerId;
                log.User_Type = "Client";
                log.Log_Status = "Pending";
                log.Log_Decs = "Client Create Appointment at " + DateTime.Now.ToShortDateString();
                await _context.Logs.AddAsync(log);
                await _context.SaveChangesAsync();

                var not = new Notification();

                not.Date = DateTime.Now;
                not.NotificationSeen = false;
                not.Usertype = "Client";
                not.Status = true;
                not.NotificationTypeId = appointmentData.AppoinmentId;
                not.NotificationType = "Appointment";
                not.NotificationMessage = log.Log_Decs;
                //not.NotificationSubject = "";
                not.UserId = appointmentData.CustomerId;
                _context.Notifications.Add(not);
                _context.SaveChanges();
                //var data = JsonConvert.SerializeObject(new { Data = "Success" });
                return Json(appointmentData.AppoinmentId);

            }
            catch (Exception ex)
            {

                return Json("Invalid Data");
            }

        }

        //for Cancled Appointments
        [HttpGet]
        public async Task<JsonResult> UpdateAppoint(int appointmentId, string appointmentStatus, string user, string decs)
        {

            try
            {
                //var appointmentData = JsonConvert.DeserializeObject<Appointment>(Request.Form["Appointment"]);
                //var appointmentLog = JsonConvert.DeserializeObject<Log>(Request.Form["AppointmentCancel"]);
                //var appoint = _context.Appointments.Find(appointmentData.AppoinmentId);
                var appoint = _context.Appointments.Find(appointmentId);
                appoint.AppoinmentStatus = appointmentStatus;
                _context.Appointments.Update(appoint);
                await _context.SaveChangesAsync();
                var log = new Log();
                log.Appointment_Id = appoint.AppoinmentId;
                log.LogDate = DateTime.Now;
                log.Status = true;
                if (user.ToLower() == "client")
                {
                    log.User_id = appoint.CustomerId;
                    log.User_Type = user;
                }
                else
                {
                    log.User_id = appoint.LawyerId;
                    log.User_Type = "Lawyer";
                }
                log.Log_Status = appointmentStatus;
                log.Log_Decs = decs + " at " + DateTime.Now.ToShortDateString();
                await _context.Logs.AddAsync(log);
                await _context.SaveChangesAsync();
                var not = new Notification();
                not.Date = DateTime.Now;
                not.NotificationSeen = false;
                not.Usertype = user;
                not.Status = true;
                not.NotificationTypeId = appoint.AppoinmentId;
                not.NotificationType = "Appointment";
                not.NotificationMessage = log.Log_Decs;
                //not.NotificationSubject = "";
                not.UserId = log.User_id;
                _context.Notifications.Add(not);
                _context.SaveChanges();
                // var data = JsonConvert.SerializeObject(new { Data = "Success" });
                if (user.ToLower() == "lawyer")
                {
                    var Appointment = new
                    {
                        AppoinmentId = appoint.AppoinmentId,
                        Token = _context.Customers.Find(appoint.CustomerId).FirbaseToken,

                    };
                    return Json(Appointment);

                }
                else
                {
                    var Appointment = new
                    {
                        AppoinmentId = appoint.AppoinmentId,
                        Token = _context.Lawyers.Find(appoint.LawyerId).FirbaseToken,
                    };
                    return Json(Appointment);
                }
               

            }
            catch (Exception ex)
            {

                return Json("Invalid Data");
            }

        }

        //for Confirmed Appointments
        public async Task<JsonResult> RescheduleAppoint(IFormCollection form)
        {

            try
            {
                //Constants con = new Constants();
                //string s =  con. ;
                var appointmentData = JsonConvert.DeserializeObject<Appointment>(Request.Form["Appointment"]);
                var user = JsonConvert.DeserializeObject<obj4>(Request.Form["user"]);
                var appoint = _context.Appointments.Find(appointmentData.AppoinmentId);
                appoint.AppoinmentStatus = appoint.AppoinmentStatus;
                if(appointmentData.LawyerAddressId == null)
                    appoint.LawyerAddress = null;
                appoint.LawyerAddressId = appointmentData.LawyerAddressId;
                appoint.ScheduleDate = appointmentData.ScheduleDate;
                appoint.TimeFrom = appointmentData.TimeFrom;
                appoint.TimeTo = appointmentData.TimeTo;
                appoint.AppoinmentType = appointmentData.AppoinmentType;
                appoint.CaseCharges = appointmentData.CaseCharges;
                appoint.AppoinmentStatus = "Confirmed";
                _context.Appointments.Update(appointmentData);
                await _context.SaveChangesAsync();
                var log = new Log();
                log.Appointment_Id = appointmentData.AppoinmentId;
                log.LogDate = DateTime.Now;
                log.Status = true;
               
                    log.User_id = appoint.LawyerId;
                    log.User_Type = "Lawyer";
                    log.Log_Decs = "Lawyer Reschedule Appointment at " + DateTime.Now.ToShortDateString();
                

                log.Log_Status = appoint.AppoinmentStatus;
                
                await _context.Logs.AddAsync(log);
                await _context.SaveChangesAsync();

                var not = new Notification();
                not.Date = DateTime.Now;
                not.NotificationSeen = false;
                not.Usertype = user.type;
                not.Status = true;
                not.NotificationTypeId = appoint.AppoinmentId;
                not.NotificationType = "Appointment";
                not.NotificationMessage = log.Log_Decs;
                //not.NotificationSubject = "";
                not.UserId = log.User_id;
                _context.Notifications.Add(not);
                _context.SaveChanges();
                // var data = JsonConvert.SerializeObject(new { Data = "Success" });
                return Json("Success!!!");

            }
            catch (Exception ex)
            {

                return Json("Invalid Data");
            }

        }

        
        //for User Contact with Us
        public async Task<JsonResult> UserContWithUs(IFormCollection form)
        {

            try
            {
                ContactUs contact = JsonConvert.DeserializeObject<ContactUs>(Request.Form["Contact"]);
                contact.Date = DateTime.Now;
                contact.Status = true;
                //if (contact.User_Type == "Client")
                //    contact.ContactUs_PhoneNo = _context.Customers.Where(a => a.CustomerId == contact.User_Id).SingleOrDefault().Contact;
                await _context.ContactUs.AddAsync(contact);
                await _context.SaveChangesAsync();
               // var data = JsonConvert.SerializeObject(new { Data = "Success" });
                return Json("Success!!!");

            }
            catch (Exception ex)
            {

                return Json("Invalid Data");
            }

        }
        
        // for Lawyer Intro Data Edit
        public async Task<JsonResult> EditLawyerIntro(IFormCollection form)
        {

            try
            {
                var lawyerData = JsonConvert.DeserializeObject<Lawyer>(Request.Form["Lawyer"]);
                obj o = JsonConvert.DeserializeObject<obj>(form["OtherInfo"]);
                string images = o.ImageName;
                if(images == null)
                {
                    images = "0";
                }
                List<LawyerLanguage> EditLawyerLanguage = JsonConvert.DeserializeObject<LawyerLanguage[]>(form["EditLanguages"].ToString()).ToList();
                List<LawyerLanguage> NewLawyerLanguage = JsonConvert.DeserializeObject<LawyerLanguage[]>(form["NewLanguages"].ToString()).ToList();
                List<LawyerLanguage> DeletedLawyerLanguage = JsonConvert.DeserializeObject<LawyerLanguage[]>(form["DeletedLanguages"].ToString()).ToList();
                var EditLawyer = new List<LawyerLanguage>();
                
                if(DeletedLawyerLanguage.Count()>0)
                    EditLawyer = EditLawyerLanguage.Where(a => !DeletedLawyerLanguage.Any(x=>x.LawyerLanguageId == a.LawyerLanguageId)).ToList();

                var lawyer = await _context.Lawyers.FindAsync(lawyerData.LawyerId);

                lawyer.DateOfBirth = lawyerData.DateOfBirth;
                lawyer.Gender = lawyerData.Gender;
                lawyer.Cnic = lawyerData.Cnic;
                lawyer.City = lawyerData.City;

                var user = _context.Users.Where(s => s.Id == lawyer.AppUserId).SingleOrDefault();
                if (user.Email != o.Email)
                    user.Email = o.Email;
                if (lawyer.FirstName != lawyerData.FirstName || lawyer.LastName != lawyerData.LastName)
                {
                    lawyer.FirstName = lawyerData.FirstName;
                    lawyer.LastName = lawyerData.LastName;
                    Random rndm = new Random();
                    user.UserName = (lawyer.FirstName.ToLower().Replace(" ", "")) + (lawyer.LastName.ToLower().Replace(" ", "")) + rndm.Next(0000, 9999);

                }
                IdentityResult result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    if (images != "0")
                    {
                        //var path = Path.Combine(_env.ContentRootPath, "App_Data/Files");
                        var path = Path.Combine(_env.ContentRootPath, "wwwroot/Uploads");
                        //String path = Path.Combine("~/wwwroot/Uploads"); //Path
                        if (!System.IO.Directory.Exists(path))
                        {
                            System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                        }
                        string Imagename = images + ".jpg";
                        string imgPath = Path.Combine(path, Imagename);

                        byte[] imageBytes = Convert.FromBase64String(lawyerData.ProfilePic);
                        System.IO.File.WriteAllBytes(imgPath, imageBytes);

                        lawyer.ProfilePic = Imagename;

                    }
                    _context.Lawyers.Update(lawyer);
                    await _context.SaveChangesAsync();
                    if (NewLawyerLanguage.Count() > 0)
                    {
                        foreach (var language in NewLawyerLanguage)
                        {
                            await _context.LawyerLanguages.AddAsync(language);
                        }
                        await _context.SaveChangesAsync();
                    }
                    if (EditLawyer.Count() > 0)
                    {
                        foreach (var language in EditLawyer)
                        {
                            var lang = await _context.LawyerLanguages.FindAsync(language.LawyerLanguageId);
                            lang.LanguageNo = language.LanguageNo;
                            lang.Language = language.Language;
                            _context.LawyerLanguages.Update(lang);
                        }
                        await _context.SaveChangesAsync();
                    }
                    if (DeletedLawyerLanguage.Count() > 0)
                    {
                        foreach (var language in DeletedLawyerLanguage)
                        {
                            var lang = await _context.LawyerLanguages.FindAsync(language.LawyerLanguageId);
                            _context.LawyerLanguages.Remove(lang);
                        }
                        
                        await _context.SaveChangesAsync();
                    }

                }
                    var LawyerData = new
                {
                   Updated= true

                };

                return Json(LawyerData);

            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }

        }

        // For Lawyer Education Data Edit
        public async Task<JsonResult> EditLawyerEducations(IFormCollection form)
        {
            try
            {
                List<LawyerQualification> EditLawyerQualification = JsonConvert.DeserializeObject<LawyerQualification[]>(form["EditEducation"].ToString()).ToList();
                List<LawyerQualification> NewLawyerQualification = JsonConvert.DeserializeObject<LawyerQualification[]>(form["NewEducation"].ToString()).ToList();
                List<LawyerQualification> DeletedLawyerQualification = JsonConvert.DeserializeObject<LawyerQualification[]>(form["DeletedEducation"].ToString()).ToList();
              
                
               // var EditLawyer = EditLawyerQualification.Where(a => !DeletedLawyerQualification.Any(x => x.LawyerQualificationId == a.LawyerQualificationId)).ToList();

                if (NewLawyerQualification.Count() > 0)
                {
                    foreach (var education in NewLawyerQualification)
                    {
                        if (education.SpecializationId == 0 || education.SpecializationId == null)
                        {
                            education.Check = true;
                            //await _context.LawyerQualifications.AddAsync(education);

                        }
                        else
                        {
                            education.Check = false;
                        }
                        await _context.LawyerQualifications.AddAsync(education);
                    }
                    await _context.SaveChangesAsync();
                }

                if (EditLawyerQualification.Count() > 0)
                {
                    foreach (var education in EditLawyerQualification)
                    {
                        var edu = await _context.LawyerQualifications.FindAsync(education.LawyerQualificationId);
                        if (education.SpecializationId == 0 || education.SpecializationId == null)
                        {
                            edu.Check = true;
                        }
                        else
                        {
                            edu.Check = false;
                        }
                        edu.DegreeId = education.DegreeId;
                        edu.DegreeTypeId = education.DegreeTypeId;
                        edu.SpecializationId = education.SpecializationId;
                        edu.CompletionYear = education.CompletionYear;
                        _context.LawyerQualifications.Update(edu);
                    }
                  
                    await _context.SaveChangesAsync();
                }

                if (DeletedLawyerQualification.Count() > 0)
                {
                    foreach (var education in DeletedLawyerQualification)
                    {
                        var edu = await _context.LawyerQualifications.FindAsync(education.LawyerQualificationId);
                       
                        _context.LawyerQualifications.Remove(edu);
                    }
                    
                    await _context.SaveChangesAsync();
                }


                var LawyerData = new
                {
                    Updete = true
                };

                return Json(LawyerData);

            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }

        }

        // for Lawyer LawField Data Edit
        public async Task<JsonResult> EditLawyerLawField(IFormCollection form)
        {
            try
            {
                var EditServices = JsonConvert.DeserializeObject<LawyerCaseCategory[]>(form["EditService"].ToString()).ToList();
                var NewServices = JsonConvert.DeserializeObject<LawyerCaseCategory[]>(form["NewService"].ToString()).ToList();
                var DeletedServices = JsonConvert.DeserializeObject<LawyerCaseCategory[]>(form["DeletedService"].ToString()).ToList();

                //var EditServices1 = EditServices.Where(a => !DeletedServices.Any(x => x.LawyerCaseCategoryId == a.LawyerCaseCategoryId)).ToList();


                List<LawyerExperience> EditExperiences = JsonConvert.DeserializeObject<LawyerExperience[]>(form["EditExperience"].ToString()).ToList();
                List<LawyerExperience> NewExperiences = JsonConvert.DeserializeObject<LawyerExperience[]>(form["NewExperience"].ToString()).ToList();
                List<LawyerExperience> DeletedExperiences = JsonConvert.DeserializeObject<LawyerExperience[]>(form["DeletedExperience"].ToString()).ToList();

               // var EditExperiences1 = EditExperiences.Where(a => !DeletedExperiences.Any(x => x.LawyerExperienceId == a.LawyerExperienceId)).ToList();


                List<LawyerLicense> EditLawyerLicense = JsonConvert.DeserializeObject<LawyerLicense[]>(form["EditLicense"].ToString()).ToList();
                List<LawyerLicense> DeletedLawyerLicense = JsonConvert.DeserializeObject<LawyerLicense[]>(form["DeletedLicense"].ToString()).ToList();
                List<LawyerLicense> NewLawyerLicense = JsonConvert.DeserializeObject<LawyerLicense[]>(form["NewLicense"].ToString()).ToList();
               

               // var EditLawyerLicense1 = EditLawyerLicense.Where(a => !DeletedLawyerLicense.Any(x => x.LawyerLicenseId == a.LawyerLicenseId)).ToList();


                var lawyerData = JsonConvert.DeserializeObject<Lawyer>(Request.Form["Lawyer"]);

                var lawyer = await _context.Lawyers.FindAsync(lawyerData.LawyerId);

                lawyer.TotalExperience = lawyerData.TotalExperience;

                _context.Lawyers.Update(lawyer);
                await _context.SaveChangesAsync();

                if (NewServices.Count() > 0)
                {
                    foreach (var service in NewServices)
                    {
                        await _context.LawyerCaseCategories.AddAsync(service);
                    }
                    await _context.SaveChangesAsync();
                }
                if (EditServices.Count() > 0)
                {
                    //foreach (var service in EditServices)
                    //{
                    //    await _context.LawyerCaseCategories.UpdateAsync(service);
                    //}
                    _context.LawyerCaseCategories.UpdateRange(EditServices);
                    await _context.SaveChangesAsync();
                }
                if (DeletedServices.Count() > 0)
                {
                    //foreach (var service in DeletedServices)
                    //{
                    //    var ser = await _context.LawyerCaseCategories.FindAsync(service.LawyerCaseCategoryId);
                    //    _context.LawyerCaseCategories.Remove(ser);
                    //    await _context.SaveChangesAsync();
                    //}
                    _context.LawyerCaseCategories.RemoveRange(DeletedServices);
                    await _context.SaveChangesAsync();
                }

                //Experience code
                if (NewExperiences.Count() > 0)
                {
                    foreach (var experience in NewExperiences)
                    {
                        await _context.LawyerExperiences.AddAsync(experience);
                    }
                    await _context.SaveChangesAsync();
                }
                if (EditExperiences.Count() > 0)
                {

                    _context.LawyerExperiences.UpdateRange(EditExperiences);
                    await _context.SaveChangesAsync();
                }
                if (DeletedExperiences.Count() > 0)
                {
                    //foreach (var experience in DeletedExperiences)
                    //{
                    //    var experience1 = await _context.LawyerExperiences.FindAsync(experience.LawyerExperienceId);
                    //    _context.LawyerExperiences.Remove(experience1);
                    //}
                    _context.LawyerExperiences.RemoveRange(DeletedExperiences);
                    await _context.SaveChangesAsync();
                }

                if (NewLawyerLicense.Count() > 0)
                {
                    foreach (var license in NewLawyerLicense)
                    {
                        await _context.LawyerLicenses.AddAsync(license);
                    }
                    await _context.SaveChangesAsync();
                }
                if (EditLawyerLicense.Count() > 0)
                {
                    _context.LawyerLicenses.UpdateRange(EditLawyerLicense);
                    await _context.SaveChangesAsync();
                }
                if (DeletedLawyerLicense.Count() > 0)
                {
                    //foreach (var license in DeletedLawyerLicense)
                    //{
                    //    var lic = await _context.LawyerLicenses.FindAsync(license.LawyerLicenseId);
                    //    _context.LawyerLicenses.Remove(lic);
                    //}
                    _context.LawyerLicenses.RemoveRange(DeletedLawyerLicense);
                    await _context.SaveChangesAsync();
                }

                var LawyerData = new
                {
                    Updete = true
                };


                return Json(LawyerData);

            }
            catch (Exception ex)
            {

                return Json("Invalid Data");
            }
        }

        // for Lawyer About Data Edit
        public async Task<JsonResult> EditLawyerAbout(IFormCollection form)
        {

            try
           {
                List<LawyerClient> EditLawyerClient = JsonConvert.DeserializeObject<LawyerClient[]>(form["EditRenownedClient"].ToString()).ToList();
                List<LawyerClient> NewLawyerClient = JsonConvert.DeserializeObject<LawyerClient[]>(form["NewRenownedClient"].ToString()).ToList();
                List<LawyerClient> DeletedLawyerClient = JsonConvert.DeserializeObject<LawyerClient[]>(form["DeletedRenownedClient"].ToString()).ToList();

                //var EditLawyerClient1 = EditLawyerClient.Where(a => !DeletedLawyerClient.Any(x => x.LawyerClientId == a.LawyerClientId)).ToList();


                var lawyerData = JsonConvert.DeserializeObject<Lawyer>(Request.Form["Lawyer"]);

                var lawyer = await _context.Lawyers.FindAsync(lawyerData.LawyerId);

                lawyer.Biography = lawyerData.Biography;

                _context.Lawyers.Update(lawyer);
                await _context.SaveChangesAsync();

                if (NewLawyerClient.Count() > 0)
                {
                    foreach (var client in NewLawyerClient)
                    {
                        await _context.LawyerClients.AddAsync(client);
                    }
                    await _context.SaveChangesAsync();
                }
                if (EditLawyerClient.Count() > 0)
                {
                    _context.LawyerClients.UpdateRange(EditLawyerClient);
                    await _context.SaveChangesAsync();
                }
                if (DeletedLawyerClient.Count() > 0)
                {
                    _context.LawyerClients.RemoveRange(DeletedLawyerClient);
                    await _context.SaveChangesAsync();
                }

                var LawyerData = new
                {
                   update = true
                };

                return Json(LawyerData);

            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }

        }

        public async Task<JsonResult> EditLawyerAddress(IFormCollection form)
        {

            try
            {
                List<LawyerAddress> EditLawyerAddress = JsonConvert.DeserializeObject<LawyerAddress[]>(form["EditOfficeAddresses"].ToString()).ToList();
                List<LawyerAddress> NewLawyerAddress = JsonConvert.DeserializeObject<LawyerAddress[]>(form["NewOfficeAddresses"].ToString()).ToList();
                List<LawyerAddress> DeletedLawyerAddress = JsonConvert.DeserializeObject<LawyerAddress[]>(form["DeletedOfficeAddresses"].ToString()).ToList();

                //var EditLawyerAddress1 = EditLawyerAddress.Where(a => !DeletedLawyerAddress.Any(x => x.LawyerAddressId == a.LawyerAddressId)).ToList();


                if (NewLawyerAddress.Count() > 0)
                {
                    //foreach (var address in NewLawyerAddress)
                    //{
                    //    await _context.LawyerAddresses.AddAsync(address);
                    //}
                    await _context.LawyerAddresses.AddRangeAsync(NewLawyerAddress);
                    await _context.SaveChangesAsync();
                }
                if (EditLawyerAddress.Count() > 0)
                {
                    _context.LawyerAddresses.UpdateRange(EditLawyerAddress);
                    await _context.SaveChangesAsync();
                }
                if (DeletedLawyerAddress.Count() > 0)
                {
                    _context.LawyerAddresses.RemoveRange(DeletedLawyerAddress);
                    await _context.SaveChangesAsync();
                }

                var LawyerData = new
                {
                   updete=true
                };

                return Json(LawyerData);

            }
            catch (Exception ex)
            {

                return Json("Invalid Data");
            }

        }

        public async Task<JsonResult> EditLawyerPackages(IFormCollection form)
        {

            try
            {
                List<LawyerTiming> LawyerPackage = JsonConvert.DeserializeObject<LawyerTiming[]>(form["Packages"].ToString()).ToList();
                //List<LawyerTiming> NewLawyerPackage = JsonConvert.DeserializeObject<LawyerTiming[]>(form["NewPackages"].ToString()).ToList();
                //List<LawyerTiming> DeletedLawyerPackage = JsonConvert.DeserializeObject<LawyerTiming[]>(form["DeletedPackages"].ToString()).ToList();

                //var EditLawyerPackage1 = EditLawyerPackage.Where(a => !DeletedLawyerPackage.Any(x => x.LawyerTimingId == a.LawyerTimingId)).ToList();
                var Packages = _context.LawyerTimings.Where(a => a.LawyerId == LawyerPackage[0].LawyerId && a.SlotDate == LawyerPackage[0].SlotDate && a.Status).ToList();

                var editPackages = LawyerPackage.Where(a => Packages.Any(x => x.LawyerTimingId == a.LawyerTimingId)).ToList();
               // var editPackages2 = Packages.Where(a => LawyerPackage.Any(x => x.LawyerTimingId == a.LawyerTimingId)).ToList();

                var delPackages = Packages.Where(a => !LawyerPackage.Any(x => x.LawyerTimingId == a.LawyerTimingId)).ToList();

                var NewPackages = LawyerPackage.Where(a => a.LawyerTimingId==0).ToList();



                if (NewPackages.Count() > 0)
                {
                    foreach (var packages in NewPackages)
                    {
                        if (packages.SlotType.ToLower() == "inperson")
                        {
                            packages.Check2 = false;
                        }
                        else
                        {
                            packages.Check2 = true;
                            packages.Location = "Online Video Consultation (Online)";
                        }

                        packages.Status = true;
                        await _context.LawyerTimings.AddAsync(packages);
                    }
                    await _context.SaveChangesAsync();
                }
                if (editPackages.Count() > 0)
                {
                    foreach (var packages in editPackages)
                    {
                        var pak = Packages.Where(a => a.LawyerTimingId == packages.LawyerTimingId).SingleOrDefault();
                        pak.Charges = packages.Charges;
                        pak.Day = packages.Day;
                        pak.EndTime24 = packages.EndTime24;
                        pak.InternationalCharges = packages.InternationalCharges;
                        pak.InternationalIndex = packages.InternationalIndex;
                        pak.LocalIndex = packages.LocalIndex;
                        pak.Location = packages.Location;
                        pak.SlotDate = packages.SlotDate;
                        pak.SlotType = packages.SlotType;
                        pak.StartTime24 = packages.StartTime24;
                        pak.TimeFrom = packages.TimeFrom;
                        pak.TimeTo = packages.TimeTo;
                        if (packages.SlotType.ToLower() == "inperson")
                        {
                            pak.Check2 = false;
                            pak.Location = "";
                        }
                        else
                        {
                            pak.Check2 = true;
                            pak.Location = "Online Video Consultation (Online)";
                        }

                        _context.LawyerTimings.Update(pak);
                    }
                   
                    await _context.SaveChangesAsync();
                }
                if (delPackages.Count() > 0)
                {
                    foreach (var packages in delPackages)
                    {
                        packages.Status = false;
                        _context.LawyerTimings.Update(packages);
                    }
                    
                    //_context.LawyerTimings.RemoveRange(delPackages);
                    await _context.SaveChangesAsync();
                }

                var LawyerData = new
                {
                    updete = true
                };

                return Json(LawyerData);

            }
            catch (Exception ex)
            {

                return Json("Invalid Data");
            }

        }
        // for Lawyer Documents Data
        public async Task<JsonResult> EditLawyerDocuments(IFormCollection form)
        {

            try
            {
                List<LawyerCertificatePic> EditCertificates = JsonConvert.DeserializeObject<LawyerCertificatePic[]>(form["EditCertificates"].ToString()).ToList();
                List<LawyerCertificatePic> NewCertificates = JsonConvert.DeserializeObject<LawyerCertificatePic[]>(form["NewCertificates"].ToString()).ToList();
                List<LawyerCertificatePic> DeletedCertificates = JsonConvert.DeserializeObject<LawyerCertificatePic[]>(form["DeletedCertificates"].ToString()).ToList();
                
                //var EditCertificates1 = EditCertificates.Where(a => !DeletedCertificates.Any(x => x.LawyerCertificatePicId == a.LawyerCertificatePicId)).ToList();

                var lawyerData = JsonConvert.DeserializeObject<Lawyer>(Request.Form["Lawyer"]);

                obj2 obj2 = JsonConvert.DeserializeObject<obj2>(form["MainImages"]);

                List<obj3> Editobj3 = JsonConvert.DeserializeObject<List<obj3>>(form["EditCertificateImages"]);
                List<obj3> Newobj3 = JsonConvert.DeserializeObject<List<obj3>>(form["NewCertificateImages"]);
                List<obj3> Deletedobj3 = JsonConvert.DeserializeObject<List<obj3>>(form["DeletedCertificateImages"]);

                //var Editobj31 = Editobj3.Where(a => !Deletedobj3.Any(x => x. == a.LawyerCertificatePicId)).ToList();

                var path = Path.Combine(_env.ContentRootPath, "wwwroot/Uploads");
                string Imagename;
                string imgPath;
                byte[] imageBytes;
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path); //Create directory if it doesn't exist
                }

                var i = 0;
                if (NewCertificates.Count() > 0)
                {
                    foreach (var certificate in NewCertificates)
                    {
                        if (Newobj3[i].CertificateImageName != "0")
                        {
                            Imagename = Newobj3[i].CertificateImageName + ".jpg";
                            imgPath = Path.Combine(path, Imagename);
                            imageBytes = Convert.FromBase64String(certificate.CertificatePic);
                            System.IO.File.WriteAllBytes(imgPath, imageBytes);
                            certificate.CertificatePic = Imagename;
                        }
                        i++;
                        await _context.LawyerCertificatePics.AddAsync(certificate);
                    }
                    await _context.SaveChangesAsync();
                    i = 0;
                }
                if (EditCertificates.Count() > 0)
                {
                    foreach (var certificate in EditCertificates)
                    {
                        if (Editobj3[i].CertificateImageName != "0")
                        {
                            Imagename = Editobj3[i].CertificateImageName + ".jpg";
                            imgPath = Path.Combine(path, Imagename);
                            imageBytes = Convert.FromBase64String(certificate.CertificatePic);
                            System.IO.File.WriteAllBytes(imgPath, imageBytes);
                            certificate.CertificatePic = Imagename;
                        }
                        i++;
                        _context.LawyerCertificatePics.Update(certificate);
                    }
                    await _context.SaveChangesAsync();
                    i = 0;
                }

                if (DeletedCertificates.Count() > 0)
                {
                    _context.LawyerCertificatePics.RemoveRange(DeletedCertificates);
                    await _context.SaveChangesAsync();
                }

                var lawyer = _context.Lawyers.Where(a => a.LawyerId == lawyerData.LawyerId).SingleOrDefault();
                if (obj2.CnicFrontImageName == null)
                {
                    obj2.CnicFrontImageName = "0";
                }

                if (obj2.CnicBackImageName == null)
                {
                    obj2.CnicBackImageName = "0";
                }

                if (obj2.RecentDegreeImageName == null)
                {
                    obj2.RecentDegreeImageName = "0";
                }

                if (obj2.CnicFrontImageName != "0")
                {
                    
                    Imagename = obj2.CnicFrontImageName + ".jpg";
                    imgPath = Path.Combine(path, Imagename);
                    imageBytes = Convert.FromBase64String(lawyerData.CnicFrontPic);
                    System.IO.File.WriteAllBytes(imgPath, imageBytes);
                    lawyer.CnicFrontPic = Imagename;
                }
                if (obj2.CnicBackImageName != "0")
                {
                    Imagename = obj2.CnicBackImageName + ".jpg";
                    imgPath = Path.Combine(path, Imagename);
                    imageBytes = Convert.FromBase64String(lawyerData.CnicBackPic);
                    System.IO.File.WriteAllBytes(imgPath, imageBytes);
                    lawyer.CnicBackPic = Imagename;
                }

                if (obj2.RecentDegreeImageName != "0")
                {
                    Imagename = obj2.RecentDegreeImageName + ".jpg";
                    imgPath = Path.Combine(path, Imagename);
                    imageBytes = Convert.FromBase64String(lawyerData.RecentDegreePic);
                    System.IO.File.WriteAllBytes(imgPath, imageBytes);
                    lawyer.RecentDegreePic = Imagename;
                }


                _context.Lawyers.Update(lawyer);
                await _context.SaveChangesAsync();

                var LawyerData = new
                {
                   updete =true

                };


                return Json(LawyerData);

            }
            catch (Exception ex)
            {

                return Json("Invalid Data");
            }

        }

        [HttpGet]
        public async Task<JsonResult> GetLawyerStatus(int lawyerid)
        {
            try
            {

                var lawyer = await _context.Lawyers.FindAsync(lawyerid);
                var LawyerData = new
                {
                    IntroStatus = _context.LawyerLanguages.Any(a => a.LawyerId == lawyer.LawyerId),
                    EducationStatus = _context.LawyerQualifications.Any(a => a.LawyerId == lawyer.LawyerId),
                    LawFieldStatus = _context.LawyerExperiences.Any(a => a.LawyerId == lawyer.LawyerId),
                    AboutStatus = _context.LawyerClients.Any(a => a.LawyerId == lawyer.LawyerId),
                    AddOfficeStatus = _context.LawyerAddresses.Any(a => a.LawyerId == lawyer.LawyerId),
                    PackageStatus = _context.LawyerTimings.Any(a => a.LawyerId == lawyer.LawyerId),
                    DocumentStatus = _context.LawyerCertificatePics.Any(a => a.LawyerId == lawyer.LawyerId),

                };

                return Json(LawyerData);
            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }
        }
        [HttpGet]
        public async Task<JsonResult> GetLawyerPackges(int lawyerid)
        {
            try
            {

               var LawyerPackages = _context.LawyerTimings.OrderByDescending(a => a.LawyerAddressId).Where(t => t.LawyerId == lawyerid && t.Status).Select(p => new
                {
                    Day = p.Day,
                    StartTime = p.TimeFrom,
                    EndTime = p.TimeTo,
                    Location = p.Location,
                    PackageType = p.SlotType,
                    LocalCharges = p.Charges,
                    OfficeAddressId = p.LawyerAddressId,
                    OfficeAddress = p.LawyerAddress.Address,
                    Check1 = p.Check,
                    Check2 = p.Check2,
                    InternationalCharges = p.InternationalCharges,
                    SlotDate = p.SlotDate,
                    InternationalIndex = p.InternationalIndex,
                    LocalIndex = p.LocalIndex,
                    StartTime24 = p.StartTime24,
                    EndTime24 = p.EndTime24
                }).ToList();
                return Json(LawyerPackages);
            }
            catch (Exception ex)
            {
                return Json("Invalid Data");
            }
        }

    }
}



