using System.Collections.Generic;
using System.Threading.Tasks;

using BulletinBoard.Core.Models;

namespace BulletinBoard.Core.Interfaces
{
    public interface IAnnouncementService
    {
        Task CreateAnnouncementAsync(AnnouncementModel model);
        Task<IEnumerable<AnnouncementModel>> GetAllAnnouncementsAsync(int? categoryId = null, int? subCategoryId = null);
        Task<IEnumerable<AnnouncementModel>> GetAnnouncementsByUserIdAsync(int userId);
        Task<AnnouncementModel> GetAnnouncementByIdAsync(int id);
        Task UpdateAnnouncementAsync(AnnouncementModel model);
        Task DeleteAnnouncementAsync(int id);
    }
}
