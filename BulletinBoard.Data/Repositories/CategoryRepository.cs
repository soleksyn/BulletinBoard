using BulletinBoard.Data.DTOs;
using BulletinBoard.Data.Interfaces;

namespace BulletinBoard.Data.Repositories
{
    public class CategoryRepository : DapperRepositoryBase, ICategoryRepository
    {
        public CategoryRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync()
        {
            return await GetCollectionAsync<CategoryDto>("[dbo].[sp_GetAllCategories]");
        }

        public async Task<IEnumerable<SubCategoryDto>> GetSubCategoriesAsync(int categoryId)
        {
            var parameters = new { CategoryId = categoryId };
            return await GetCollectionAsync<SubCategoryDto>("[dbo].[sp_GetSubCategoriesByCategoryId]", parameters);
        }
    }
}
