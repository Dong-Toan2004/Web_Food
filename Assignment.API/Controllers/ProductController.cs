using Assignment.Application.DataTransferObj.ProductDto;
using Assignment.Application.Interface;
using Assignment.Domain.Database.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Assignment.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _product;
        private readonly IMapper _mapper;

        // GET: api/<ProductController>
        public ProductController(IProductRepository product, IMapper mapper)
        {
            _product = product;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> Get([FromQuery] ProductSearch productSearch)
        {
            var products = await _product.GetAll(productSearch);
            if (products == null)
            {
                return NotFound();
            }
            else
            {
                //var productDto = _mapper.Map<IEnumerable<Product>>(products);
                return Ok(products);
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(Guid id)
        {
            var product = await _product.GetById(id);
            return Ok(product);
        }

        [HttpGet("image")]
        public async Task<ActionResult> GetImage(Guid id)
        {
            var product = await _product.GetById(id);
            var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", product.Image.TrimStart('/'));
            var imageFileStream = System.IO.File.OpenRead(imagePath);

            return File(imageFileStream, "image/jpeg"); // Điều chỉnh loại nội dung nếu cần thiết
        }

        // POST api/<ProductController>
        [HttpPost("create")]
        public async Task<ActionResult> Create([FromForm] ProductCreateRequest productCreate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Lưu ảnh lên server nếu có
            if (productCreate.ImageFile != null)
            {
                // Đường dẫn tuyệt đối tới thư mục lưu trữ ảnh
                var imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadImage");
                var imagePath = Path.Combine(imageFolderPath, productCreate.ImageFile.FileName);

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(imageFolderPath))
                {
                    Directory.CreateDirectory(imageFolderPath);
                }

                // Lưu file vào thư mục
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await productCreate.ImageFile.CopyToAsync(stream);
                }

                // Cập nhật đường dẫn ảnh trong productCreate
                productCreate.Image = $"/UploadImage/{productCreate.ImageFile.FileName}";
            }

            var product = _mapper.Map<Product>(productCreate);
            await _product.Create(product);

            return Ok(product);
        }


        [HttpPut("update/{id}")]
        public async Task<ActionResult> Update(Guid id, [FromForm] ProductUpdateRequest productUpdate, IFormFile imageFile)
        {
            // Kiểm tra nếu ModelState không hợp lệ
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Lấy sản phẩm cần cập nhật theo ID
            var itemUpdate = await _product.GetById(id);
            if (itemUpdate == null)
            {
                return NotFound("Không tìm thấy món ăn");
            }

            // Cập nhật các thuộc tính của sản phẩm
            itemUpdate.Name = productUpdate.Name;
            itemUpdate.Description = productUpdate.Description;
            itemUpdate.Price = productUpdate.Price;
            itemUpdate.CategoryId = productUpdate.CategoryId;

            // Cập nhật ảnh nếu có
            if (imageFile != null)
            {
                // Đường dẫn tuyệt đối tới thư mục lưu trữ ảnh
                var imageFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadImage");
                var imagePath = Path.Combine(imageFolderPath, imageFile.FileName);

                // Tạo thư mục nếu chưa tồn tại
                if (!Directory.Exists(imageFolderPath))
                {
                    Directory.CreateDirectory(imageFolderPath);
                }

                // Lưu file vào thư mục
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                // Xóa ảnh cũ nếu có
                if (!string.IsNullOrEmpty(itemUpdate.Image) && System.IO.File.Exists(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", itemUpdate.Image)))
                {
                    try
                    {
                        // Đảm bảo file không bị khóa trước khi xóa
                        System.IO.File.SetAttributes(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", itemUpdate.Image), FileAttributes.Normal);
                        System.IO.File.Delete(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", itemUpdate.Image));
                    }
                    catch (IOException ioEx)
                    {
                        // Log lỗi và xử lý thích hợp nếu cần
                        return StatusCode(500, $"Error deleting old image: {ioEx.Message}");
                    }
                }

                // Cập nhật đường dẫn ảnh mới
                itemUpdate.Image = $"/UploadImage/{imageFile.FileName}";
            }

            // Gọi phương thức Update để lưu các thay đổi
            await _product.Update(itemUpdate);

            // Trả về kết quả thành công với sản phẩm đã cập nhật
            return Ok(itemUpdate);
        }


        // DELETE api/<ProductController>/5
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            // Lấy sản phẩm cần xóa theo ID
            var itemToDelete = await _product.GetById(id);
            if (itemToDelete == null)
            {
                return NotFound("Không tìm thấy món ăn");
            }

            // Gọi phương thức Delete để xóa sản phẩm
            await _product.Delete(id);

            // Trả về kết quả thành công với thông báo xác nhận
            return Ok("Xóa món ăn thành công" + itemToDelete);
        }

    }
}
