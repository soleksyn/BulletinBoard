using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using BulletinBoard.Web.Models;
using Microsoft.Extensions.Options;

namespace BulletinBoard.Web.Services
{
    public class ApiAnnouncementService : IApiAnnouncementService
    {
        private readonly HttpClient _httpClient;
        private readonly ApiSettings _settings;

        public ApiAnnouncementService(HttpClient httpClient, IOptions<ApiSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings.Value;
        }

        public async Task<IEnumerable<AnnouncementViewModel>> GetAllAsync(int? categoryId = null, int? subCategoryId = null)
        {
            var queryParams = new List<string>();
            if (categoryId.HasValue && categoryId > 0) queryParams.Add($"categoryId={categoryId}");
            if (subCategoryId.HasValue && subCategoryId > 0) queryParams.Add($"subCategoryId={subCategoryId}");
            
            var url = "announcements";
            if (queryParams.Count > 0)
            {
                url += "?" + string.Join("&", queryParams);
            }
            
            return await _httpClient.GetFromJsonAsync<IEnumerable<AnnouncementViewModel>>(url) ?? new List<AnnouncementViewModel>();
        }

        public async Task<AnnouncementViewModel> GetByIdAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<AnnouncementViewModel>($"announcements/{id}") ?? new AnnouncementViewModel();
        }

        public async Task CreateAsync(AnnouncementViewModel model)
        {
            await _httpClient.PostAsJsonAsync("announcements", model);
        }

        public async Task UpdateAsync(AnnouncementViewModel model)
        {
            await _httpClient.PutAsJsonAsync($"announcements/{model.Id}", model);
        }

        public async Task DeleteAsync(int id)
        {
            await _httpClient.DeleteAsync($"announcements/{id}");
        }
    }
}
