using Assignment.Application.DataTransferObj.CategoryDto;
using Assignment.Application.DataTransferObj.ProductDto;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Web.Services.IServices;

namespace Web.Components.Pages
{
    public partial class Home : ComponentBase
    {
        [Inject] private IProductService ProductService { get; set; }
        [Inject] private ICategoryService CategoryService { get; set; }
        private ProductSearch ProductSearch = new ProductSearch();
        private IEnumerable<ProductDto> Products = new List<ProductDto>();
        private IEnumerable<CategoryDto> Category = new List<CategoryDto>();
        protected override async Task OnInitializedAsync()
        {
            await GetAll();
        }
        private async Task GetAll()
        {
            Products = await ProductService.GetAll(ProductSearch);
            Category = await CategoryService.GetAll();
            foreach (var item in Products)
            {
                item.Image = await GetProductImageUrl(item.Id);
            }
        }

        private async Task<string> GetProductImageUrl(Guid productId)
        {
            return $"https://localhost:7078/api/Product/image?id={productId}";
        }

        private async Task Search()
        {
            await GetAll();
        }
        private async Task SearchByCategory(Guid categoryId)
        {
            if (categoryId == Guid.Empty)
            {
                ProductSearch.CategoryId = null;
            }
            else
            {
                ProductSearch.CategoryId = categoryId;
            }
            await GetAll();
        }
    }
}
