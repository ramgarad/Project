using Microsoft.EntityFrameworkCore;
using Product_Management.Context;
using Product_Management.Models;
using Product_Management.Repository.Interface_Repository;
using System.Drawing.Printing;

namespace Product_Management.Repository.Interface_Implementation
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<List<Category>> GetCategories(int pageNumber, int pageSize)
        {
                return await _context.Categories
                    .OrderBy(c => c.CategoryId) 
                    .Skip((pageNumber - 1) * pageSize) 
                    .Take(pageSize) 
                    .ToListAsync();
        }

        public async Task<int> GetTotalCategoryCount()
        {
            
                return await _context.Categories.CountAsync();
        }


        public async Task<Category> GetCategoryById(int id)
        {
            return await _context.Categories.FindAsync(id);
        }

        public async Task AddCategory(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category != null)
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
            }
        }
    }
}
