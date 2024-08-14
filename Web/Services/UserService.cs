using Assignment.Application.DataTransferObj.LoginDto;
using Assignment.Application.DataTransferObj.UserDto;
using Assignment.Domain.Database.Entities;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.WebSockets;
using Web.Services.IServices;

namespace Web.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;

        public UserService(HttpClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<UserDto>> GetAll(UserSearch userSearch)
        {
            var queryParam = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(userSearch.UserName))
            {
                queryParam.Add("UserName", userSearch.UserName);
            }
            if (userSearch.Role.HasValue)
            {
                queryParam.Add("Role", userSearch.Role.ToString());
            }
            var urlWithQuery = QueryHelpers.AddQueryString("api/User", queryParam);
            var user = await _client.GetFromJsonAsync<IEnumerable<UserDto>>(urlWithQuery);
            return user;
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _client.GetFromJsonAsync<User>($"api/User/{id}");
            return user;
        }

        public async Task<LoginReponse> Login(LoginRequest loginRequest)
        {
            var response = await _client.PostAsJsonAsync("api/Login/login", loginRequest);
            response.EnsureSuccessStatusCode();
            var user = await response.Content.ReadFromJsonAsync<LoginReponse>();
            return user;
        }

        public async Task<bool> Register(User user)
        {
            var response = await _client.PostAsJsonAsync("api/Login/register", user);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Update(User user)
        {
            var response = await _client.PutAsJsonAsync("api/User/update", user);
            return response.IsSuccessStatusCode;
        }
    }
}
