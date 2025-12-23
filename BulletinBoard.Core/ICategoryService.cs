using System.Collections.Generic;
using System.Threading.Tasks;

namespace BulletinBoard.Core
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync();
        Task<IEnumerable<SubCategoryModel>> GetSubCategoriesAsync(int categoryId);
    }
}
