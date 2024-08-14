using Assignment.Application.DataTransferObj.CartDto;
using Assignment.Application.DataTransferObj.OderDetailDto;
using Assignment.Application.DataTransferObj.OrderDto;
using Assignment.Domain.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Application.Interface
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderDto>> GetAll();
        Task<IEnumerable<OrderDetailDto>> GetDetailDtos();
        Task<Order> GetById(Guid id);
        Task<Order> Create(Order order);
        Task<Order> Update(Order order);
        Task<Order> Delete(Guid id);
    }
}
