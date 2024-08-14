using Assignment.Application.DataTransferObj.LoginDto;
using Assignment.Application.DataTransferObj.UserDto;
using Assignment.Domain.Database.Entities;

namespace Web.Services.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAll(UserSearch userSearch);
        Task<User> GetById(Guid id);
        Task<bool> Update(User user);
        Task<LoginReponse> Login(LoginRequest loginRequest);
        Task<bool> Register(User user);
    }
}
