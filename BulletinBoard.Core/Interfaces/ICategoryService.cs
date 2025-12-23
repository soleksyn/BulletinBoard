using System.Collections.Generic;
using System.Threading.Tasks;

using BulletinBoard.Core.Models;

namespace BulletinBoard.Core.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryModel>> GetAllCategoriesAsync();
        Task<IEnumerable<SubCategoryModel>> GetSubCategoriesAsync(int categoryId);
    }
}
