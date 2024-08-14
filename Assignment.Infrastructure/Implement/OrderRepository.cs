using Assignment.Application.DataTransferObj.CartDto;
using Assignment.Application.DataTransferObj.OderDetailDto;
using Assignment.Application.DataTransferObj.OrderDto;
using Assignment.Application.Interface;
using Assignment.Domain.Database.Entities;
using Assignment.Domain.Enum;
using Assignment.Infrastructure.Database.AppDbContext;
using Assignment.Infrastructure.Extension;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Implement
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;
        private readonly JwtUserId jwtUserId;
        private readonly IMapper mapper;
        private readonly IHttpContextAccessor httpContextAccessor;

        public OrderRepository(AppDbContext context, JwtUserId jwtUserId, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            this.context = context;
            this.jwtUserId = jwtUserId;
            this.httpContextAccessor = httpContextAccessor;
            this.mapper = mapper;
        }
        public string GetUserId()
        {
            var request = httpContextAccessor.HttpContext.Request;
            var token = jwtUserId.ExtractTokenFromHeader(request);
            var userId = jwtUserId.GetUserIdFromToken(token);
            if (userId == null)
            {
                return null;
            }
            return userId;
        }

        public async Task<Order> Create(Order order)
        {
            var userJwt = GetUserId();
            if (!Guid.TryParse(userJwt, out Guid userGuid))
            {
                return null;
            }

            var user = await context.Users.FirstOrDefaultAsync(x => x.Id == userGuid);
            var cart = await context.Carts.FirstOrDefaultAsync(x => x.UserId == user.Id);
            var cartDetails= await context.Cartdetails.Where(x => x.CartId == cart.Id && x.Statust == CartDetailStatust.Cancel).ToListAsync();
            if (user == null)
            {
                return null;
            }

            order.Id = Guid.NewGuid();
            order.UserId = user.Id;
            order.OrderDate = DateTime.Now;
            order.OrderStatus = OrderStatus.Pending;
            order.ShippingAddress = user.Address;

            using (var transaction = await context.Database.BeginTransactionAsync())
            {
                try
                {
                    await context.Orders.AddAsync(order);
                    await context.SaveChangesAsync();

                    // Tạo và thêm các OrderDetails
                    foreach (var detailRequest in cartDetails)
                    {
                        var orderDetail = new OrderDetail
                        {
                            Id = Guid.NewGuid(),
                            OrderId = order.Id,
                            ProductId = detailRequest.ProductId,
                            Quantity = detailRequest.Quantity,
                            Price = detailRequest.Price
                        };
                        // Remove the list of cart details
                        await context.OrderDetails.AddAsync(orderDetail);
                    }
                    context.Cartdetails.RemoveRange(cartDetails);
                    await context.SaveChangesAsync();

                    // Cập nhật tổng số tiền của Order
                    order.Amount = context.OrderDetails
                        .Where(x => x.OrderId == order.Id)
                        .Sum(x => x.Price);

                    await context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }

            return order;
        }


        public async Task<Order> Delete(Guid id)
        {
            var order = await context.Orders.FindAsync(id);
            if (order == null)
            {
                return null;
            }
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<OrderDto>> GetAll()
        {
            var userJwt = GetUserId();
            var userId = context.Users.FirstOrDefault(x => x.Id == Guid.Parse(userJwt));
            if (userId == null)
            {
                return null;
            }
            var orders = context.Orders.Where(x => x.UserId == userId.Id).OrderByDescending(x=>x.OrderDate).Select(x => new OrderDto
            {
                Id = x.Id,
                UserId = x.UserId,
                OrderDate = x.OrderDate,
                OrderStatus = x.OrderStatus,
                Amount = x.Amount,
                ShippingAddress = x.ShippingAddress,
                PaymentMethod = x.PaymentMethod
            });
            return orders;
        }

        public async Task<Order> GetById(Guid id)
        {
            var order = await context.Orders.FindAsync(id);
            if (order == null)
            {
                return null;
            }
            return order;
        }

        public async Task<Order> Update(Order order)
        {
            context.Orders.Update(order);
            await context.SaveChangesAsync();
            return order;
        }

        public async Task<IEnumerable<OrderDetailDto>> GetDetailDtos()
        {
            var orderDetails = await context.OrderDetails.Include(x=>x.Product).Select(x => new OrderDetailDto
                                {
                                    Id = x.Id,
                                    OrderId = x.OrderId,
                                    ProductId = x.ProductId,
                                    ProductName = x.Product.Name,
                                    Quantity = x.Quantity,
                                    Price = x.Price
                                }).ToListAsync();
            return orderDetails;
        }
    }
}
