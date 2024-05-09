using BlazorEcommerce.Server.Data;
using BlazorEcommerce.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorEcommerce.Server.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _dataContext;

        public CategoryService(DataContext context)
        {
            _dataContext = context;
        }

        public async Task<ServiceResponse<List<Category>>> GetCategories()
        {
            var categories = await _dataContext.Categories.ToListAsync();
            return new ServiceResponse<List<Category>> { Data = categories };
        }
    }
}
