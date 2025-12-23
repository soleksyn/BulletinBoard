using System.Collections.Generic;
using System.Threading.Tasks;

namespace BulletinBoard.Core
{
    public interface IAnnouncementService
    {
        Task CreateAnnouncementAsync(AnnouncementModel model);
        Task<IEnumerable<AnnouncementModel>> GetAllAnnouncementsAsync(int? categoryId = null, int? subCategoryId = null);
        Task<AnnouncementModel> GetAnnouncementByIdAsync(int id);
        Task UpdateAnnouncementAsync(AnnouncementModel model);
        Task DeleteAnnouncementAsync(int id);
    }
}
