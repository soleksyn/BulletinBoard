using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BulletinBoard.Core.Constants;
using BulletinBoard.Core.Interfaces;
using BulletinBoard.Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnnouncementsController : ControllerBase
    {
        private readonly IAnnouncementService _service;

        public AnnouncementsController(IAnnouncementService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<AnnouncementModel>>> GetAnnouncements([FromQuery] int? categoryId = null, [FromQuery] int? subCategoryId = null)
        {
            try
            {
                int? filterCategoryId = (categoryId.HasValue && categoryId.Value > 0) ? categoryId.Value : (int?)null;
                int? filterSubCategoryId = (subCategoryId.HasValue && subCategoryId.Value > 0) ? subCategoryId.Value : (int?)null;

                var announcements = await _service.GetAllAnnouncementsAsync(filterCategoryId, filterSubCategoryId);
                return Ok(announcements ?? new List<AnnouncementModel>());
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message, detail = ex.InnerException?.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AnnouncementModel>> GetAnnouncement(int id)
        {
            try
            {
                var announcement = await _service.GetAnnouncementByIdAsync(id);
                if (announcement == null)
                {
                    return NotFound(ErrorConstants.NotFound);
                }
                return Ok(announcement);
            }
            catch (Exception ex)
            {
                return StatusCode(500, string.Format(ErrorConstants.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        public async Task<ActionResult> PostAnnouncement(AnnouncementModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorConstants.InvalidModelState);
                }

                await _service.CreateAnnouncementAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, string.Format(ErrorConstants.InternalServerError, ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAnnouncement(int id, AnnouncementModel model)
        {
            try
            {
                if (id != model.Id)
                {
                    return BadRequest(ErrorConstants.IdMismatch);
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorConstants.InvalidModelState);
                }

                var existing = await _service.GetAnnouncementByIdAsync(id);
                if (existing == null)
                {
                    return NotFound(ErrorConstants.NotFound);
                }

                await _service.UpdateAnnouncementAsync(model);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, string.Format(ErrorConstants.InternalServerError, ex.Message));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            try
            {
                var existing = await _service.GetAnnouncementByIdAsync(id);
                if (existing == null)
                {
                    return NotFound(ErrorConstants.NotFound);
                }

                await _service.DeleteAnnouncementAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, string.Format(ErrorConstants.InternalServerError, ex.Message));
            }
        }
    }
}
