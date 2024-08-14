using Assignment.Application.DataTransferObj.CategoryDto;
using Assignment.Domain.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Application.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDto>> GetAll();
        Task<Category> GetById(Guid id);
        Task<Category> Create(Category category);
        Task<Category> Update(Category category);
        Task<Category> DeleteById(Guid id);
    }
}
