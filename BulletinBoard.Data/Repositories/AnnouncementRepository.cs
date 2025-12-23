using System.Collections.Generic;
using System.Threading.Tasks;
using BulletinBoard.Data.DTOs;
using BulletinBoard.Data.Interfaces;

namespace BulletinBoard.Data.Repositories
{
    public class AnnouncementRepository : DapperRepositoryBase, IAnnouncementRepository
    {
        public AnnouncementRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task CreateAsync(AnnouncementDto announcement)
        {
            var parameters = new
            {
                Title = announcement.Title,
                Description = announcement.Description,
                IsActive = announcement.IsActive,
                CategoryId = announcement.CategoryId,
                SubCategoryId = announcement.SubCategoryId,
                Price = announcement.Price
            };

            await ExecuteAsync("[dbo].[sp_CreateAnnouncement]", parameters);
        }

        public async Task<IEnumerable<AnnouncementDto>> GetAllAsync(int? categoryId = null, int? subCategoryId = null)
        {
            var parameters = new
            {
                CategoryId = categoryId,
                SubCategoryId = subCategoryId
            };

            return await GetCollectionAsync<AnnouncementDto>("[dbo].[sp_GetAllAnnouncements]", parameters);
        }

        public async Task<AnnouncementDto> GetByIdAsync(int id)
        {
            var parameters = new { Id = id };
            return await GetObjectAsync<AnnouncementDto>("[dbo].[sp_GetAnnouncementById]", parameters);
        }

        public async Task UpdateAsync(AnnouncementDto announcement)
        {
            var parameters = new
            {
                Id = announcement.Id,
                Title = announcement.Title,
                Description = announcement.Description,
                IsActive = announcement.IsActive,
                CategoryId = announcement.CategoryId,
                SubCategoryId = announcement.SubCategoryId,
                Price = announcement.Price
            };

            await ExecuteAsync("[dbo].[sp_UpdateAnnouncement]", parameters);
        }

        public async Task DeleteAsync(int id)
        {
            var parameters = new { Id = id };
            await ExecuteAsync("[dbo].[sp_DeleteAnnouncement]", parameters);
        }
    }
}
