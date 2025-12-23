using System.Collections.Generic;
using System.Threading.Tasks;
using BulletinBoard.Core;
using Microsoft.AspNetCore.Mvc;

namespace BulletinBoard.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategories()
        {
            try
            {
                var categories = await _service.GetAllCategoriesAsync();
                return Ok(categories);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{categoryId}/subcategories")]
        public async Task<ActionResult<IEnumerable<SubCategoryModel>>> GetSubCategories(int categoryId)
        {
            try
            {
                var subCategories = await _service.GetSubCategoriesAsync(categoryId);
                return Ok(subCategories);
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
