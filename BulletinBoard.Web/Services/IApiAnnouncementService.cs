using System.Collections.Generic;
using System.Threading.Tasks;
using BulletinBoard.Web.Models;

namespace BulletinBoard.Web.Services
{
    public interface IApiAnnouncementService
    {
        Task<IEnumerable<AnnouncementViewModel>> GetAllAsync(int? categoryId = null, int? subCategoryId = null);
        Task<IEnumerable<AnnouncementViewModel>> GetMyAnnouncementsAsync();
        Task<AnnouncementViewModel> GetByIdAsync(int id);
        Task CreateAsync(AnnouncementViewModel model);
        Task UpdateAsync(AnnouncementViewModel model);
        Task DeleteAsync(int id);
    }
}
