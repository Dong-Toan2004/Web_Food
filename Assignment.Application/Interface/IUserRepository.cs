using Assignment.Application.DataTransferObj.LoginDto;
using Assignment.Application.DataTransferObj.UserDto;
using Assignment.Domain.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Application.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<UserDto>> GetAll(UserSearch userSearch);
        Task<User> GetById(Guid id);
        Task<User> Create(User user);
        Task<User> Update(User user);
        Task<User> Delete(Guid id);
        Task<User> Login(LoginRequest loginRequest);
        Task<User> Register(User user);
    }
}
