using Assignment.Application.DataTransferObj.CategoryDto;
using Assignment.Domain.Database.Entities;

namespace Web.Services.IServices
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAll();
        Task<Category> GetById(Guid id);
    }
}
