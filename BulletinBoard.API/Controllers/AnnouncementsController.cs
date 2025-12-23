using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using BulletinBoard.Core.Constants;
using BulletinBoard.Core.Interfaces;
using BulletinBoard.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = string.Format(ErrorConstants.InternalServerError, ex.Message), detail = ex.InnerException?.Message });
            }
        }

        [HttpGet("mine")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<AnnouncementModel>>> GetMyAnnouncements()
        {
            try
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
                if (userIdClaim == null)
                {
                    return Unauthorized();
                }

                if (!int.TryParse(userIdClaim.Value, out int userId))
                {
                    return BadRequest(string.Format(ErrorConstants.InvalidUserIdInToken, userIdClaim.Value));
                }

                var announcements = await _service.GetAnnouncementsByUserIdAsync(userId);
                return Ok(announcements ?? new List<AnnouncementModel>());
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = string.Format(ErrorConstants.InternalServerError, ex.Message) });
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
                return StatusCode((int)HttpStatusCode.InternalServerError, string.Format(ErrorConstants.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PostAnnouncement(AnnouncementModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ErrorConstants.InvalidModelState);
                }

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
                if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
                {
                    model.UserId = userId;
                }
                else
                {
                    return Unauthorized(string.Format(ErrorConstants.InvalidUserIdInToken, userIdClaim?.Value ?? "null"));
                }

                await _service.CreateAnnouncementAsync(model);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, string.Format(ErrorConstants.InternalServerError, ex.Message));
            }
        }

        [HttpPut("{id}")]
        [Authorize]
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

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) ?? User.FindFirst("sub");
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId) || existing.UserId != userId)
                {
                    return Forbid();
                }

                model.UserId = userId; // Ensure UserId is not changed

                await _service.UpdateAnnouncementAsync(model);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, string.Format(ErrorConstants.InternalServerError, ex.Message));
            }
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteAnnouncement(int id)
        {
            try
            {
                var existing = await _service.GetAnnouncementByIdAsync(id);
                if (existing == null)
                {
                    return NotFound(ErrorConstants.NotFound);
                }

                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
                if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId) || existing.UserId != userId)
                {
                    return Forbid();
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
