using OlaWakeel.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OlaWakeel.Models;
using Microsoft.EntityFrameworkCore;

namespace OlaWakeel.Services.LawyerQaulificationService
{
    public interface ILawyerQaulificationService
    {
        Task addLawyerQaulification(List<LawyerQualification> addLawyerQaulifications, int id);
        Task<List<LawyerQualification>> GetLawyerQaulification(int id);
        Task DeleteLawyerQaulification(int id);
        //Task UpdateLawyerQualification(List<LawyerSpecialization> editLawyerSpecialization);
    }

    public class LawyerQaulificationService :ILawyerQaulificationService
    {
        private readonly ApplicationDbContext _context;
        public LawyerQaulificationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task addLawyerQaulification(List<LawyerQualification> addLawyerQaulifications, int id)
        {
            foreach (var item in addLawyerQaulifications)
            {
                item.LawyerId = id;
                await _context.LawyerQualifications.AddAsync(item);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLawyerQaulification(int id)
        {
            var deleteQaulification = await _context.LawyerQualifications.FindAsync(id);
            _context.Remove(deleteQaulification);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LawyerQualification>> GetLawyerQaulification(int id)
        {
            var lawyerQaulification = await _context.LawyerQualifications.Where(x => x.LawyerId == id).ToListAsync();
            return lawyerQaulification;
        }
    }
}
