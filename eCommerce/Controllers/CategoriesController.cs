using Application.Catalog.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetAll(string languageId)
        {

            var categories = await _categoryService.GetAll(languageId);

            return Ok(categories);
        }
    }
}
