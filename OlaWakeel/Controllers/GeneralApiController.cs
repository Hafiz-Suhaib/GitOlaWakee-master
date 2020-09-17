using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OlaWakeel.Data;
using OlaWakeel.Data.ApplicationUser;
using OlaWakeel.Models;

namespace OlaWakeel.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GeneralApiController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        public class obj
        {
            public int LawyerId;
            public string FirstName;
            public string LastName;
            public string LawyerPic;
            public LawyerCaseCategory Services;
        }
        public class obj1
        {
            public int LawyerId;
        }
        public class obj2
        {
            public int AppointmentId;
            public DateTime RescheduleDate;
            public string token, act, body, title, user, TimeTo, TimeFrom, AppointmentDay, Id;
        }

        private readonly ApplicationDbContext _context;

        public GeneralApiController(ApplicationDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private static int CalculateAge(DateTime dateOfBirth)
        {
            int age = 0;
            age = DateTime.Now.Year - dateOfBirth.Year;
            if (DateTime.Now.DayOfYear < dateOfBirth.DayOfYear)
                age = age - 1;

            return age;
        }

        [HttpGet]
        [Route("ClientExist")]
        public ActionResult ClientExist(string PhoneNo)
        {
            try
            {
                var configs = new { does_exist = _context.Customers.Any(a => a.Contact == PhoneNo) };

                var lawyerExist = JsonConvert.SerializeObject(configs);
                return Ok(lawyerExist);
            }
            catch (Exception e)
            {
                return Ok("Invalid Data");
            }
        }

        [HttpGet]
        [Route("LawyerExist")]
        public ActionResult LawyerExist(string PhoneNo)
        {
            try
            {
                if (_context.Lawyers.Any(a => a.Contact == PhoneNo))
                {

                    var lawyer = _context.Lawyers.Where(a => a.Contact == PhoneNo).Select(x => new
                    {
                        LawyerId = x.LawyerId,
                        lawyerName = x.FirstName + " " + x.LastName,
                        LawyerPic = x.ProfilePic,
                        OnlineStatus = x.OnlineStatus,
                        //IntroStatus = _context.LawyerLanguages.Any(a => a.LawyerId == x.LawyerId),
                        //EducationStatus = _context.LawyerQualifications.Any(a => a.LawyerId == x.LawyerId),
                        //LawFieldStatus = _context.LawyerExperiences.Any(a => a.LawyerId == x.LawyerId),
                        //AboutStatus = _context.LawyerClients.Any(a => a.LawyerId == x.LawyerId),
                        //AddOfficeStatus = _context.LawyerAddresses.Any(a => a.LawyerId == x.LawyerId),
                        //PackageStatus = _context.LawyerTimings.Any(a => a.LawyerId == x.LawyerId),
                        //DocumentStatus = _context.LawyerCertificatePics.Any(a => a.LawyerId == x.LawyerId),
                    }).FirstOrDefault();
                    var configs = new
                    {
                        does_exist = true,
                        lawyer = lawyer,

                    };
                    var lawyerExist1 = JsonConvert.SerializeObject(configs);
                    return Ok(lawyerExist1);


                }

                var lawyerExist = JsonConvert.SerializeObject(new { does_exist = false });
                return Ok(lawyerExist);
            }
            catch (Exception e)
            {
                return Ok("Invalid Data");
            }

        }

        //for clients side  
        [HttpGet]
        [Route("ClientData")]
        public ActionResult ClientData(string PhoneNo)
        {
            try
            {
                Customer customer = _context.Customers.Where(a => a.Contact == PhoneNo).SingleOrDefault();
                var Customer = new
                {
                    CustomerId = customer.CustomerId,
                    FirstName = customer.FirstName,
                    LastName = customer.LastName,
                    ProfilePic = customer.ProfilePic,
                    AppUserId = customer.AppUserId
                };
                if (customer != null)
                    return Ok(Customer);
                var configs = new { Customer = "Not Exist" };

                var lawyerExist = JsonConvert.SerializeObject(configs);
                return Ok(lawyerExist);
            }
            catch (Exception e)
            {
                return Ok("Invalid Data");
            }
        }

        [HttpGet]
        [Route("GetLawyers")]
        public ActionResult GetLawyers()
        {
            try
            {
                _context.Database.SetCommandTimeout(0);
                var LawyerData = _context.Lawyers.Select(x => new
                {
                    LawyerId = x.LawyerId,
                    LawyerName = x.FirstName + " " + x.LastName,
                    LawyerPic = x.ProfilePic,
                    TotalExperience = x.TotalExperience,
                    Rating = x.Rating,
                    LawyerGender = x.Gender,
                    OnlineStatus = x.OnlineStatus,
                    FirbaseToken = x.FirbaseToken,
                    ServiceProvide = _context.LawyerCaseCategories.Where(sp => sp.LawyerId == x.LawyerId).Select(c => new
                    {
                        ServiceName = c.CaseCategory.Name,
                        LawyerServiceId = c.LawyerCaseCategoryId,
                        CaseCategoryId = c.CaseCategoryId,
                    }).ToList(),
                    LawyerPackages = _context.LawyerTimings.OrderByDescending(or => or.LawyerAddressId).Where(t => t.LawyerId == x.LawyerId).Select(p => new
                    {
                        Day = p.Day,
                        StartTime = p.TimeFrom,
                        EndTime = p.TimeTo,
                        Location = p.Location,
                        PackageType = p.SlotType,
                        Fee = p.Charges,
                        OfficeAddressId = p.LawyerAddressId,
                        OfficeAddress = p.LawyerAddress.Address,
                        Check1 = p.Check,
                        Check2 = p.Check2
                    }).ToList(),
                    LawyerDegree = _context.LawyerQualifications.Where(de => de.LawyerId == x.LawyerId).Select(d => new
                    {
                        DegreeName = d.Degree.Name
                    }).ToList(),
                    LawyerLanguages = _context.LawyerLanguages.Where(a => a.LawyerId == x.LawyerId).Select(la => new
                    {
                        Language = la.Language,
                    }).ToList(),
                }).ToList();

                //var LawyerAll = new { };
                // var Data = new Tuple<List<LawyerCaseCategory>, List<LawyerTiming>, List<LawyerQualification>, List<LawyerLanguage>>(ServiceProvide, LawyerPackages, LawyerDegree, LawyerLanguages);
                //List<Tuple<Lawyer,List<LawyerCaseCategory>, List<LawyerTiming>, List<LawyerQualification>, List<LawyerLanguage>>> obj = new List<Tuple<Lawyer, List<LawyerCaseCategory>, List<LawyerTiming>, List<LawyerQualification>, List<LawyerLanguage>>>();
                //int i = 0;
                //foreach (var x in LawyerData)
                //{
                //    LawyerAll= new[int,string,string,int, int,string,bool,List<LawyerCaseCategory>,]
                //    {
                //        LawyerId = x.LawyerId,
                //        LawyerName = x.FirstName + " " + x.LastName,
                //        LawyerPic = x.ProfilePic,
                //        TotalExperience = x.TotalExperience,
                //        Rating = x.Rating,
                //        LawyerGender = x.Gender,
                //        OnlineStatus = x.OnlineStatus,
                //        ServiceProvide = _context.LawyerCaseCategories.Where(sp => sp.LawyerId == x.LawyerId).Select(c => new
                //        {
                //            ServiceName = c.CaseCategory.Name,
                //            LawyerServiceId = c.LawyerCaseCategoryId,
                //            CaseCategoryId = c.CaseCategoryId,
                //        }).ToList(),
                //        LawyerPackages = _context.LawyerTimings.OrderByDescending(a => a.LawyerAddressId).Where(t => t.LawyerId == x.LawyerId).Select(p => new
                //        {
                //            Day = p.Day,
                //            StartTime = p.TimeFrom,
                //            EndTime = p.TimeTo,
                //            Location = p.Location,
                //            PackageType = p.SlotType,
                //            Fee = p.Charges,
                //            OfficeAddressId = p.LawyerAddressId,
                //            OfficeAddress = p.LawyerAddress.Address,
                //            Check1 = p.Check,
                //            Check2 = p.Check2
                //        }).ToList(),
                //        LawyerDegree = _context.LawyerQualifications.Where(de => de.LawyerId == x.LawyerId).Select(d => new
                //        {
                //            DegreeName = d.Degree.Name
                //        }).ToList(),
                //        LawyerLanguages = _context.LawyerLanguages.Where(a => a.LawyerId == x.LawyerId).Select(la => new
                //        {
                //            Language = la.Language,
                //        }).ToList(),
                //    };


                //}
                //var LawyerAllData = new
                //{
                //    LawyerData = LawyerData,
                //    LawyerLanguages = LawyerLanguages,
                //    LawyerDegree = LawyerDegree,
                //    LawyerPackages = LawyerPackages,
                //    ServiceProvide = ServiceProvide,
                //};

                return Ok(LawyerData);
            }
            catch (Exception e)
            {
                var configs = new { Lawyer = "Not Exist" };

                return Ok(JsonConvert.SerializeObject(configs));
            }


        }

        //[HttpGet]
        //[Route("CategoryOnes")]
        //public HttpResponseMessage CategoryOne()
        //{



        //    try
        //    {
        //        var response = new HttpResponseMessage(HttpStatusCode.OK);

        //        var LawyerData = _context.Lawyers.ToList().Select(x => new {
        //            LawyerId = x.LawyerId,
        //            LawyerName = x.FirstName + x.LastName,
        //            LawyerPic = x.ProfilePic,
        //            TotalExperience = x.TotalExperience,
        //            Rating = x.Rating,

        //            ServiceProvide = x.LawyerCaseCategories.Select(c=>new{ ServiceName= c.CaseCategory.Name }).ToList(),
        //            LawyerDegree = x.LawyerQualifications.Select(d=>new { DegreeName = d.Degree.Name }).ToList(),
        //            LawyerPackages = x.LawerTimings.ToList(),
        //            OnlineStatus = x.OnlineStatus,


        //        });
        //        var DegreeData = _context.Degrees.ToList();

        //        response.Content = new StringContent(JsonConvert.SerializeObject(new
        //        {
        //            LawyersData = LawyerData,
        //            DegreeData = DegreeData


        //        }));
        //        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        return response;
        //    }
        //    catch
        //    {
        //        return new HttpResponseMessage(HttpStatusCode.BadGateway);
        //    }
        //}

        [HttpGet]
        [Route("GetLawyerDetails")]
        public ActionResult GetLawyerDetails(int lawyerid)
        {
            try
            {
                _context.Database.SetCommandTimeout(0);
                var LawyerData = _context.Lawyers.Where(law => law.LawyerId == lawyerid).Select(x => new
                {

                    LawyerName = x.FirstName + " " + x.LastName,
                    LawyerPic = x.ProfilePic,
                    LawyerCity = x.City,
                    Rating = x.Rating,
                    OnlineStatus = x.OnlineStatus,
                    FirbaseToken = x.FirbaseToken,
                    ServiceProvide = _context.LawyerCaseCategories.Where(sp => sp.LawyerId == x.LawyerId).Select(c => new
                    {
                        ServiceName = c.CaseCategory.Name,
                        LawyerServiceId = c.LawyerCaseCategoryId,
                        CaseCategoryId = c.CaseCategoryId,
                    }).ToList(),
                    LawyerDegree = _context.LawyerQualifications.Where(de => de.LawyerId == x.LawyerId).Select(d => new { DegreeType = d.Degree.DegreeTypes.TypeName, DegreeYear = d.Degree.EligibleAfter, CompletionYear = d.CompletionYear, DegreeName = d.Degree.Name }).ToList(),
                    LawyerPackages = _context.LawyerTimings.OrderByDescending(a => a.LawyerAddressId).Where(t => t.LawyerId == x.LawyerId).Select(p => new
                    {
                        Day = p.Day,
                        StartTime = p.TimeFrom,
                        EndTime = p.TimeTo,
                        Location = p.Location,
                        PackageType = p.SlotType,
                        Fee = p.Charges,
                        OfficeAddressId = p.LawyerAddressId,
                        OfficeAddress = p.LawyerAddress.Address,
                        Check1 = p.Check,
                        Check2 = p.Check2
                    }).ToList(),
                    OfficeLocation = _context.LawyerAddresses.Where(a => a.LawyerId == lawyerid).Select(loc => new
                    {
                        LawyerAddressId = loc.LawyerAddressId,
                        Address = loc.Address
                    }).ToList(),
                    Experience = _context.LawyerExperiences.Where(le => le.LawyerId == x.LawyerId).Select(e => new { ExperienceField = e.CaseCategory.Name, ExperienceYear = e.ExperienceYears }).ToList(),
                    AboutUs = x.Biography,
                    ReownedClient = _context.LawyerClients.Where(a => a.LawyerId == x.LawyerId).Select(c => new { ClientName = c.ClientName }).ToList(),

                }).SingleOrDefault();
                int[] id = new int[4];
                int index = 0;
                foreach (var newService in LawyerData.ServiceProvide.Take(2))
                {
                    var data1 = _context.LawyerCaseCategories.Where(a => a.LawyerId != lawyerid && a.CaseCategoryId == newService.CaseCategoryId).Take(2).Select(x => x.LawyerId);
                    foreach (var d in data1)
                    {
                        id[index++] = d;
                    }
                }

                var lawyer1 = _context.Lawyers.Where(law => law.LawyerId == id[0]).Select(x => new
                {
                    LawyerName = x.FirstName + " " + x.LastName,
                    LawyerPic = x.ProfilePic,
                    LawyerId = x.LawyerId,
                    ServiceProvide = _context.LawyerCaseCategories.Where(sp => sp.LawyerId == x.LawyerId).Select(c => new
                    {
                        ServiceName = c.CaseCategory.Name,
                        LawyerServiceId = c.LawyerCaseCategoryId,
                        CaseCategoryId = c.CaseCategoryId,

                    }).ToList(),

                }).SingleOrDefault();
                var lawyer2 = _context.Lawyers.Where(law => law.LawyerId == id[1]).Select(x => new
                {
                    LawyerName = x.FirstName + " " + x.LastName,
                    LawyerPic = x.ProfilePic,
                    LawyerId = x.LawyerId,
                    ServiceProvide = _context.LawyerCaseCategories.Where(sp => sp.LawyerId == x.LawyerId).Select(c => new
                    {
                        ServiceName = c.CaseCategory.Name,
                        LawyerServiceId = c.LawyerCaseCategoryId,
                        CaseCategoryId = c.CaseCategoryId,
                    }).ToList(),


                }).SingleOrDefault();
                //SimilarLawyer.Add(lawyer3data);
                var lawyer3 = _context.Lawyers.Where(law => law.LawyerId == id[2]).Select(x => new
                {
                    LawyerName = x.FirstName + " " + x.LastName,
                    LawyerPic = x.ProfilePic,
                    LawyerId = x.LawyerId,
                    ServiceProvide = _context.LawyerCaseCategories.Where(sp => sp.LawyerId == x.LawyerId).Select(c => new
                    {
                        ServiceName = c.CaseCategory.Name,
                        LawyerServiceId = c.LawyerCaseCategoryId,
                        CaseCategoryId = c.CaseCategoryId,
                    }).ToList(),


                }).SingleOrDefault();
                var lawyer4 = _context.Lawyers.Where(law => law.LawyerId == id[3]).Select(x => new
                {
                    LawyerName = x.FirstName + " " + x.LastName,
                    LawyerPic = x.ProfilePic,
                    LawyerId = x.LawyerId,
                    ServiceProvide = _context.LawyerCaseCategories.Where(sp => sp.LawyerId == x.LawyerId).Select(c => new
                    {
                        ServiceName = c.CaseCategory.Name,
                        LawyerServiceId = c.LawyerCaseCategoryId,
                        CaseCategoryId = c.CaseCategoryId,
                    }).ToList(),


                }).SingleOrDefault();
                var SemilarLawyer = new[] { lawyer1, lawyer2, lawyer3, lawyer4 };
                var LawyerDetail = new
                {
                    LawyerData,
                    SemilarLawyer
                };
                if (LawyerData != null)
                    return Ok(JsonConvert.SerializeObject(LawyerDetail));
                var configs = new { Lawyer = "Not Exist" };

                return Ok(JsonConvert.SerializeObject(configs));
            }
            catch (Exception e)
            {
                return Ok(e.Message);
            }
        }

        //for set favouriteLawyer
        [HttpGet]
        [Route("FavouriteLawyer")]
        public ActionResult FavouriteLawyer(int userid, int lawyerid, bool status)
        {
            try
            {
                if (status)
                {
                    if (!(_context.FavouriteLawyers.Any(a => a.LawyerId == lawyerid && a.CustomerId == userid)))
                    {
                        var favourit = new FavouriteLawyer();
                        favourit.CustomerId = userid;
                        favourit.LawyerId = lawyerid;
                        _context.FavouriteLawyers.Add(favourit);
                        _context.SaveChanges();
                    }
                }
                else
                {
                    var favourit = _context.FavouriteLawyers.Where(a => a.LawyerId == lawyerid && a.CustomerId == userid).SingleOrDefault();

                    _context.FavouriteLawyers.Remove(favourit);
                    _context.SaveChanges();
                }

                return Ok("Success!!!");
            }
            catch (Exception ex)
            {

                return Ok("Invalid Data");
            }

        }

        //for get favouriteLawyer
        [HttpGet]
        [Route("GetFavouriteLawyers")]
        public ActionResult GetFavouriteLawyers(int userid)
        {
            try
            {
                var configs = _context.FavouriteLawyers.Where(a => a.CustomerId == userid).Select(x => new
                {
                    FavouriteLawyerId = x.FavouriteLawyerId,
                    LawyerId = x.LawyerId
                }).ToList();

                //var lawyerExist = JsonConvert.SerializeObject(configs);
                return Ok(configs);
            }
            catch (Exception e)
            {
                return Ok("Invalid Data");
            }
        }

        //screen # 10 for LaywerSide And also For 
        [HttpGet]
        [Route("GetService")]
        public ActionResult<List<Lawyer>> GetService()
        {
            try
            {
                var casecat = _context.CaseCategories.ToList().Select(x => new
                {
                    ServiceName = x.Name,
                    ServiceId = x.CaseCategoryId,
                    ServiceIcon = x.VectorIcon
                });

                if (casecat != null)
                    return Ok(casecat);
                var configs = new { Service = "Not Exist" };

                return Ok(JsonConvert.SerializeObject(configs));
            }
            catch (Exception e)
            {
                return Ok("Invalid Data");
            }
        }



        //for Lawyer side 
        //screen # 5
        [HttpGet]
        [Route("GetLawyerData")]
        public ActionResult GetLawyerData(int lawyerid)
        {
            try
            {
                //    var IntroStatus = false;
                //    var EducationStatus = false;
                //    var LawFieldStatus = false;
                //    var AboutStatus = false;
                //    var AddOfficeStatus = false;
                //    var PackageStatus = false;
                //    var DocumentStatus = false;

                var LawyerData = _context.Lawyers.Where(law => law.LawyerId == lawyerid).Select(x => new
                {
                    lawyerName = x.FirstName + " " + x.LastName,
                    LawyerPic = x.ProfilePic,
                    OnlineStatus = x.OnlineStatus,
                    IntroStatus = _context.LawyerLanguages.Any(a => a.LawyerId == lawyerid),
                    EducationStatus = _context.LawyerQualifications.Any(a => a.LawyerId == lawyerid),
                    LawFieldStatus = _context.LawyerExperiences.Any(a => a.LawyerId == lawyerid),
                    AboutStatus = _context.LawyerLicenses.Any(a => a.LawyerId == lawyerid),
                    AddOfficeStatus = _context.LawyerAddresses.Any(a => a.LawyerId == lawyerid),
                    PackageStatus = _context.LawyerTimings.Any(a => a.LawyerId == lawyerid),
                    DocumentStatus = false,

                    Notifications = "...",

                }).SingleOrDefault();
                if (LawyerData != null)
                    return Ok(LawyerData);
                var configs = new { Lawyer = "Not Exist" };

                return Ok(JsonConvert.SerializeObject(configs));
            }
            catch (Exception e)
            {
                return Ok("Invalid Data");
            }

        }

        //screen # 6
        [HttpGet]
        [Route("GetIntroLawyerData")]
        public ActionResult GetIntroLawyerData(int lawyerid)
        {
            try
            {
                var IntroLawyerData = _context.Lawyers.Where(law => law.LawyerId == lawyerid).Select(x => new
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                }).SingleOrDefault();
                if (IntroLawyerData != null)
                    return Ok(IntroLawyerData);
                var configs = new { Lawyer = "Not Exist" };

                return Ok(JsonConvert.SerializeObject(configs));
            }
            catch (Exception e)
            {
                return Ok("Invalid Data");
            }
        }

        //screen # 9
        //[HttpGet]
        //[Route("GetDegreeData")]
        //public HttpResponseMessage GetDegreeData()
        //{
        //    try
        //    {
        //        var response = new HttpResponseMessage(HttpStatusCode.OK);

        //        var DegreeData = _context.Degrees.ToList().Select(x => new
        //        {
        //            DegreeId = x.DegreeId,
        //            DegreetypeId = x.DegreeTypeId,
        //            DegreeName = x.Name,
        //            DegreeYear = x.EligibleAfter
        //        });

        //        var DegreeTypeData = _context.DegreeTypes.ToList().Select(x => new
        //        {

        //            DegreetypeId = x.DegreeTypeId,
        //            TypeName = x.TypeName,

        //        });
        //      //  var DegreeData = _context.Degrees.ToList();
        //       // var DegreeTypeData = _context.DegreeTypes.ToList();
        //        var Specialization = _context.Specializations.ToList();

        //        response.Content = new StringContent(JsonConvert.SerializeObject(new
        //        {
        //            DegreeData = DegreeData,
        //            DegreeTypeData = DegreeTypeData,
        //            Specializations = Specialization

        //        }));
        //        response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        return response;

        //    }
        //    catch(Exception ex)
        //    {
        //        return new HttpResponseMessage(HttpStatusCode.BadGateway);
        //    }
        //}

        ////screen # 10 
        //[HttpGet]
        //[Route("GetLicenseData")]
        //public HttpResponseMessage GetLicenseData()
        //{

        //    try
        //    {
        //        var LicenseData = new HttpResponseMessage(HttpStatusCode.OK);

        //        var LicenseCityData = _context.LicenseCities.ToList();
        //        var LicenseDistrictData = _context.LicenseDistricts.ToList();

        //        LicenseData.Content = new StringContent(JsonConvert.SerializeObject(new
        //        {
        //            LicenseCityData = LicenseCityData,
        //            LicenseDistrictData = LicenseDistrictData

        //        }));
        //        LicenseData.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        //        return LicenseData;
        //    }
        //    catch
        //    {
        //        return new HttpResponseMessage(HttpStatusCode.BadGateway);
        //    }
        //}

        //screen # 18 

        [HttpGet]
        [Route("GetLawyerClientsData")]
        public ActionResult GetLawyerClientsData(int lawyerid)
        {
            try
            {
                var LawyerData = _context.Lawyers.Where(law => law.LawyerId == lawyerid).Select(x => new
                {
                    LaywerName = x.FirstName + " " + x.LastName,
                    LawyerPic = x.ProfilePic,
                    LawyerOnlineStatus = x.OnlineStatus,
                    Notifications = "...",
                    Clients = _context.LawyerClients.Where(cl => cl.LawyerId == x.LawyerId).Select(cli => new
                    {
                        ClientId = cli.LawyerClientId,
                        ClientName = cli.ClientName,
                        //    //ClientPic = 
                        //    //ClientAge = 
                        //    //ClientGender =
                        //    //CLientPhoneNo =
                        AppoinmentDetail = "",

                    }).ToList()
                }).SingleOrDefault();
                if (LawyerData != null)
                    return Ok(LawyerData);
                var configs = new { Lawyer = "Not Exist" };

                return Ok(JsonConvert.SerializeObject(configs));
            }
            catch (Exception e)
            {
                return Ok("Invalid Data");
            }
        }

        //Lawyer Detail screen # 26 - 31 
        [HttpGet]
        [Route("GetLawyerDetail")]
        public ActionResult GetLawyerDetail(int lawyerid)
        {
            try
            {
                //_context.Database.SetCommandTimeout(0);
                var LawyerData = _context.Lawyers.Where(law => law.LawyerId == lawyerid).Select(x => new
                {

                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    LawyerPic = x.ProfilePic,
                    LawyerCity = x.City,
                    Rating = x.Rating,
                    Contact = x.Contact,
                    OnlineStatus = x.OnlineStatus,
                    DateOfBirth = x.DateOfBirth,
                    CNICNo = x.Cnic,
                    Gender = x.Gender,
                    City = x.City,
                    AboutUs = x.Biography,
                    CnicFrontPic = x.CnicFrontPic,
                    CnicBackPic = x.CnicBackPic,
                    RecentDegreePic = x.RecentDegreePic,
                    Email = _context.Users.Where(u => u.Id == x.AppUserId).SingleOrDefault().Email,
                    TotalExprince = x.TotalExperience,

                }).SingleOrDefault();
                var LawyerMoreData = new
                {

                    ServiceProvide = _context.LawyerCaseCategories.Where(sp => sp.LawyerId == lawyerid).Select(c => new
                    {
                        ServiceName = c.CaseCategory.Name,
                        ServiceId = c.CaseCategoryId,
                        LawyerCaseCategoryId = c.LawyerCaseCategoryId,
                    }).ToList(),
                    LawyerDegree = _context.LawyerQualifications.Where(de => de.LawyerId == lawyerid).Select(d => new
                    {
                        LawyerQualificationId = d.LawyerQualificationId,
                        DegreeId = d.DegreeId,
                        DegreeTypeId = d.DegreeTypeId,
                        DegreeType = d.Degree.DegreeTypes.TypeName,
                        CompletionYear = d.CompletionYear,
                        SpecializationId = d.SpecializationId,
                        SpecializationName = d.Specialization.SpecializationName,
                        DegreeName = d.Degree.Name
                    }).ToList(),
                    LawyerPackages = _context.LawyerTimings.Where(t => t.LawyerId == lawyerid).Select(p => new
                    {
                        LawyerPackageId = p.LawyerTimingId,
                        Day = p.Day,
                        StartTime = p.TimeFrom,
                        EndTime = p.TimeTo,
                        Location = p.Location,
                        PackageType = p.SlotType,
                        Fee = p.Charges,
                        OfficeAddressId = p.LawyerAddressId,
                        Check1 = p.Check,
                        Check2 = p.Check2
                    }).ToList(),

                    LawyerOffices = _context.LawyerAddresses.Where(a => a.LawyerId == lawyerid).Select(s => new
                    {
                        LawyerAddressId = s.LawyerAddressId,
                        Address = s.Address,
                        Xcoordinate = s.Xcoordinate,
                        Ycoordinate = s.Ycoordinate
                    }).ToList(),
                    License = _context.LawyerLicenses.Where(li => li.LawyerId == lawyerid).Select(a => new
                    {
                        LawyerLicenseId = a.LawyerLicenseId,
                        LicenseCityId = a.LicenseCityId,
                        LicenseCity = a.LicenseCity.CityName,
                        LawyerId = a.LawyerId,
                        LicenseDistrict = a.LicenseCity.LicenseDistrict.DistrictName,
                        DistrictBar = a.DistrictBar,
                        CityBar = a.CityBar,
                        Check = a.Check
                    }).ToList(),
                    Experience = _context.LawyerExperiences.Where(le => le.LawyerId == lawyerid).Select(e => new
                    {
                        LawyerExperienceId = e.LawyerExperienceId,
                        ExperienceField = e.CaseCategory.Name,
                        ExperienceId = e.CaseCategoryId,
                        ExperienceYear = e.ExperienceYears
                    }).ToList(),

                    ReownedClient = _context.LawyerClients.Where(a => a.LawyerId == lawyerid).Select(c => new
                    {
                        LawyerClientId = c.LawyerClientId,
                        ClientName = c.ClientName
                    }).ToList(),
                    Documents = _context.LawyerCertificatePics.Where(a => a.LawyerId == lawyerid).Select(d => new
                    {
                        LawyerCertificatePicId = d.LawyerCertificatePicId,
                        CertificatePic = d.CertificatePic,

                    }).ToList(),
                };
                var LawyerLanguage = _context.LawyerLanguages.Where(la => la.LawyerId == lawyerid).Select(s => new
                {
                    LawyerLanguageId = s.LawyerLanguageId,
                    LawyerId = s.LawyerId,
                    Language = s.Language,
                    LanguageNo = s.LanguageNo
                }).ToList();

                var configs1 = new
                {
                    LawyerData = LawyerData,
                    LawyerMoreData = LawyerMoreData,
                    LawywerLanguage = LawyerLanguage
                };

                if (configs1 != null)
                    return Ok(configs1);
                var configs = new { Lawyer = "Not Exist" };

                return Ok(JsonConvert.SerializeObject(configs));
            }
            catch (Exception e)
            {
                return Ok("Invalid Data");
            }
        }

        [HttpGet]
        [Route("Getcategies")]
        public ActionResult<List<CaseCategory>> Getcategies()
        {
            try
            {
                var casecat = _context.CaseCategories.ToList().Select(x => new
                {
                    CategoryName = x.Name,
                    ParentId = x.ParentId,
                    Children = x.Children,
                    VectorIcon = x.VectorIcon

                });
                if (casecat != null)
                    return Ok(casecat);
                var configs = new { Service = "Not Exist" };

                return Ok(JsonConvert.SerializeObject(configs));
            }
            catch (Exception e)
            {
                return Ok("Invalid Data");
            }
        }

        //for screen 12
        [HttpGet]
        [Route("GetOfficeLocation")]
        public ActionResult<List<LawyerAddress>> GetOfficeLocation(int lawyerid)
        {
            try
            {
                var OfficeLocation = _context.LawyerAddresses.Where(a => a.LawyerId == lawyerid).ToList().Select(x => new
                {
                    LawyerAddressId = x.LawyerAddressId,
                    Address = x.Address,
                    X_Coordinate = x.Xcoordinate,
                    Y_Coordinate = x.Ycoordinate

                });
                if (OfficeLocation != null)

                    return Ok(OfficeLocation);
                var configs = new { Address = "Not Exist" };

                return Ok(JsonConvert.SerializeObject(configs));
            }
            catch (Exception e)
            {
                return Ok("Invalid Data");
            }
        }

        //for screen # 47 in clint side
        [HttpGet]
        [Route("GetAppointments")]
        public ActionResult<List<Appointment>> GetAppointments(int userid, string? type)
        {
            try
            {
                if (type == "Lawyer")
                {
                    var Appointments = _context.Appointments.Where(a => a.LawyerId == userid).Select(x => new
                    {
                        AppoinmentId = x.AppoinmentId,
                        AppoinmentStatus = x.AppoinmentStatus,
                        AppoinmentType = x.AppoinmentType,
                        CaseCharges = x.CaseCharges,
                        ScheduleDate = x.ScheduleDate,
                        TimeFrom = x.TimeFrom,
                        AppointmentCode = x.AppointmentCode,
                        TimeTo = x.TimeTo,
                        AddressId = x.LawyerAddressId,
                        Address = x.LawyerAddress.Address,
                        ClientId = x.CustomerId,
                        FirstName = x.Customer.FirstName,
                        LastName = x.Customer.LastName,
                        ProfilePic = x.Customer.ProfilePic,
                        Age = CalculateAge(x.Lawyer.DateOfBirth),
                        Gender = x.Lawyer.Gender,
                        ServiceName = x.CaseCategory.Name,
                        ServiceId = x.CaseCategoryId,
                        LawyerCaseCategoryId = x.CaseCategoryId,


                    }).ToList();
                    if (Appointments != null)

                        return Ok(Appointments);

                }
                else
                {
                    var Appointments = _context.Appointments.Where(a => a.CustomerId == userid).Select(x => new
                    {
                        AppoinmentId = x.AppoinmentId,
                        AppoinmentStatus = x.AppoinmentStatus,
                        AppoinmentType = x.AppoinmentType,
                        CaseCharges = x.CaseCharges,
                        ScheduleDate = x.ScheduleDate,
                        TimeFrom = x.TimeFrom,
                        TimeTo = x.TimeTo,
                        AddressId = x.LawyerAddressId,
                        Address = x.LawyerAddress.Address,
                        LawyerId = x.LawyerId,
                        FirstName = x.Lawyer.FirstName,
                        LastName = x.Lawyer.LastName,
                        FirbaseToken = x.Lawyer.FirbaseToken,
                        ProfilePic = x.Lawyer.ProfilePic,
                        ServiceName = x.CaseCategory.Name,
                        ServiceId = x.CaseCategoryId

                    }).ToList();
                    if (Appointments != null)

                        return Ok(Appointments);
                }

                var configs = new { Address = "Not Exist" };

                return Ok(JsonConvert.SerializeObject(configs));
            }
            catch (Exception e)
            {
                return Ok("Invalid Data");
            }
        }

        [HttpGet]
        [Route("GetWallet")]
        public ActionResult<List<Wallet>> GetWallet(int clientid, string type)
        {
            try
            {
                if (type.ToLower() == "lawyer")
                {
                    var wallet = _context.Wallets.Where(a => a.UserId == clientid && a.WalletType == type).Select(x => new
                    {
                        WalletAmount = x.WalletAmount,
                        WalletId = x.WalletId,
                        TotalEarning = 0,
                        TodayEarning = 0,
                        TotalWithdraw = 0

                    }).SingleOrDefault();
                    if (wallet != null)

                        return Ok(wallet);
                    var config = 0;

                    return Ok(JsonConvert.SerializeObject(config));

                }
                var wallets = _context.Wallets.Where(a => a.UserId == clientid && a.WalletType == type).Select(x => new
                {
                    WalletAmount = x.WalletAmount,
                    WalletId = x.WalletId

                }).SingleOrDefault();
                if (wallets != null)

                    return Ok(wallets);
                var configs = 0;

                return Ok(JsonConvert.SerializeObject(configs));
            }
            catch (Exception e)
            {
                return Ok(0);
            }
        }

        [HttpGet]
        [Route("GetWalletHistory")]
        public ActionResult<List<WalletHistory>> GetWalletHistory(int walletid)
        {
            try
            {

                var walletHistory = _context.WalletHistories.Where(a => a.WalletId == walletid).Select(x => new
                {
                    Amount = x.WalletHistoryAmount,
                    Description = x.WalletHistoryDisc,
                    Date = x.Date,
                    Time = x.Date.ToLongTimeString()


                }).ToList();
                if (walletHistory != null)

                    return Ok(walletHistory);
                var configs = new { Address = "Not Exist" };

                return Ok(JsonConvert.SerializeObject(configs));
            }
            catch (Exception e)
            {
                return Ok("Invalid Data");
            }
        }

        //for Lawyer Online or Offline
        [HttpGet]
        [Route("LawyerOnline")]
        public ActionResult LawyerOnline(int lawyerid, bool status)
        {

            try
            {
                var lawyer = _context.Lawyers.Where(a => a.LawyerId == lawyerid).SingleOrDefault();
                if (status)
                    lawyer.OnlineStatus = "Online";
                else
                    lawyer.OnlineStatus = "Offline";
                _context.Lawyers.Update(lawyer);
                _context.SaveChanges();

                return Ok(lawyer.OnlineStatus);

            }
            catch (Exception ex)
            {

                return Ok("Invalid Data");
            }


        }

        [HttpGet]
        [Route("SeenNotification")]
        public ActionResult SeenNotification(int notificationId)
        {
            var notifi = _context.Notifications.Find(notificationId);
            notifi.NotificationSeen = true;
            _context.Notifications.Add(notifi);
            _context.SaveChanges();
            return Ok();

        }

        [HttpGet]
        [Route("AllNotification")]
        public ActionResult AllNotification(string type, int userid)
        {
            if (_context.Notifications.Any(a => a.Usertype == type && a.UserId == userid))
            {
                var notifi = _context.Notifications.Where(a => a.Usertype == type && a.UserId == userid).ToList();
                return Ok(notifi);
            }

            return Ok("Not Exist");
        }

        //For mobile hit Api
        [HttpGet]
        [Route("SendNotification")]
        public ActionResult SendNotification(string token, string Id, string act, string body, string title, string user)
        {
            if (act == "")
            {
                var appoint = _context.Appointments.Find(Convert.ToInt32(Id));
                appoint.AppoinmentStatus = "ReschaduleRequested";
                _context.Appointments.Add(appoint);
                _context.SaveChanges();
            }

            // var apponit = _context.Appointments.Find(Id);
            var notification = new
            {
                to = token,
                //to = "dx-Az4OGVD8:APA91bECIn3igUHumyK3FeLBq9ee0Z9W0yTTtIGSeJpI0EKwHe32mS7-LiOVfGjEMlkx6PFdPLZykG3qlyw4rFp_oiDxqjMXiLaJ3CRngITLIy9UjmbOpZEauVNV22Hvx8hLrImXctSY",
                //  to = "/topics/send",
                //registration_ids = _userToken,
                data = new
                {
                    title = title,
                    body = body,
                    data = Id,
                    act = act,

                }
            };
            SendNotification(notification, user);
            var not = new Notification();
            not.Date = DateTime.Now;
            not.NotificationSeen = false;
            not.Usertype = user;
            not.Status = true;
            not.NotificationTypeId = Convert.ToInt32(Id);
            not.NotificationType = title;
            not.NotificationMessage = act;
            if (user == "Client")
                not.UserId = _context.Appointments.Where(a => a.AppoinmentId == not.NotificationTypeId).SingleOrDefault().CustomerId;
            else
                not.UserId = _context.Appointments.Where(a => a.AppoinmentId == not.NotificationTypeId).SingleOrDefault().LawyerId;
            //not.NotificationSubject = "";
            _context.Notifications.Add(not);
            _context.SaveChanges();
            return Ok();
        }
        public void SendNotification(object data, string user)
        {
            //var serializer = new JavaScriptSerializer();
            //var json = serializer.Serialize(data);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            Byte[] byteArray = Encoding.UTF8.GetBytes(json);
            SendNotification(byteArray, user);
        }
        public HttpResponseMessage SendNotification(Byte[] byteArray, string user)
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK);
            try
            {
                string SERVER_API_KEY = "";
                var SENDER_ID = "";
                if (user.ToLower() == "client")
                {
                    SERVER_API_KEY = "AAAAp87HyOI:APA91bEMXV9DNqs0IHyzt2mmKZKr6yBK5mjHdbHZePOXN71fA4yJBsiNEXvH4Aheva_vNaBCrSJOJkdoLtCS7dGVxO-OEWvybo9owkLK42qUuRJ-GEbspeHhA_gXxmXHUGLAkIoVWSng";
                    SENDER_ID = "720728738018";
                }
                else
                {
                    SERVER_API_KEY = "AAAAV-bexCo:APA91bH78D5YZVh4inCT8RkYFm26uBxhKLpTJ-Wmp-z7Vn7Ft3w50p8hU8Aybv4ob3yfUdxWJAp43LhgFFGeq2SliporILJQaJYavVjO6jobyxIr9EAIJpFAHnCYumtjUcC-aUFcsxef";
                    SENDER_ID = "377535513642";
                }

                // Create Request
                WebRequest tRequest;
                tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");     // FCM link
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";
                tRequest.Headers.Add(string.Format($"Authorization: key ={SERVER_API_KEY}"));     //Server Api Key Header
                tRequest.Headers.Add(string.Format($"Sender: id ={SENDER_ID}"));     // Sender Id Header
                tRequest.ContentLength = byteArray.Length;
                Stream dataStream = tRequest.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
                WebResponse tResponse = tRequest.GetResponse();
                dataStream = tResponse.GetResponseStream();
                StreamReader tReader = new StreamReader(dataStream);
                String sResponseFromServer = tReader.ReadToEnd();
                System.Diagnostics.Debug.WriteLine(sResponseFromServer);
                tReader.Close();
                dataStream.Close();
                tResponse.Close();
                response.Content = new StringContent(JsonConvert.SerializeObject("Successfull"));

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
            catch (Exception ex)
            {
                // throw ex;
                response.Content = new StringContent(JsonConvert.SerializeObject("Successfull"));

                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return response;
            }
        }

        [HttpGet]
        [Route("GetPackage")]
        public ActionResult<List<Appointment>> GetPackage(int id)
        {
            try
            {

                var LawyerPackages = _context.LawyerTimings.Where(t => t.LawyerId == id).Select(p => new
                {
                    Day = p.Day,
                    StartTime = p.TimeFrom,
                    EndTime = p.TimeTo,
                    Location = p.Location,
                    PackageType = p.SlotType,
                    Fee = p.Charges,
                    OfficeAddressId = p.LawyerAddressId,
                    OfficeAddress = p.LawyerAddress.Address,
                    Check1 = p.Check,
                    Check2 = p.Check2
                }).ToList();


                return Ok(JsonConvert.SerializeObject(LawyerPackages));
            }
            catch (Exception e)
            {
                return Ok("Invalid Data");
            }
        }

        public ActionResult pushNotification(string token, string Id, string act, string body, string title, string user)
        {
            var appoint = _context.Appointments.Find(Convert.ToInt32(Id));
            appoint.AppoinmentStatus = "ReschaduleRequested";
            _context.Appointments.Add(appoint);
            _context.SaveChanges();

            var log = new Log();
            log.Appointment_Id = appoint.AppoinmentId;
            log.LogDate = DateTime.Now;
            log.Status = true;

            log.User_id = appoint.CustomerId;
            log.User_Type = "Client";
            log.Log_Decs = body + " at " + DateTime.Now.ToShortDateString();
            log.Log_Status = appoint.AppoinmentStatus;

            _context.Logs.Add(log);
            _context.SaveChanges();

            // var apponit = _context.Appointments.Find(Id);
            var notification = new
            {
                to = token,
                //to = "dx-Az4OGVD8:APA91bECIn3igUHumyK3FeLBq9ee0Z9W0yTTtIGSeJpI0EKwHe32mS7-LiOVfGjEMlkx6PFdPLZykG3qlyw4rFp_oiDxqjMXiLaJ3CRngITLIy9UjmbOpZEauVNV22Hvx8hLrImXctSY",
                //  to = "/topics/send",
                //registration_ids = _userToken,
                data = new
                {
                    title = title,
                    body = body,
                    data = Id,
                    act = act,

                }
            };
            SendNotification(notification, user);
            var not = new Notification();
            not.Date = DateTime.Now;
            not.NotificationSeen = false;
            not.Usertype = user;
            not.Status = true;
            not.NotificationTypeId = Convert.ToInt32(Id);
            not.NotificationType = title;
            not.NotificationMessage = body;

            not.UserId = _context.Appointments.Where(a => a.AppoinmentId == not.NotificationTypeId).SingleOrDefault().CustomerId;

            not.NotificationSubject = act;
            _context.Notifications.Add(not);
            _context.SaveChanges();
            return Ok();
        }

        [HttpGet]
        [Route("ClientRescheduleAppoint")]
        public ActionResult RescheduleAppoint(string token, string Id, string act, string body, string title, string user, string RescheduleDate, string TimeTo, string TimeFrom, float Charges)
        {
            var reappoint = new RescheduleAppoint();
            reappoint.AppointmentId = Convert.ToInt32(Id);
            reappoint.RescheduleDate = Convert.ToDateTime(RescheduleDate);
            reappoint.TimeFrom = TimeFrom;
            reappoint.TimeTo = TimeTo;
            reappoint.CaseCharges = Charges;
            _context.RescheduleAppoints.Add(reappoint);
            _context.SaveChanges();

            pushNotification(token, Id.ToString(), act, body, title, user);


            return Ok();

        }

        [HttpGet]
        [Route("Reschedule")]
        public ActionResult Reschedule(int Id)
        {
            var reappoint = _context.RescheduleAppoints.Find(Id);
            var appoint = _context.Appointments.Find(Id);
            appoint.AppoinmentStatus = "Confirmed";
            appoint.CaseCharges = reappoint.CaseCharges;
            appoint.Date = DateTime.Now;
            appoint.ScheduleDate = reappoint.RescheduleDate;
            appoint.TimeFrom = reappoint.TimeFrom;
            appoint.TimeTo = reappoint.TimeTo;

            _context.Appointments.Update(appoint);
            _context.SaveChanges();

            var log = new Log();
            log.Appointment_Id = appoint.AppoinmentId;
            log.LogDate = DateTime.Now;
            log.Status = true;

            log.User_id = appoint.LawyerId;
            log.User_Type = "Lawyer";
            log.Log_Decs = "The Lawyer Confirmed Reschedule Request at " + DateTime.Now.ToShortDateString();

            log.Log_Status = appoint.AppoinmentStatus;

            _context.Logs.Add(log);
            _context.SaveChanges();

            var not = new Notification();
            not.Date = DateTime.Now;
            not.NotificationSeen = false;
            not.Usertype = "Client";
            not.Status = true;
            not.NotificationTypeId = appoint.AppoinmentId;
            not.NotificationType = "RescheduleAppointment";
            not.NotificationMessage = log.Log_Decs;
            //not.NotificationSubject = "";
            not.UserId = log.User_id;
            _context.Notifications.Add(not);
            _context.SaveChanges();

            _context.RescheduleAppoints.Remove(reappoint);
            _context.SaveChanges();

            return Ok();

        }

        [HttpGet]
        [Route("TokenRefresh")]
        public ActionResult TokenRefresh(int userId, string userType, string token)
        {
            if (userType == "Client")
            {
                var client = _context.Customers.Where(a => a.CustomerId == userId).SingleOrDefault();
                client.FirbaseToken = token;
                _context.Customers.Add(client);

            }
            else
            {
                var lawyer = _context.Lawyers.Where(a => a.LawyerId == userId).SingleOrDefault();
                lawyer.FirbaseToken = token;
                _context.Lawyers.Add(lawyer);
            }
            _context.SaveChanges();
            return Ok();

        }
    }
}