using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulletinBoard.Web.Services
{
    public interface IApiCategoryService
    {
        Task<IEnumerable<SelectListItem>> GetCategoriesAsync();
        Task<IEnumerable<SelectListItem>> GetSubCategoriesAsync(int? categoryId);
    }
}
