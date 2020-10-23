using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Requests;
using Storage.Tables;

namespace Core.Services.Interfaces
{
    public interface ICategoryService
    {
        Category GetCategory(int id);
        Task<Category> CreateCategory(CategoryCreateRequest request);
        Task<List<Category>> CreateCategories(List<CategoryCreateRequest> requests);
        Task<List<Category>> UpdateCategories(List<CategoryUpdateRequest> requests);
        Task DeleteCategory(int id);
    }
}
