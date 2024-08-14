using Assignment.Application.DataTransferObj.UserDto;
using Assignment.Application.Interface;
using Assignment.Domain.Database.Base;
using Assignment.Domain.Database.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _user;
        private readonly IMapper _mapper;

        // GET: api/<UserController>
        public UserController(IUserRepository user, IMapper mapper)
        {
            _user = user;
            _mapper = mapper;
        }
        [HttpGet]        
        public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromQuery] UserSearch userSearch)
        {
            var user = await _user.GetAll(userSearch);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        // GET api/<UserController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var user = await _user.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(user);
            }
        }

        // POST api/<UserController>
        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Create([FromBody] UserCreateRequest userCreate)
        {
            var salt = PasswordHelper.CreateSalt();
            var passwordHash = PasswordHelper.CreatePasswordHash(userCreate.Password, salt);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var user = new User()
                {
                    UserName = userCreate.UserName,
                    Email = userCreate.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = salt,
                    Address = userCreate.Address,
                    Role = userCreate.Role,
                    PhoneNumber = userCreate.PhoneNumber
                };
                await _user.Create(user);
                return Ok(user);
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("update")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Update(Guid id, [FromBody] UserUpdateRequest updateRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userItem = await _user.GetById(id);
            if (userItem == null)
            {
                return NotFound();
            }
            else
            {
                userItem.Email = updateRequest.Email;
                userItem.Address = updateRequest.Address;
                userItem.Role = updateRequest.Role;
                userItem.PhoneNumber = updateRequest.PhoneNumber;
                await _user.Update(userItem);
            }
            return Ok(userItem);
        }

        // DELETE api/<UserController>/5
        [HttpDelete("delete")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await _user.GetById(id);
            if (user == null)
            {
                return NotFound("Không tìm thấy");
            }
            await _user.Delete(id);
            return Ok("Đã xóa thành công" + user);
        }
    }
}
