using Microsoft.EntityFrameworkCore;
using OlaWakeel.Data;
using OlaWakeel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Services.LicenseCityService
{
    public interface ILicenseCityService
    {
        Task AddLicenseCity(LicenseCity addLicenseCity);
        Task<List<LicenseCity>> GetAllLicenseCity();
        Task EditLicenseCity(LicenseCity editLicenseCity);
        Task Delete(int id);
        Task<LicenseCity> GetById(int id);
    }
    public class LicenseCityService : ILicenseCityService
    {
        private readonly ApplicationDbContext _context;
        public LicenseCityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddLicenseCity(LicenseCity addLicenseCity)
        {
            await _context.LicenseCities.AddAsync(addLicenseCity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var del = await _context.LicenseCities.FindAsync(id);
            _context.LicenseCities.Remove(del);
            await _context.SaveChangesAsync();
        }

        public async Task EditLicenseCity(LicenseCity editLicenseCity)
        {
            var licenseCity = await _context.LicenseCities.FindAsync(editLicenseCity.LicenseCityId);
            licenseCity.CityName = editLicenseCity.CityName;
            licenseCity.LicenseDistrictId = editLicenseCity.LicenseDistrictId;
            licenseCity.LicenseExist = editLicenseCity.LicenseExist;
            _context.LicenseCities.Update(licenseCity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LicenseCity>> GetAllLicenseCity()
        {
            var allLicenseCity = await _context.LicenseCities.Include(x => x.LicenseDistrict).ToListAsync();
            return allLicenseCity;
        }

        public async Task<LicenseCity> GetById(int id)
        {
            var byId = await _context.LicenseCities.FindAsync(id);
            return byId;
        }
    }
}
