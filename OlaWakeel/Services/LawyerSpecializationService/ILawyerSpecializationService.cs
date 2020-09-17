using Microsoft.EntityFrameworkCore;
using OlaWakeel.Data;
using OlaWakeel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Services.LawyerSpecializationService
{
    public interface ILawyerSpecializationService
    {
        Task addLawyerSpecialization(List<LawyerSpecialization> addLawyerTimings, int id);
        Task<List<LawyerSpecialization>> GetLawyerSpecialization(int id);
        Task DeleteLawyerSpecialization(int id);
        //Task UpdateLawyerSpecialization(List<LawyerSpecialization> editLawyerSpecialization);
    }
    public class LawyerSpecializationService : ILawyerSpecializationService
    {
        private readonly ApplicationDbContext _context;
        public LawyerSpecializationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task addLawyerSpecialization(List<LawyerSpecialization> addLawyerSpecializations, int id)
        {
            foreach (var item in addLawyerSpecializations)
            {
                item.LawyerId = id;
                await _context.LawyerSpecializations.AddAsync(item);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLawyerSpecialization(int id)
        {
            var deleteSpecialization = await _context.LawyerSpecializations.FindAsync(id);
            _context.Remove(deleteSpecialization);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LawyerSpecialization>> GetLawyerSpecialization(int id)
        {
            var lawyerSpecialization = await _context.LawyerSpecializations.Where(x => x.LawyerId == id).ToListAsync();
            return lawyerSpecialization;
        }

        //public Task UpdateLawyerSpecialization(List<LawyerSpecialization> editLawyerSpecialization)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
