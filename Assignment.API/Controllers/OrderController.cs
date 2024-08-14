using Assignment.Application.DataTransferObj.OderDetailDto;
using Assignment.Application.DataTransferObj.OrderDto;
using Assignment.Application.Interface;
using Assignment.Domain.Database.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository orderRepository;
        private readonly IMapper mapper;

        public OrderController(IOrderRepository orderRepository,IMapper mapper)
        {
            this.orderRepository = orderRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDto>>> Get()
        {
            var order = await orderRepository.GetAll();
            return Ok(order);
        }
        [HttpGet("detail")]
        public async Task<ActionResult<IEnumerable<OrderDetailDto>>> GetDetail()
        {
            var orderDetail = await orderRepository.GetDetailDtos();
            return Ok(orderDetail);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var order = await orderRepository.GetById(id);
            return Ok(order);
        }
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromBody] OrderCreateRequest orderCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var order = mapper.Map<Order>(orderCreate);
            await orderRepository.Create(order);
            return Ok(orderCreate);
        }
        [HttpPut("update")]
        public async Task<ActionResult> Update(Guid id,[FromBody] OrderUpdateRequest orderUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orderItem = await orderRepository.GetById(id);
            orderItem.ShippingAddress = orderUpdate.ShippingAddress;
            orderItem.OrderStatus = orderUpdate.OrderStatus;
            await orderRepository.Update(orderItem);
            return Ok(orderItem);
        }

        [HttpDelete("delete")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var order = await orderRepository.Delete(id);
            return Ok(order);
        }
    }
}
