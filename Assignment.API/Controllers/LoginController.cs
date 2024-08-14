using Assignment.Application.DataTransferObj.LoginDto;
using Assignment.Application.DataTransferObj.UserDto;
using Assignment.Application.Interface;
using Assignment.Domain.Database.Base;
using Assignment.Domain.Database.Entities;
using Assignment.Domain.Enum;
using Assignment.Infrastructure.Database.AppDbContext;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _user;

        public LoginController(IConfiguration configuration, IUserRepository user)
        {
            _configuration = configuration;
            _user = user;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginRequest loginRequest)
        {
            var user = await _user.Login(loginRequest);
            if (user != null)
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim("UserId", user.Id.ToString()),
                        new Claim(ClaimTypes.Role, user.Role.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = _configuration["Jwt:Issuer"],
                    Audience = _configuration["Jwt:Audience"],
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenString = tokenHandler.WriteToken(token);

                return Ok(new LoginReponse { Successfull = true, Token = tokenString });
            }

            return Unauthorized(new LoginReponse { Successfull = false, Error = "Invalid username or password." });
        }
        [HttpPost("register")]
        public async Task<ActionResult> Register(UserRegisterRequest userRegister)
        {
            var salt = PasswordHelper.CreateSalt();
            var passwordHash = PasswordHelper.CreatePasswordHash(userRegister.Password, salt);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var user = new User()
                {
                    Id = userRegister.Id,
                    UserName = userRegister.UserName,
                    Email = userRegister.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = salt,
                    Address = userRegister.Address,
                    Role = Role.Client,
                    PhoneNumber = userRegister.PhoneNumber
                };
                await _user.Create(user);
                return Ok(user);
            }
        }
    }
}
