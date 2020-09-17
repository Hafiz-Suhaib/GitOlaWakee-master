
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

namespace OlaWakeel.Services.CaseCategoryService
{
    
    public interface ICaseCategoryService
    {
        Task<bool> AddCaseCategory(CatDto addCaseCategory);
        List<CaseCategory> GetAllCaseCategory();
        Task<List<CaseCategory>> GetAllNonRecursive();
        Task UpdateCaseCategory(CatDto editCaseCategory);
        Task DeleteCaseCategory(int Id);
        Task<CaseCategory> CatGetById(int id);
    }
    public class CaseCategoryService : ICaseCategoryService
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public CaseCategoryService(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;
            _context = context;
        }

        public async Task<bool> AddCaseCategory(CatDto addCaseCategory)
        {
            var result = await _context.CaseCategories.Where(x => x.Name.Equals(addCaseCategory.Name)).ToListAsync();
            if (result.Count == 0)
            {
                string uniqueFileName = UploadedFile(addCaseCategory);
                CaseCategory caseCategory = new CaseCategory
                {
                    Name = addCaseCategory.Name,
                    Description=addCaseCategory.Description,
                    ParentId=addCaseCategory.ParentId,
                    VectorIcon=uniqueFileName
                
                };
                await _context.CaseCategories.AddAsync(caseCategory);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
        private string UploadedFile(CatDto addCaseCategory)
        {
            string uniqueFileName = null;

            if (addCaseCategory.Image != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "VectorIconImages");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + addCaseCategory.Image.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    addCaseCategory.Image.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public async Task DeleteCaseCategory(int Id)
        {
            //var caseCategory = await _context.CaseCategories.FindAsync(Id);
            //var del = await _context.CaseCategories.Where(x => x.ParentId == Id ).ToListAsync();
            //_context.CaseCategories.Remove(caseCategory);
            //_context.CaseCategories.RemoveRange(del);
            //await _context.SaveChangesAsync();

            List<CaseCategory> alllCat =await _context.CaseCategories.Include(x => x.Children).ToListAsync();
            var catList = alllCat
               .Where(e => e.ParentId == Id) /* grab only the root parent nodes */
               .Select(e => new CaseCategory
               {
                   CaseCategoryId = e.CaseCategoryId,
                   Name = e.Name,
                   ParentId = e.ParentId,
                   Description = e.Description,
                   Children = GetChildren(alllCat, e.CaseCategoryId) /* Recursively grab the children */
               }).ToList();

          //  CaseCategory caseCategory =await _context.CaseCategories.FindAsync(Id);
           await DeleteRecursive(catList);
           var del= await CatGetById(Id);
            _context.CaseCategories.Remove(del);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCaseCategory(CatDto editCaseCategory)
        {
            string uniqueFileName = UploadedFile(editCaseCategory);

            var caseCategory = await _context.CaseCategories.FindAsync(editCaseCategory.CaseCategoryId);
            caseCategory.Name = editCaseCategory.Name;
            caseCategory.ParentId = editCaseCategory.ParentId;
            caseCategory.Description = editCaseCategory.Description;
            if (uniqueFileName != null) 
            {
                caseCategory.VectorIcon = uniqueFileName;
            }
            
            _context.Update(caseCategory);
            await _context.SaveChangesAsync();
        }
       
        public async Task DeleteRecursive(List<CaseCategory> category) 
        {
            CaseCategoryDto dto = new CaseCategoryDto();
            foreach(var item in category)
            {               
                   await DeleteRecursive(item.Children);
                var del =await CatGetById(item.CaseCategoryId);
                _context.CaseCategories.Remove(del);
                
            }
           await _context.SaveChangesAsync();
        }
        public List<CaseCategory> GetAllCaseCategory()
        {
            List<CaseCategory> alllCat =  _context.CaseCategories.Include(x=>x.Children).ToList();
             var catList= alllCat
                .Where(e => e.ParentId == 0) /* grab only the root parent nodes */
                .Select(e => new CaseCategory
            {
                CaseCategoryId = e.CaseCategoryId,
                Name = e.Name,
                ParentId = e.ParentId,
                Description=e.Description,
                VectorIcon=e.VectorIcon,
                Children = GetChildren(alllCat, e.CaseCategoryId) /* Recursively grab the children */
            }).ToList();
            return (catList);
        }
        private  static List<CaseCategory> GetChildren(List<CaseCategory> items, int parentId)
        {
            return items
                .Where(x => x.ParentId == parentId)
                .Select(e => new CaseCategory
                {
                    CaseCategoryId = e.CaseCategoryId,
                    Name = e.Name,
                    ParentId = e.ParentId,
                    Description=e.Description,
                    VectorIcon = e.VectorIcon,
                    Children =GetChildren(items, e.CaseCategoryId)
                }).ToList();
        }

        public async Task<List<CaseCategory>> GetAllNonRecursive()
        {
            //var catList = await _context.CaseCategories.Select(x=> new CaseCategory { Id=x.Id,Name=x.Name,ParentId=x.ParentId}).ToListAsync();
            var catList = await _context.CaseCategories.ToListAsync();
            return catList;
        }

        public async Task<CaseCategory> CatGetById(int id)
        {
          var  CatGetById = await _context.CaseCategories.FindAsync(id);
          return CatGetById;
        }
    }
}
