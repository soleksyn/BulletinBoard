using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BulletinBoard.Data;

namespace BulletinBoard.Core
{
    public class AnnouncementService : IAnnouncementService
    {
        private readonly IAnnouncementRepository _repository;

        public AnnouncementService(IAnnouncementRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAnnouncementAsync(AnnouncementModel model)
        {
            var mappedDto = MapToDto(model);
            await _repository.CreateAsync(mappedDto);
        }

        public async Task<IEnumerable<AnnouncementModel>> GetAllAnnouncementsAsync(int? categoryId = null, int? subCategoryId = null)
        {
            var dtos = await _repository.GetAllAsync(categoryId, subCategoryId);
            return dtos?.Select(MapToModel) ?? Enumerable.Empty<AnnouncementModel>();
        }

        public async Task<AnnouncementModel> GetAnnouncementByIdAsync(int id)
        {
            var dto = await _repository.GetByIdAsync(id);
            return dto != null ? MapToModel(dto) : null;
        }

        public async Task UpdateAnnouncementAsync(AnnouncementModel model)
        {
            var mappedDto = MapToDto(model);
            await _repository.UpdateAsync(mappedDto);
        }

        public async Task DeleteAnnouncementAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        private AnnouncementDto MapToDto(AnnouncementModel model)
        {
            return new AnnouncementDto
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                CreatedDate = model.CreatedDate ?? DateTime.Now,
                IsActive = model.IsActive,
                CategoryId = model.CategoryId,
                SubCategoryId = model.SubCategoryId,
                Price = model.Price
            };
        }

        private AnnouncementModel MapToModel(AnnouncementDto dto)
        {
            return new AnnouncementModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Description = dto.Description,
                CreatedDate = dto.CreatedDate ?? DateTime.Now,
                IsActive = dto.IsActive ?? true,
                CategoryId = dto.CategoryId,
                SubCategoryId = dto.SubCategoryId,
                Price = dto.Price,
                CategoryName = dto.CategoryName ?? "Unknown",
                SubCategoryName = dto.SubCategoryName ?? "Unknown"
            };
        }
    }
}
