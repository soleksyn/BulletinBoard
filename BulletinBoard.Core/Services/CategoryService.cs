using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulletinBoard.Core.Interfaces;
using BulletinBoard.Core.Models;
using BulletinBoard.Data.Interfaces;

namespace BulletinBoard.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync()
        {
            var dtos = await _repository.GetAllCategoriesAsync();
            return dtos?.Select(d => new CategoryModel { Id = d.Id, Name = d.Name }) ?? Enumerable.Empty<CategoryModel>();
        }

        public async Task<IEnumerable<SubCategoryModel>> GetSubCategoriesAsync(int categoryId)
        {
            var dtos = await _repository.GetSubCategoriesAsync(categoryId);
            return dtos?.Select(d => new SubCategoryModel { Id = d.Id, CategoryId = d.CategoryId, Name = d.Name }) ?? Enumerable.Empty<SubCategoryModel>();
        }
    }
}
