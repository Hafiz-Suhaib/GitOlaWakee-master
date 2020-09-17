using Microsoft.EntityFrameworkCore;
using OlaWakeel.Data;
using OlaWakeel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Services.LawyerExperienceService
{
    public interface ILawyerExperienceService
    {
        Task addLawyerExperience(List<LawyerExperience> addLawyerExperiences, int id);
        Task<List<LawyerExperience>> GetLawyerExperience(int id);
        Task DeleteLawyerExperience(int id);
        //Task UpdateLawyerExperience(List<LawyerExperience> editLawyerExperiences);
    }
    public class LawyerExperienceService: ILawyerExperienceService
    {
        private readonly ApplicationDbContext _context;
        public LawyerExperienceService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task addLawyerExperience(List<LawyerExperience> addLawyerExperiences, int id)
        {
            foreach (var item in addLawyerExperiences)
            {
                item.LawyerId = id;
                await _context.LawyerExperiences.AddAsync(item);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLawyerExperience(int id)
        {
            var deleteExperience = await _context.LawyerExperiences.FindAsync(id);
            _context.Remove(deleteExperience);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LawyerExperience>> GetLawyerExperience(int id)
        {
            var lawyerExperience = await _context.LawyerExperiences.Where(x => x.LawyerId == id).ToListAsync();
            return lawyerExperience;
        }
    }
}
