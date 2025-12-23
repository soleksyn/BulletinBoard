using BulletinBoard.Data.DTOs;

namespace BulletinBoard.Data.Interfaces
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<IEnumerable<SubCategoryDto>> GetSubCategoriesAsync(int categoryId);
    }
}
