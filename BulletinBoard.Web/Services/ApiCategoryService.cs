using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BulletinBoard.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulletinBoard.Web.Services
{
    public class ApiCategoryService : IApiCategoryService
    {
        private readonly HttpClient _httpClient;

        public ApiCategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<SelectListItem>> GetCategoriesAsync()
        {
            var categories = await _httpClient.GetFromJsonAsync<IEnumerable<CategoryApiModel>>("categories");
            return categories?.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Name }) ?? Enumerable.Empty<SelectListItem>();
        }

        public async Task<IEnumerable<SelectListItem>> GetSubCategoriesAsync(int? categoryId)
        {
            if (categoryId == null || categoryId <= 0)
            {
                return Enumerable.Empty<SelectListItem>();
            }

            var subCategories = await _httpClient.GetFromJsonAsync<IEnumerable<SubCategoryApiModel>>($"categories/{categoryId}/subcategories");
            return subCategories?.Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }) ?? Enumerable.Empty<SelectListItem>();
        }
    }
}
