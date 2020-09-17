using Microsoft.EntityFrameworkCore;
using OlaWakeel.Data;
using OlaWakeel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Services.LawyerTimingService
{
    //definations
    public interface ILawyerTimingService
    {
        Task addTiming(List<LawyerTiming> addLawyerTimings, int id);
        Task<List<LawyerTiming>> GetLawyerTimings(int id);
        Task DeleteLawyerTiming(int id);
        //Task UpdateTiming(LawyerTiming editLawyerTiming);

    }
    //implementations
    public class LawyerTimingService : ILawyerTimingService
    {
        private readonly ApplicationDbContext _context;
        public LawyerTimingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task addTiming(List<LawyerTiming> addLawyerTimings, int id)
        {
            foreach (var item in addLawyerTimings)
            {
                item.LawyerId = id;
                await _context.LawyerTimings.AddAsync(item);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteLawyerTiming(int id)
        {
            var deleteTiming = await _context.LawyerTimings.FindAsync(id);
            _context.Remove(deleteTiming);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LawyerTiming>> GetLawyerTimings(int id)
        {
           var lawyerTiming= await _context.LawyerTimings.Where(x => x.LawyerId == id).ToListAsync();
            return lawyerTiming;
        }
    }
}
