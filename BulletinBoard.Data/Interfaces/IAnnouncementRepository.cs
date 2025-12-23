using System.Collections.Generic;
using System.Threading.Tasks;

using BulletinBoard.Data.DTOs;

namespace BulletinBoard.Data.Interfaces
{
    public interface IAnnouncementRepository
    {
        Task CreateAsync(AnnouncementDto announcement);
        Task<IEnumerable<AnnouncementDto>> GetAllAsync(int? categoryId = null, int? subCategoryId = null);
        Task<AnnouncementDto> GetByIdAsync(int id);
        Task UpdateAsync(AnnouncementDto announcement);
        Task DeleteAsync(int id);
    }
}
