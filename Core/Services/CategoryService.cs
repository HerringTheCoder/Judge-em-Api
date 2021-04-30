using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Requests;
using Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Storage.Repositories.Interfaces;
using Storage.Tables;

namespace Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<List<Category>> GetCategoriesByGameId(int gameId)
        {
            var categories = await _categoryRepository.GetByFilterAsync(c => c.GameId == gameId);

            return categories;
        }

        public async Task<Category> CreateCategory(CategoryCreateRequest request)
        {
            var category = new Category
            {
                Name = request.Name,
                Weight = request.Weight
            };
            await _categoryRepository.AddAsync(category);
            return category;
        }

        public async Task<List<Category>> CreateCategories(List<CategoryCreateRequest> categoryRequests)
        {
            var categories = new List<Category>();
            foreach (var categoryRequest in categoryRequests)
            {
                var category = new Category
                {
                    Name = categoryRequest.Name,
                    Weight = categoryRequest.Weight,
                    GameId = categoryRequest.GameId
                };
                categories.Add(category);
            }

            return await _categoryRepository.AddRangeAsync(categories);
        }

        public async Task<List<Category>> UpdateCategories(List<CategoryUpdateRequest> categoryRequests)
        {
            var categoryIds = categoryRequests.Select(cr => cr.Id);
            var categories = await _categoryRepository.GetByFilterAsync(c => categoryIds.Contains(c.Id));
            foreach (var category in categories)
            {
                var categoryRequest = categoryRequests.Find(cr => cr.Id == category.Id);
                if (categoryRequest != null)
                {
                    category.Name = categoryRequest.Name;
                    category.Weight = categoryRequest.Weight;
                    category.GameId = categoryRequest.GameId;
                }
            }

            return await _categoryRepository.UpdateRangeAsync(categories);
        }

        public async Task DeleteCategory(int id)
        {
            var category = await _categoryRepository.GetFirstByFilterAsync(c => c.Id == id);
            await _categoryRepository.DeleteAsync(category);
        }
    }
}
