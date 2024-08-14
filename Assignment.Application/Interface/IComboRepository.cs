using Assignment.Application.DataTransferObj.CategoryDto;
using Assignment.Application.DataTransferObj.ComboDto;
using Assignment.Domain.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Application.Interface
{
    public interface IComboRepository
    {
        Task<IEnumerable<ComboDto>> GetAll();
        Task<Combo> GetById(Guid id);
        Task<Combo> Create(Combo category);
        Task<Combo> Update(Combo category);
        Task<Combo> DeleteById(Guid id);
    }
}
