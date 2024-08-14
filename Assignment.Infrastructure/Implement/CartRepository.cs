using Assignment.Application.DataTransferObj.CartDto;
using Assignment.Application.Interface;
using Assignment.Domain.Database.Entities;
using Assignment.Domain.Enum;
using Assignment.Infrastructure.Database.AppDbContext;
using Assignment.Infrastructure.Extension;
using AutoMapper;
using Azure;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Implement
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext context;
        private readonly IMapper mapper;
        private readonly JwtUserId jwtUser;
        private readonly IHttpContextAccessor httpContextAccessor;

        public CartRepository(AppDbContext context, IMapper mapper, JwtUserId jwtUser, IHttpContextAccessor httpContextAccessor)
        {
            this.context = context;
            this.mapper = mapper;
            this.jwtUser = jwtUser;
            this.httpContextAccessor = httpContextAccessor;
        }
        public string GetUserId()
        {
            var request = httpContextAccessor.HttpContext.Request;
            var token = jwtUser.ExtractTokenFromHeader(request);
            var userId = jwtUser.GetUserIdFromToken(token);
            if (userId == null)
            {
                return null;
            }
            return userId;
        }
        public async Task<Cartdetail> Create(Cartdetail cartdetail)
        {
            var userJwt = GetUserId();
            var userId = await context.Users.FindAsync(Guid.Parse(userJwt));
            if (userId == null)
            {
                return null;
            }
            if (cartdetail.Quantity < 0)
            {
                return null;
            }

            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == cartdetail.ProductId);
            if (product == null)
            {
                return null;
            }

            var cartdetailItem = await context.Cartdetails
                .FirstOrDefaultAsync(x => x.ProductId == cartdetail.ProductId && x.CartId == userId.Id);

            if (cartdetailItem != null)
            {
                if (cartdetail.Quantity == 0)
                {
                    cartdetail.Quantity++;
                }
                else
                {
                    cartdetail.Quantity = cartdetailItem.Quantity + cartdetail.Quantity;
                }
                cartdetail.Price = cartdetail.Quantity * product.Price;
                cartdetailItem.Quantity = cartdetail.Quantity;
                cartdetailItem.Price = cartdetail.Price;
                context.Cartdetails.Update(cartdetailItem);
            }
            else
            {
                cartdetail.Id = Guid.NewGuid();
                cartdetail.CartId = userId.Id;
                cartdetail.ProductId = cartdetail.ProductId;
                cartdetail.Quantity = 1; // Đặt số lượng ban đầu là 1
                cartdetail.Price = cartdetail.Quantity * product.Price;
                cartdetail.Statust = CartDetailStatust.Cancel;
                await context.Cartdetails.AddAsync(cartdetail);
            }

            await context.SaveChangesAsync();
            return cartdetail;
        }


        public async Task<Cartdetail> DeleteById(Guid id)
        {
            var cartItemDelete = await context.Cartdetails.FindAsync(id);
            context.Cartdetails.Remove(cartItemDelete);
            await context.SaveChangesAsync();
            return cartItemDelete;
        }
        
        public async Task<IEnumerable<CartDetailDto>> GetAll()
        {
            var userJwt = GetUserId();
            var userId = context.Users.FirstOrDefault(x=>x.Id == Guid.Parse(userJwt));
            if (userId == null)
            {
                return null;
            }
            var cart = await context.Cartdetails.Where(x=>x.CartId == userId.Id && x.Statust == CartDetailStatust.Cancel).Select(x=> new CartDetailDto
            {
                Id = x.Id,
                CartId = x.CartId,
                ProductId = x.ProductId,
                Quantity = x.Quantity,
                Price = x.Price,
            }).ToListAsync();
            return cart;
        }

        public async Task<Cartdetail> GetById(Guid id)
        {
            var cartItem = await context.Cartdetails.FindAsync(id);
            return cartItem;
        }

        public async Task<Cartdetail> Update(Cartdetail category)
        {
            context.Cartdetails.Update(category);
            await context.SaveChangesAsync();
            return category;
        }
    }
}
