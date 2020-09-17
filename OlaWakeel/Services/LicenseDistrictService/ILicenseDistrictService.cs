using Microsoft.EntityFrameworkCore;
using OlaWakeel.Data;
using OlaWakeel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Services.LicenseDistrictService
{
    public interface ILicenseDistrictService
    {
        Task AddLicenseDistrict(LicenseDistrict addLicenseDistrict);
        Task<List<LicenseDistrict>> GetAllLicenseDistrict();
        Task EditLicenseDistrict(LicenseDistrict editLicenseDistrict);
        Task Delete(int id);
        Task<LicenseDistrict> GetById(int id);
    }
    public class LicenseDistrictService : ILicenseDistrictService
    {
        private readonly ApplicationDbContext _context;
        public LicenseDistrictService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddLicenseDistrict(LicenseDistrict addLicenseDistrict)
        {
           await _context.LicenseDistricts.AddAsync(addLicenseDistrict);
           await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var del = await _context.LicenseDistricts.FindAsync(id);
            _context.LicenseDistricts.Remove(del);
            await _context.SaveChangesAsync();
        }

        public async Task EditLicenseDistrict(LicenseDistrict editLicenseDistrict)
        {
            var license = await _context.LicenseDistricts.FindAsync(editLicenseDistrict.LicenseDistrictId);
            license.DistrictName = editLicenseDistrict.DistrictName;
            _context.LicenseDistricts.Update(license);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LicenseDistrict>> GetAllLicenseDistrict()
        {
            var allLicenseDistrict = await _context.LicenseDistricts.ToListAsync();
            return allLicenseDistrict;
        }

        public async Task<LicenseDistrict> GetById(int id)
        {
            var byId = await _context.LicenseDistricts.FindAsync(id);
            return byId;
        }
    }
}
