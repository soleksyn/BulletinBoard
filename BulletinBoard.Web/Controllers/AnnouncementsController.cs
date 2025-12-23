using BulletinBoard.Web.Models;
using BulletinBoard.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulletinBoard.Web.Controllers
{
    public class AnnouncementsController : Controller
    {
        private readonly IApiAnnouncementService _apiService;
        private readonly IApiCategoryService _categoryService;

        public AnnouncementsController(IApiAnnouncementService apiService, IApiCategoryService categoryService)
        {
            _apiService = apiService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index(int? categoryId = null, int? subCategoryId = null)
        {
            var announcements = await _apiService.GetAllAsync(categoryId, subCategoryId);
            
            var viewModel = new AnnouncementListViewModel
            {
                Announcements = announcements,
                SelectedCategoryId = categoryId,
                SelectedSubCategoryId = subCategoryId,
                Categories = await _categoryService.GetCategoriesAsync(),
                SubCategories = categoryId.HasValue 
                    ? await _categoryService.GetSubCategoriesAsync(categoryId.Value) 
                    : new List<SelectListItem>()
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Filter(int? categoryId = null, int? subCategoryId = null)
        {
            var announcements = await _apiService.GetAllAsync(categoryId, subCategoryId);
            return PartialView("_AnnouncementList", announcements);
        }

        public async Task<IActionResult> Details(int id)
        {
            var viewModel = await _apiService.GetByIdAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new AnnouncementViewModel
            {
                Categories = await _categoryService.GetCategoriesAsync(),
                SubCategories = new List<SelectListItem>(),
                IsActive = true
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnnouncementViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                await _apiService.CreateAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }

            viewModel.Categories = await _categoryService.GetCategoriesAsync();
            if (viewModel.CategoryId > 0)
            {
                viewModel.SubCategories = await _categoryService.GetSubCategoriesAsync(viewModel.CategoryId);
            }
            else
            {
                viewModel.SubCategories = new List<SelectListItem>();
            }
            return View(viewModel);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await _apiService.GetByIdAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            viewModel.Categories = await _categoryService.GetCategoriesAsync();
            viewModel.SubCategories = await _categoryService.GetSubCategoriesAsync(viewModel.CategoryId);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AnnouncementViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                await _apiService.UpdateAsync(viewModel);
                return RedirectToAction(nameof(Index));
            }

            viewModel.Categories = await _categoryService.GetCategoriesAsync();
            viewModel.SubCategories = await _categoryService.GetSubCategoriesAsync(viewModel.CategoryId);
            return View(viewModel);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var viewModel = await _apiService.GetByIdAsync(id);
            if (viewModel == null)
            {
                return NotFound();
            }

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _apiService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> GetSubCategories(int categoryId)
        {
            var subCategories = await _categoryService.GetSubCategoriesAsync(categoryId);
            return Json(subCategories);
        }
    }
}
