using Assignment.Application.DataTransferObj.CategoryDto;
using Assignment.Application.Interface;
using Assignment.Domain.Database.Entities;
using Assignment.Infrastructure.Database.AppDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Implement
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Category> Create(Category category)
        {
            category.Id = Guid.NewGuid();
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> DeleteById(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(); 
            return category;
        }

        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            var category = await _context.Categories.Select(x => new CategoryDto
            {
                Id = x.Id,
                Name = x.Name,
            }).ToListAsync();
            return category;
        }

        public async Task<Category> GetById(Guid id)
        {
            var category = await _context.Categories.FindAsync(id);
            return category;
        }

        public async Task<Category> Update(Category category)
        {
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
