using Assignment.Application.DataTransferObj.CategoryDto;
using Assignment.Application.Interface;
using Assignment.Domain.Database.Entities;
using Assignment.Infrastructure.Database.AppDbContext;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _category;
        private readonly IMapper _mapper;

        // GET: api/<CategoryController>
        public CategoryController(ICategoryRepository category, IMapper mapper)
        {
            _category = category;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> Get()
        {
            var category = await _category.GetAll();
            if (category == null)
            {
                return NotFound();
            }
            else
            {
                var categoryDto = _mapper.Map<IEnumerable<CategoryDto>>(category);
                return Ok(categoryDto);
            }
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var category = await _category.GetById(id);
            return Ok(category);
        }

        // POST api/<CategoryController>
        [HttpPost("create")]
        public async Task<ActionResult> Create ([FromBody] CategoryCreateRequest category)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var categoryCreate = _mapper.Map<Category>(category);
                await _category.Create(categoryCreate);
                return Ok(categoryCreate);
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut("update")]
        public async Task<ActionResult> Update(Guid id, [FromBody] CategoryUpdateRequest categoryUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var itemUpdate = await _category.GetById(id);
                if (itemUpdate == null)
                {
                    return NotFound("Không tìm thấy");
                }
                else
                {
                    itemUpdate.Name = categoryUpdate.Name;
                    await _category.Update(itemUpdate);
                }
                return Ok(itemUpdate);
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("delete")]
        public async Task<ActionResult> DeleteById(Guid id)
        {
            var category = await _category.GetById(id);
            if (category == null)
            {
                return NotFound("Không tìm thấy");
            }
            await _category.DeleteById(id);
            return Ok("Đã xóa thành công" + category);
        }
    }
}
