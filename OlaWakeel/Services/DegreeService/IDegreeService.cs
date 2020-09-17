using Microsoft.EntityFrameworkCore;
using OlaWakeel.Data;
using OlaWakeel.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OlaWakeel.Services.DegreeService
{
    //definations
    public interface IDegreeService
    {
        Task AddDegree(Degree addDegree);
        Task<List<Degree>> GetAllDegrees();
        Task Delete(int id);
        Task<Degree> GetById(int id);
        Task EditDegree(int id, Degree editDegree);
        Task<Degree> SingleOrDefaultDG(int id);
    }

    //Implementations
    public class DegreeService : IDegreeService
    {
        private readonly ApplicationDbContext _context;
        public DegreeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddDegree(Degree addDegree)
        {
            var value = new Degree
            {
                Description = addDegree.Description,
                EligibleAfter = addDegree.EligibleAfter,
                Name = addDegree.Name,
                PreRequisite = addDegree.PreRequisite,
                DegreeTypeId = addDegree.DegreeTypeId
            };
            await _context.Degrees.AddAsync(value);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var degree = await _context.Degrees.FindAsync(id);
            _context.Remove(degree);
            await _context.SaveChangesAsync();
        }

        public async Task EditDegree(int id, Degree editDegree)
        {
            var degree = await _context.Degrees.FindAsync(id);
            degree.Name = editDegree.Name;
            degree.Description = editDegree.Description;
            degree.PreRequisite = editDegree.PreRequisite;
            degree.EligibleAfter = editDegree.EligibleAfter;
            degree.DegreeTypeId = editDegree.DegreeTypeId;
            degree.DegreeTypes = editDegree.DegreeTypes;
            _context.Update(degree);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Degree>> GetAllDegrees()
        {
            var degreeList = await _context.Degrees.ToListAsync();
            return degreeList;
        }

        public async Task<Degree> GetById(int id)
        {
            var degreeById = await _context.Degrees.FindAsync(id);
            return degreeById;
        }

        public async Task<Degree> SingleOrDefaultDG(int id)
        {
            var degreeListById = await _context.Degrees.Include(s => s.DegreeTypes).SingleOrDefaultAsync(s => s.DegreeId == id);
            return degreeListById;
        }
    }
}
