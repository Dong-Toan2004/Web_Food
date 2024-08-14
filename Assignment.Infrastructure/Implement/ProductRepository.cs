using Assignment.Application.DataTransferObj.ProductDto;
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
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Product> Create(Product product)
        {
            product.Id = Guid.NewGuid();
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Delete(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<IEnumerable<ProductDto>> GetAll(ProductSearch productSearch)
        {
            var query = _context.Products.Include(x=>x.Category).AsQueryable();
            if (!string.IsNullOrEmpty(productSearch.Name))
            {
                query = query.Where(x => x.Name.Contains(productSearch.Name));
            }
            if (productSearch.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == productSearch.CategoryId);
            }
            return await query.Select(x=> new ProductDto
            {
                Id = x.Id,
                Name = x.Name,
                Image = x.Image,
                Description = x.Description,
                Price = x.Price,
                CategoryId = x.CategoryId,
                CategoryName = x.Category.Name
            }).ToListAsync();
        }

        public async Task<Product> GetById(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }
    }
}
