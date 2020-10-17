using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Core.Requests;
using Core.Services;
using Core.Services.Interfaces;
using Storage.Repositories.Interfaces;
using Storage.Tables;
using Testing.Mocks;
using Xunit;

namespace Testing.Unit
{
    public class CategoryServiceTest
    {
        private const int ObjectsCount = 5;
        private readonly CategoryRepositoryMock _categoryRepositoryMock;
        private readonly ICategoryService _categoryService;

        //Called before each test
        public CategoryServiceTest()
        {
            _categoryRepositoryMock = new CategoryRepositoryMock(ObjectsCount);
            _categoryService = new CategoryService(_categoryRepositoryMock);
        }

        [Fact]
        public async Task TestIfCategoryIsCreated()
        {
            var categoryRequest = new CategoryCreateRequest
            {
                Name = "TestCategoryName",
                Weight = 5
            };
            await _categoryService.CreateCategory(categoryRequest);
            var category = _categoryRepositoryMock.Categories.Last();
            Assert.Equal(categoryRequest.Name, category.Name);
            Assert.Equal(categoryRequest.Weight, category.Weight);
        }

        [Fact]
        public async Task TestIfCategoryIsDeleted()
        {
            await _categoryService.DeleteCategory(5);
            Assert.Equal(ObjectsCount - 1, _categoryRepositoryMock.Categories.Count);
        }

        [Fact]
        public void TestIfCategoriesAreRetrievable()
        {
            var categories = new List<Category>();
            for (int i = 1; i <= ObjectsCount; i++)
                categories.Add(_categoryService.GetCategory(i));

            Assert.Equal(ObjectsCount, categories.Count);
            Assert.IsType<List<Category>>(categories);
        }
    }
}
