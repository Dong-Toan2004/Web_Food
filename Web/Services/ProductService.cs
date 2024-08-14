using Assignment.Application.DataTransferObj.ProductDto;
using Assignment.Domain.Database.Entities;
using Microsoft.AspNetCore.WebUtilities;
using Web.Services.IServices;

namespace Web.Services
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _client;
        public ProductService(HttpClient client)
        {
            _client = client;
        }
        public async Task<bool> Create(Product product)
        {
            var result = await _client.PostAsJsonAsync("api/Product/create", product);
            return result.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(Guid id)
        {
            var result = await _client.DeleteAsync($"api/Product/delete/?id={id}");
            return result.IsSuccessStatusCode;
        }

        public async Task<IEnumerable<ProductDto>> GetAll(ProductSearch productSearch)
        {
            // Tạo dictionary để lưu các tham số query từ productSearch
            var queryParams = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(productSearch.Name))
            {
                queryParams.Add("Name", productSearch.Name);
            }
            if (productSearch.CategoryId.HasValue)
            {
                queryParams.Add("CategoryId", productSearch.CategoryId.ToString());
            }
            // Xây dựng URL với các tham số query
            string urlWithQuery = QueryHelpers.AddQueryString("api/Product", queryParams);
            // Gửi request tới API với URL đã bao gồm các tham số query
            var result = await _client.GetFromJsonAsync<IEnumerable<ProductDto>>(urlWithQuery);
            return result;
        }

        public async Task<Product> GetById(Guid id)
        {
            var result = await _client.GetFromJsonAsync<Product>($"api/Product/{id}");
            return result;
        }

        public async Task<bool> Update(Guid id, Product product)
        {
            var result = await _client.PutAsJsonAsync($"api/Product/update/?id={id}", product);
            return result.IsSuccessStatusCode;
        }      
    }
}
