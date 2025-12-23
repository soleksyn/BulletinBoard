using System.Collections.Generic;
using System.Threading.Tasks;

namespace BulletinBoard.Data
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<CategoryDto>> GetAllCategoriesAsync();
        Task<IEnumerable<SubCategoryDto>> GetSubCategoriesAsync(int categoryId);
    }
}
