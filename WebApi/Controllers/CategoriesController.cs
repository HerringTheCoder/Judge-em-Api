using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Requests;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Storage.Tables;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<List<Category>> CreateCategories([FromBody] List<CategoryCreateRequest> requests)
        {
            var categories = await _categoryService.CreateCategories(requests);
            return categories;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<List<Category>> UpdateCategories([FromBody] List<CategoryUpdateRequest> requests)
        {
            var categories = await _categoryService.UpdateCategories(requests);
            return categories;
        }
    }
}
