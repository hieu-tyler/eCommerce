using ECommerce.ECommerce.Application.Catalog.Products;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.ECommerce.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        public readonly IPublicProductService _publicProductService;

        public ProductController(IPublicProductService publicProductService)
        {
            _publicProductService = publicProductService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _publicProductService.GetAll();

            return Ok(products);
        }

    }
}
