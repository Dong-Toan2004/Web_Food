using Assignment.Application.DataTransferObj.ProductDto;
using Assignment.Domain.Database.Entities;

namespace Web.Services.IServices
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAll(ProductSearch productSearch);
        Task<Product> GetById(Guid id);
        Task<bool> Update(Guid id,Product product);
        Task<bool> Create(Product product);
        Task<bool> Delete(Guid id);
    }
}
