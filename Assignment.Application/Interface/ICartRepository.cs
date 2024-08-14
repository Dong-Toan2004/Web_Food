using Assignment.Application.DataTransferObj.CartDto;
using Assignment.Domain.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Application.Interface
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartDetailDto>> GetAll();
        Task<Cartdetail> GetById(Guid id);
        Task<Cartdetail> Create(Cartdetail category);
        Task<Cartdetail> Update(Cartdetail category);
        Task<Cartdetail> DeleteById(Guid id);
    }
}
