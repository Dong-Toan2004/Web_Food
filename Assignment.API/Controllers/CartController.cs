using Assignment.Application.DataTransferObj.CartDto;
using Assignment.Application.Interface;
using Assignment.Domain.Database.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        // GET: api/<CartController>
        private readonly ICartRepository _cartRepository;
        private readonly IMapper _mapper;
        public CartController(ICartRepository cartRepository, IMapper mapper)
        {
            _cartRepository = cartRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<CartDetailDto>>> Get()
        {
            var cart = await _cartRepository.GetAll();
            if (cart == null)
            {
                return NotFound();
            }
            return Ok(cart);
        }

        // GET api/<CartController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> GetById(Guid id)
        {
            var cart = await _cartRepository.GetById(id);
            var cartDto = _mapper.Map<CartDetailDto>(cart);
            return Ok(cartDto);
        }

        // POST api/<CartController>
        [HttpPost("create")]
        [Authorize]
        public async Task<ActionResult> Create([FromBody] CartDetailCreateRequest cartDetailCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cart = _mapper.Map<Cartdetail>(cartDetailCreate);
            await _cartRepository.Create(cart);
            return Ok(cartDetailCreate);
        }

        // PUT api/<CartController>/5
        [HttpPut("update")]
        [Authorize]
        public async Task<ActionResult> Update(Guid id, [FromBody] CartDetailUpdateRequest cartDetailUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var cartItem = await _cartRepository.GetById(id);
            if (cartItem == null)
            {
                return NotFound("Ko tìm thấy");
            }
            cartItem.Quantity = cartDetailUpdate.Quantity;
            cartItem.Price = cartDetailUpdate.Price;
            await _cartRepository.Update(cartItem);
            return Ok(cartItem);
        }

        // DELETE api/<CartController>/5
        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult> Delete(Guid id)
        {
            var cartItem = await _cartRepository.GetById(id);
            if (cartItem == null)
            {
                return NotFound("Ko tìm thấy");
            }
            await _cartRepository.DeleteById(cartItem.Id);
            return Ok();
        }
    }
}
