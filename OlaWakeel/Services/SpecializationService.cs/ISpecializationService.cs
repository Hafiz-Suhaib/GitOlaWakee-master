using Microsoft.AspNetCore.Identity.UI.V3.Pages.Internal.Account;
using Microsoft.EntityFrameworkCore;
using OlaWakeel.Data;
using OlaWakeel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Services.ISpecializationService.cs
{
   public interface ISpecializationService
    {
        
        Task AddSpecialization(Specialization addSpecialization);
        Task<List<Specialization>> GetSpecializations();
        Task Delete(int id);
        Task EditSpecialization(Specialization editSpecialization);

        Task<Specialization> GetById(int id);
    }

    //implementations

    public class SpecializationService : ISpecializationService
    {
        private readonly ApplicationDbContext _context;

        public SpecializationService( ApplicationDbContext context )
        {
            _context = context;

        }

        public async Task AddSpecialization(Specialization addSpecialization)
        {
            await _context.Specializations.AddAsync(addSpecialization);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            Specialization specialization = await _context.Specializations.FindAsync(id);
            _context.Remove(specialization);
            await _context.SaveChangesAsync();
        }

        public async Task<Specialization> GetById(int id)
        {
            Specialization specialization = await _context.Specializations.FindAsync(id);
            return specialization;
            
        }

        public async Task EditSpecialization(Specialization editSpecialization)
        {
            Specialization specialization = await _context.Specializations.FindAsync(editSpecialization.SpecializationId);
            specialization.SpecializationName = editSpecialization.SpecializationName;
            specialization.Description = editSpecialization.Description;
            _context.Update(specialization);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Specialization>> GetSpecializations()
        {
            var specializationList = await _context.Specializations.ToListAsync();
            return specializationList;

        }





    }
}
