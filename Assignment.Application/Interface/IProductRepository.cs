using Assignment.Application.DataTransferObj.ProductDto;
using Assignment.Domain.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Application.Interface
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductDto>> GetAll(ProductSearch productSearch);
        Task<Product> GetById(Guid id);
        Task<Product> Update(Product product);
        Task<Product> Create(Product product);
        Task<Product> Delete(Guid id);
    }
}
