using Assignment.Application.DataTransferObj.CategoryDto;
using Assignment.Domain.Database.Entities;
using Web.Services.IServices;

namespace Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly HttpClient _client;

        public CategoryService(HttpClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<CategoryDto>> GetAll()
        {
            var categories = await _client.GetFromJsonAsync<IEnumerable<CategoryDto>>("api/Category");
            return categories;
        }

        public async Task<Category> GetById(Guid id)
        {
            var category = await _client.GetFromJsonAsync<Category>($"api/Category/{id}");
            return category;
        }
    }
}
