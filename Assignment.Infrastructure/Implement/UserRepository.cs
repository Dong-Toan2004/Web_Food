using Assignment.Application.DataTransferObj.CartDto;
using Assignment.Application.DataTransferObj.LoginDto;
using Assignment.Application.DataTransferObj.UserDto;
using Assignment.Application.Interface;
using Assignment.Domain.Database.Base;
using Assignment.Domain.Database.Entities;
using Assignment.Domain.Enum;
using Assignment.Infrastructure.Database.AppDbContext;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Infrastructure.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<User> Create(User user)
        {        
            user.Id = Guid.NewGuid();
            CartDto cart = new CartDto()
            {
                Id = user.Id,
                UserId = user.Id,
            };
            
            await _context.Users.AddAsync(user);
            await _context.Carts.AddAsync(_mapper.Map<Cart>(cart));
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Delete(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            var cart = await _context.Carts.FindAsync(id);
            _context.Carts.Remove(cart);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<UserDto>> GetAll(UserSearch userSearch)
        {
            var query = _context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(userSearch.UserName))
            {
                query = query.Where(x=>x.UserName.Contains(userSearch.UserName));
            }
            if (userSearch.Role.HasValue)
            {
                query = query.Where(x => x.Role == userSearch.Role.Value);
            }
            return await query.Select(x=> new UserDto
            {
                Id = x.Id,
                UserName = x.UserName,
                PhoneNumber = x.PhoneNumber,
                Address = x.Address,
                Email = x.Email,
                Role = x.Role,
            }).ToListAsync();
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            return user ;
        }

        public async Task<User> Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user ;
        }

        public async Task<User> Login(LoginRequest loginRequest)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName == loginRequest.Username);
            if (user == null || !VerifyPassword(loginRequest.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }
            return user ;
        }
        public bool VerifyPassword(string password, string storedHash, string storedSalt)
        {
            // Tạo hash cho mật khẩu nhập vào bằng cách sử dụng salt đã lưu trữ
            var hash = PasswordHelper.CreatePasswordHash(password, storedSalt);
            // So sánh hash của mật khẩu nhập vào với hash đã lưu trữ
            return hash == storedHash;
        }

        public async Task<User> Register(User user)
        {
            user.Id = Guid.NewGuid();
            user.Role = Role.Client;
            CartDto cart = new CartDto()
            {
                Id = user.Id,
                UserId = user.Id,
            };
            await _context.Users.AddAsync(user);
            await _context.Carts.AddAsync(_mapper.Map<Cart>(cart));
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
