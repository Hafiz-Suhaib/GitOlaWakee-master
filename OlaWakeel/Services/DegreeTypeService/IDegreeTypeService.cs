using Microsoft.EntityFrameworkCore;
using OlaWakeel.Data;
using OlaWakeel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlaWakeel.Services.DegreeTypeService
{
    public interface IDegreeTypeService
    {
        Task AddDegreeType(DegreeType addDegree);
        Task<List<DegreeType>> GetAllDegrees();
        Task Delete(int id);
        Task<DegreeType> GetById(int id);
        Task EditDegree(DegreeType editDegree);
        Task<DegreeType> SingleOrDefaultDT(int id);
    }
    public class DegreeTypeService : IDegreeTypeService
    {
        private readonly ApplicationDbContext _context;
        public DegreeTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddDegreeType(DegreeType addDegree)
        {
            await _context.DegreeTypes.AddAsync(addDegree);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var DegreeTypes = await _context.DegreeTypes.FindAsync(id);
            _context.Remove(DegreeTypes);
            await _context.SaveChangesAsync();
        }

        public async Task EditDegree(DegreeType editDegree)
        {
            var degree =  _context.DegreeTypes.Find(editDegree.DegreeTypeId);
            degree.TypeName = editDegree.TypeName;
            _context.Update(degree);
            _context.SaveChanges();
        }

        public async Task<List<DegreeType>> GetAllDegrees()
        {
            var degreeList = await _context.DegreeTypes.ToListAsync();
            return degreeList;
        }

        public async Task<DegreeType> GetById(int id)
        {
            var degreeById = await _context.DegreeTypes.FindAsync(id);
            return degreeById;
        }

        public async Task<DegreeType> SingleOrDefaultDT(int id)
        {
            var degreeType = await _context.DegreeTypes.SingleOrDefaultAsync(s => s.DegreeTypeId == id);
            return degreeType;
        }
    }
}
