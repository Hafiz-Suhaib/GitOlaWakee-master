using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using OlaWakeel.Data;
using OlaWakeel.Dto;
using OlaWakeel.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Services.LawyerService
{
    public interface ILawyerService
    {
        public Task AddLawyer(Lawyer addLawyer, List<LawyerAddress> lawyerAddresses, List<LawyerTiming> lawyerTimings, List<string> AddressesTemp);
        public Task AddLawyer(Lawyer addLawyer);
        public Task AddLawyerOffice(List<LawyerAddress> addLawyerAddresses);
        public Task<List<Lawyer>> GetAllLawyers();
        public Task<List<Lawyer>> GetOnlineLawyers();
        public Task<List<Lawyer>> GetOfflineLawyers();
        public Task<Lawyer> LawyerProfile(int id);
       public Task<Lawyer> LawyerProfile1(int id);
        public Task<Lawyer> LawyerProfile2(int id);
        public Task<Lawyer> GetAccountProfile(int id);
        // editing
        public Task EditLawyerQualification(LawyerQualification editLawyerQualification);
        public Task EditLawyerAccount(LawyerDto lawyerDto, IFormFile file);
        public Task AddLawyerQualification(LawyerQualification addLawyerQualification);
        public Task DeleteLawyerQualification(int id);
        public Task DeleteLawyerLicense(int id);
        public Task DeleteLawyerClient(int id);
        public Task DeleteLawyerAddress(int id);
        public Task DeleteLawyer(int id);
        public Task DeleteLawyerExperience(int id);
        public Task DeleteLawyerTiming(int id);
        public Task EditLawyerSpcecialization(LawyerSpecialization editLawyerSpecialization);
        public Task AddLawyerSpcecialization(LawyerSpecialization addLawyerSpecialization);
        public Task<int> DeleteLawyerSpecialization(int id);

        public Task EditLawyerExperience(LawyerExperience editLawyerExperience);
        public Task AddLawyerExperience(LawyerExperience addLawyerExperience);


        public Task EditLawyerTiming(LawyerTiming editLawyerTiming);
        public Task AddLawyerTiming(LawyerTiming addLawyerTiming);


        public Task<List<LawyerCaseCategory>> GetCaseCatIds(int id);
        public Task EditLawyerCasecategory(List<LawyerCaseCategory> editLawyerCaseCategory);
        public Task<int> TotalLawyersCount();
        public Task<int> OnlineLawyersCount();
        public Task<int> OfflineLawyersCount();
        public Task ChangeLawyerStatus(int id);
        public Task<Lawyer> GetLawyerId(int id);
        public Task LawyerEdit(Lawyer editLawyer,
            List<LawyerQualification> lawyerQualifications,
            List<LawyerLanguage> lawyerLanguages,
            List<LawyerExperience> lawyerExperiences,
            List<LawyerLicense> lawyerLicenses,
            List<LawyerClient> lawyerClients,
            List<LawyerAddress> lawyerAddresses,
            List<LawyerTiming> lawyerTimings,
            List<LawyerCaseCategory> lawyerCaseCategories,
            string ImagePath
            );
    }
    public class LawyerService : ILawyerService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public LawyerService(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;
            _context = context;
        }
        public async Task AddLawyer(Lawyer addLawyer, List<LawyerAddress> lawyerAddresses, List<LawyerTiming> lawyerTimings, List<string> AddressesTemp)
        {

            await _context.Lawyers.AddAsync(addLawyer);
            await _context.SaveChangesAsync();
            var lawyerId = addLawyer.LawyerId;

            foreach (var adr in lawyerAddresses)
            {
                adr.LawyerId = lawyerId;
            }
            _context.LawyerAddresses.AddRange(lawyerAddresses);
            await _context.SaveChangesAsync();

            var i = 0;
            foreach (var timing in lawyerTimings)
            {
                if (AddressesTemp[i] != "")
                {
                    try
                    {
                        var lawyeradd = lawyerAddresses.Where(a => a.Address.Trim() == AddressesTemp[i].Trim()).SingleOrDefault();
                        timing.LawyerAddressId = lawyeradd.LawyerAddressId;
                        timing.LawyerId = lawyerId;
                    }
                    catch (Exception ex)
                    {

                    }

                }
                else { timing.LawyerId = lawyerId; }

                //if (lawyerAddresses[i].Address == AddressesTemp[i])
                //{
                //    timing.LawyerAddressId = lawyerAddresses[i].LawyerAddressId;
                //    timing.LawyerId = lawyerId;
                //}
                i++;
            }
            _context.LawyerTimings.AddRange(lawyerTimings);
            await _context.SaveChangesAsync();
            //for (var i = 0; i < AddressesTemp.Count; i++)
            //{
            //    if (lawyerTimings[i].SlotType == "InPerson")
            //    {
            //        if (lawyerAddresses[i].Address == AddressesTemp[i])
            //        {
            //            lawyerTimings[i].LawyerAddressId = lawyerAddresses[i].LawyerAddressId;
            //            lawyerTimings[i].LawyerId = lawyerId;
            //        }
            //    }
            //    else if (lawyerTimings[i].SlotType == "Virtual") 
            //    {
            //        lawyerTimings[i].LawyerAddressId = 0;
            //        lawyerTimings[i].LawyerId = lawyerId;
            //    }
            //}
            _context.LawyerTimings.AddRange(lawyerTimings);
            await _context.SaveChangesAsync();

        }
        public async Task LawyerEdit(Lawyer editLawyer,
            List<LawyerQualification> lawyerQualifications,
            List<LawyerLanguage> lawyerLanguages,
            List<LawyerExperience> lawyerExperiences,
            List<LawyerLicense> lawyerLicenses,
            List<LawyerClient> lawyerClients,
            List<LawyerAddress> lawyerAddresses,
            List<LawyerTiming> lawyerTimings, List<LawyerCaseCategory> lawyerCaseCategories, string ImagePath)
        {
            var lawyerId = lawyerLanguages[0].LawyerId;
            var language = await _context.LawyerLanguages.Where(x => x.LawyerId == lawyerId).ToListAsync();

            _context.LawyerLanguages.RemoveRange(language);
            await _context.SaveChangesAsync();

            await _context.LawyerLanguages.AddRangeAsync(lawyerLanguages);
            await _context.SaveChangesAsync();

            if (ImagePath != null)
            {
                editLawyer.ProfilePic = ImagePath;
            }
            _context.Lawyers.Update(editLawyer);
            await _context.SaveChangesAsync();
            if (lawyerQualifications.Count > 0)
            {
                for (var i = 0; i < lawyerQualifications.Count; i++)
                {
                    if (lawyerQualifications[i].LawyerQualificationId != 0)
                    {
                        _context.LawyerQualifications.Update(lawyerQualifications[i]);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        await _context.LawyerQualifications.AddAsync(lawyerQualifications[i]);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            if (lawyerExperiences.Count > 0)
            {
                for (var i = 0; i < lawyerExperiences.Count; i++)
                {
                    if (lawyerExperiences[i].LawyerExperienceId != 0)
                    {
                        _context.LawyerExperiences.Update(lawyerExperiences[i]);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        await _context.LawyerExperiences.AddAsync(lawyerExperiences[i]);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            if (lawyerLicenses.Count > 0)
            {
                for (var i = 0; i < lawyerLicenses.Count; i++)
                {
                    if (lawyerLicenses[i].LawyerLicenseId != 0)
                    {
                        _context.LawyerLicenses.Update(lawyerLicenses[i]);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        await _context.LawyerLicenses.AddAsync(lawyerLicenses[i]);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            if (lawyerClients.Count > 0)
            {
                for (var i = 0; i < lawyerClients.Count; i++)
                {
                    if (lawyerClients[i].LawyerClientId != 0)
                    {
                        _context.LawyerClients.Update(lawyerClients[i]);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        await _context.LawyerClients.AddAsync(lawyerClients[i]);
                        await _context.SaveChangesAsync();
                    }
                }
            }

            if (lawyerAddresses.Count > 0)
            {
                for (var i = 0; i < lawyerAddresses.Count; i++)
                {
                    if (lawyerAddresses[i].LawyerAddressId == 0)
                    {
                        await _context.LawyerAddresses.AddAsync(lawyerAddresses[i]);
                        await _context.SaveChangesAsync();
                    }
                    //else
                    //{
                    //    await _context.LawyerAddresses.AddAsync(lawyerAddresses[i]);
                    //    await _context.SaveChangesAsync();
                    //}
                }
            }

            if (lawyerTimings.Count > 0)
            {
                for (var i = 0; i < lawyerTimings.Count; i++)
                {
                    if (lawyerTimings[i].LawyerTimingId != 0)
                    {
                        _context.LawyerTimings.Update(lawyerTimings[i]);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        await _context.LawyerTimings.AddAsync(lawyerTimings[i]);
                        await _context.SaveChangesAsync();
                    }
                }
            }


            //_context.LawyerQualifications.UpdateRange(lawyerQualifications);
            //await _context.SaveChangesAsync();

            //_context.LawyerExperiences.UpdateRange(lawyerExperiences);
            //await _context.SaveChangesAsync();

            //_context.LawyerLicenses.UpdateRange(lawyerLicenses);
            //await _context.SaveChangesAsync();

            //_context.LawyerClients.UpdateRange(lawyerClients);
            //await _context.SaveChangesAsync();

            //_context.LawyerAddresses.UpdateRange(lawyerAddresses);
            //await _context.SaveChangesAsync();

            //_context.LawyerTimings.UpdateRange(lawyerTimings);
            //await _context.SaveChangesAsync();
            await EditLawyerCasecategory(lawyerCaseCategories);

        }
        public async Task<List<Lawyer>> GetAllLawyers()
        {
            var list = await _context.Lawyers.Include(x => x.AppUser).ToListAsync();
            return list;
        }
        public async Task<List<Lawyer>> GetOnlineLawyers()
        {
            var list = await _context.Lawyers.Where(x => x.OnlineStatus == "Online").Include(x => x.AppUser).ToListAsync();
            return list;
        }

        public async Task<List<Lawyer>> GetOfflineLawyers()
        {
            var list = await _context.Lawyers.Where(x => x.OnlineStatus == "Offline").Include(x => x.AppUser).ToListAsync();
            return list;
        }

        public async Task<Lawyer> LawyerProfile(int id)
        {
            var lawerProfile = await _context.Lawyers.Include(x => x.AppUser)
                .Include(x => x.LawyerQualifications)
                .ThenInclude(x => x.Specialization)
                .Include(x => x.LawyerQualifications)
                .ThenInclude(x => x.Degree)
                .ThenInclude(x => x.DegreeTypes)

                 .Include(x => x.LawyerExperiences)
                 .ThenInclude(x => x.CaseCategory)
                 .Include(x => x.LawerTimings)
                 .Include(x => x.LawyerClients)
                 .Include(x => x.lawyerLanguages)
                 .Include(x => x.LawyerLicenses)
                 .ThenInclude(x => x.LicenseCity)
                 .Include(x => x.LawyerCaseCategories)
                 .ThenInclude(x => x.CaseCategory)
                 .Include(x=>x.LawyerAddresses)
                 
                 .SingleOrDefaultAsync(x => x.AppUserId == id);
            return lawerProfile;
        }
        public async Task<Lawyer> LawyerProfile1(int id)
        {
            var lawyerProfile = await _context.Lawyers.Include(x => x.AppUser)
                .Include(x => x.LawyerLicenses)
                .Include(x => x.LawyerClients)
                .Include(x => x.lawyerLanguages)
                .Include(x => x.LawyerQualifications)
                .Include(x => x.LawyerCaseCategories)
                .Include(x => x.LawerTimings)
                .Include(x=>x.LawyerAddresses)
                .Include(x=>x.LawyerExperiences)
                 .FirstOrDefaultAsync(x=>x.LawyerId == id);


            //.Include(x => x.LawyerQualifications)
            //    .ThenInclude(x => x.Specialization)
            //    .Include(x => x.LawyerQualifications)
            //    .ThenInclude(x => x.Degree)
            //    .ThenInclude(x => x.DegreeTypes)
            //     .Include(x => x.LawyerExperiences)
            //     .ThenInclude(x => x.CaseCategory)
            //     .Include(x => x.LawerTimings)
            //     .Include(x => x.LawyerClients)
            //     .Include(x => x.lawyerLanguages)
            //     .Include(x => x.LawyerLicenses)
            //     .ThenInclude(x => x.LicenseCity)
            //     .Include(x => x.LawyerCaseCategories)
            //     .ThenInclude(x => x.CaseCategory)
            //     .Include(x => x.LawyerAddresses)
            return lawyerProfile;
        }

        public async Task<Lawyer> LawyerProfile2(int id)
        {
            var lawerProfile = await _context.Lawyers.Include(x => x.AppUser)
                .Include(x => x.LawyerQualifications)
                .ThenInclude(x => x.Specialization)
                .Include(x => x.LawyerQualifications)
                .ThenInclude(x => x.Degree)
                .ThenInclude(x => x.DegreeTypes)

                 .Include(x => x.LawyerExperiences)
                 .ThenInclude(x => x.CaseCategory)
                 .Include(x => x.LawerTimings)
                 .Include(x => x.LawyerClients)
                 .Include(x => x.lawyerLanguages)
                 .Include(x => x.LawyerLicenses)
                 .ThenInclude(x => x.LicenseCity)
                 .Include(x => x.LawyerCaseCategories)
                 .ThenInclude(x => x.CaseCategory)
                 .Include(x => x.LawyerAddresses)

                 .Where(x => x.LawyerId == id).SingleOrDefaultAsync();
            return lawerProfile;
        }
        //GetAccountProfile for edit
        public async Task<Lawyer> GetAccountProfile(int id)
        {
            var getAccountProfile = await _context.Lawyers.Include(x => x.AppUser)
                .Include(x => x.LawyerQualifications)
                .Include(x => x.LawyerLicenses)
                .Include(x => x.LawyerExperiences)
                .Include(x => x.LawyerClients)
                .Include(x => x.LawerTimings)
                .Include(x => x.lawyerLanguages)
                .Include(x => x.LawyerAddresses)
                .Include(x => x.LawyerCaseCategories)
                .SingleOrDefaultAsync(x => x.LawyerId == id);
            getAccountProfile.LawyerQualifications.ForEach(x => x.Lawyer = null);
            getAccountProfile.LawyerExperiences.ForEach(x => x.Lawyer = null);
            getAccountProfile.LawyerLicenses.ForEach(x => x.Lawyer = null);
            getAccountProfile.LawyerClients.ForEach(x => x.Lawyer = null);
            getAccountProfile.LawerTimings.ForEach(x => x.Lawyer = null);
            getAccountProfile.lawyerLanguages.ForEach(x => x.Lawyer = null);
            getAccountProfile.LawyerAddresses.ForEach(x => x.Lawyer = null);
            getAccountProfile.LawyerCaseCategories.ForEach(x => x.Lawyer = null);
            return getAccountProfile;
        }

        public async Task EditLawyerAccount(LawyerDto lawyerDto, IFormFile file)
        {
            string uniqueFileName = UploadedFile(file);
            var lawyerAccount = await _context.Lawyers.FindAsync(lawyerDto.LawyerId);
            lawyerAccount.FirstName = lawyerDto.FirstName;
            lawyerAccount.LastName = lawyerDto.LastName;
            lawyerAccount.City = lawyerDto.City;
            lawyerAccount.Cnic = lawyerDto.Cnic;
            lawyerAccount.Address = lawyerDto.Address;
            lawyerAccount.Contact = lawyerDto.Contact;
            lawyerAccount.TotalExperience = lawyerDto.TotalExperience;
            lawyerAccount.VirtualChargesPkr = lawyerDto.VirtualChargesPkr;
            lawyerAccount.VirtualChargesUs = lawyerDto.VirtualChargesUs;
            lawyerAccount.Gender = lawyerDto.Gender;
            if (uniqueFileName != null)
            {
                lawyerAccount.ProfilePic = uniqueFileName;
            }
            _context.Lawyers.Update(lawyerAccount);
            await _context.SaveChangesAsync();
        }


        //picture upload method
        private string UploadedFile(IFormFile file)
        {
            string uniqueFileName = null;

            if (file != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Uploads");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public async Task EditLawyerQualification(LawyerQualification editLawyerQualification)
        {
            var editLawyerQuali = await _context.LawyerQualifications.FindAsync(editLawyerQualification.LawyerQualificationId);
            editLawyerQuali.DegreeId = editLawyerQualification.DegreeId;
            //editLawyerQuali.InstituteName = editLawyerQualification.InstituteName;
            // editLawyerQuali.Location = editLawyerQualification.Location;
            editLawyerQuali.CompletionYear = editLawyerQualification.CompletionYear;
            _context.LawyerQualifications.Update(editLawyerQuali);
            await _context.SaveChangesAsync();
        }

        public async Task AddLawyerQualification(LawyerQualification addLawyerQualification)
        {
            await _context.LawyerQualifications.AddAsync(addLawyerQualification);
            await _context.SaveChangesAsync();
        }

        public async Task EditLawyerSpcecialization(LawyerSpecialization editLawyerSpecialization)
        {
            var editLawyerSpec = await _context.LawyerSpecializations.FindAsync(editLawyerSpecialization.LawyerSpecializationId);
            editLawyerSpec.SpecializationId = editLawyerSpecialization.SpecializationId;
            editLawyerSpec.EndYear = editLawyerSpecialization.EndYear;
            _context.Update(editLawyerSpec);
            await _context.SaveChangesAsync();
        }


        public async Task AddLawyerSpcecialization(LawyerSpecialization addLawyerSpecialization)
        {
            await _context.LawyerSpecializations.AddAsync(addLawyerSpecialization);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLawyerQualification(int id)
        {
            var del = await _context.LawyerQualifications.FindAsync(id);
            //  int lawyerId = del.LawyerId;
            _context.Remove(del);
            await _context.SaveChangesAsync();

        }

        public async Task<int> DeleteLawyerSpecialization(int id)
        {
            var del = await _context.LawyerSpecializations.FindAsync(id);
            int lawyerId = del.LawyerId;
            _context.Remove(del);
            await _context.SaveChangesAsync();
            return lawyerId;
        }

        public async Task EditLawyerExperience(LawyerExperience editLawyerExperience)
        {
            var edit = await _context.LawyerExperiences.FindAsync(editLawyerExperience.LawyerExperienceId);
            edit.CaseCategoryId = editLawyerExperience.CaseCategoryId;
            edit.ExperienceYears = editLawyerExperience.ExperienceYears;
            _context.LawyerExperiences.Update(edit);
            await _context.SaveChangesAsync();
        }

        public async Task AddLawyerExperience(LawyerExperience addLawyerExperience)
        {
            await _context.LawyerExperiences.AddAsync(addLawyerExperience);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteLawyerExperience(int id)
        {
            var del = await _context.LawyerExperiences.FindAsync(id);
            //int lawyerId = del.LawyerId;
            _context.Remove(del);
            await _context.SaveChangesAsync();
            // return lawyerId;
        }


        public async Task EditLawyerTiming(LawyerTiming editLawyerTiming)
        {
            var lawyerTime = await _context.LawyerTimings.FindAsync(editLawyerTiming.LawyerTimingId);
            lawyerTime.Day = editLawyerTiming.Day;
            lawyerTime.Location = editLawyerTiming.Location;
            lawyerTime.Charges = editLawyerTiming.Charges;
            lawyerTime.TimeFrom = editLawyerTiming.TimeFrom;
            lawyerTime.TimeTo = editLawyerTiming.TimeTo;
            _context.LawyerTimings.Update(lawyerTime);
            await _context.SaveChangesAsync();
        }

        public async Task AddLawyerTiming(LawyerTiming addLawyerTiming)
        {
            await _context.LawyerTimings.AddAsync(addLawyerTiming);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteLawyerTiming(int id)
        {
            var del = await _context.LawyerTimings.FindAsync(id);
            //  int lawyerId = del.LawyerId;
            _context.Remove(del);
            await _context.SaveChangesAsync();
            // return lawyerId;
        }

        public async Task<List<LawyerCaseCategory>> GetCaseCatIds(int id)
        {
            var lawyerCaseIds = await _context.LawyerCaseCategories.Where(x => x.LawyerId == id).Select(x => new LawyerCaseCategory { CaseCategoryId = x.CaseCategoryId }).ToListAsync();
            return lawyerCaseIds;
        }

        public async Task EditLawyerCasecategory(List<LawyerCaseCategory> editLawyerCaseCategory)
        {
            List<LawyerCaseCategory> editObj = new List<LawyerCaseCategory>();
            List<LawyerCaseCategory> AddObj = new List<LawyerCaseCategory>();
            List<LawyerCaseCategory> delObj = new List<LawyerCaseCategory>();

            var lawyerId = editLawyerCaseCategory[0].LawyerId;
            editObj = await _context.LawyerCaseCategories.Where(x => x.LawyerId == lawyerId).ToListAsync();

            if (editLawyerCaseCategory.Count > editObj.Count)
            {
                for (var i = 0; i < editLawyerCaseCategory.Count; i++)
                {
                    if (i < editObj.Count)
                    {
                        editObj[i].CaseCategoryId = editLawyerCaseCategory[i].CaseCategoryId;
                    }
                    if (i >= editObj.Count)
                    {
                        AddObj.Add(editLawyerCaseCategory[i]);
                    }
                }
            }
            else if (editLawyerCaseCategory.Count < editObj.Count)
            {
                for (var i = 0; i < editObj.Count; i++)
                {
                    if (i < editLawyerCaseCategory.Count)
                    {
                        editObj[i].CaseCategoryId = editLawyerCaseCategory[i].CaseCategoryId;
                    }

                    else
                    {
                        delObj.Add(editObj[i]);
                    }
                    if (i >= editObj.Count)
                    {
                        AddObj.Add(editLawyerCaseCategory[i]);
                    }

                }
            }
            _context.LawyerCaseCategories.UpdateRange(editObj);
            _context.LawyerCaseCategories.AddRange(AddObj);
            _context.LawyerCaseCategories.RemoveRange(delObj);
            await _context.SaveChangesAsync();
        }

        public async Task<int> TotalLawyersCount()
        {
            var totalLawyers = await _context.Lawyers.CountAsync();
            return totalLawyers;
        }

        public async Task<int> OnlineLawyersCount()
        {
            var totalOnlineLawyers = await _context.Lawyers.Where(x => x.OnlineStatus == "Online").CountAsync();
            return totalOnlineLawyers;
        }

        public async Task<int> OfflineLawyersCount()
        {
            var totalOfflineLawyers = await _context.Lawyers.Where(x => x.OnlineStatus == "Offline").CountAsync();
            return totalOfflineLawyers;
        }

        public async Task ChangeLawyerStatus(int id)
        {
            var lawyer = await _context.Lawyers.FindAsync(id);
            if (lawyer.OnlineStatus == "Online")
            {
                lawyer.OnlineStatus = "Offline";
                _context.Lawyers.Update(lawyer);
                await _context.SaveChangesAsync();
            }
            else
            {
                lawyer.OnlineStatus = "Online";
                _context.Lawyers.Update(lawyer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Lawyer> GetLawyerId(int id)
        {
            var lawyer = await _context.Lawyers.SingleOrDefaultAsync(x => x.AppUserId == id);
            return lawyer;
        }

        public async Task DeleteLawyerLicense(int id)
        {
            var del = await _context.LawyerLicenses.FindAsync(id);
            //  int lawyerId = del.LawyerId;
            _context.Remove(del);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLawyerClient(int id)
        {
            var del = await _context.LawyerClients.FindAsync(id);
            //  int lawyerId = del.LawyerId;
            _context.Remove(del);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLawyerAddress(int id)
        {
            var del = await _context.LawyerAddresses.FindAsync(id);
            //  int lawyerId = del.LawyerId;
            _context.Remove(del);
            await _context.SaveChangesAsync();
        }

        public Task DeleteLawyer(int id)
        {
            throw new NotImplementedException();
        }

        public async Task AddLawyer(Lawyer addLawyer)
        {
            await _context.Lawyers.AddAsync(addLawyer);
            await _context.SaveChangesAsync();
        }

        public async Task AddLawyerOffice(List<LawyerAddress> addLawyerAddresses)
        {
            for (var i = 0; i < addLawyerAddresses.Count; i++)
            {
                if (addLawyerAddresses[i].LawyerAddressId == 0)
                {
                    await _context.LawyerAddresses.AddAsync(addLawyerAddresses[i]);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
