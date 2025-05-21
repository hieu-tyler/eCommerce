using ECommerce.ECommerce.Application.Catalog.Products;
using ECommerce.ECommerce.Application.Catalog.Products.Manage;
using ECommerce.ECommerce.ViewModels.Catalog.Products;
using ECommerce.ViewModels.Catalog.Products.Public;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.ECommerce.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        public readonly IPublicProductService _publicProductService;
        public readonly IManageProductService _manageProductService;

        public ProductController(IPublicProductService publicProductService, IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }

        [HttpGet("{languageId}")]
        public async Task<IActionResult> Get(string languageId)
        {
            var products = await _publicProductService.GetAll(languageId);

            return Ok(products);
        }

        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var product = await _manageProductService.GetProductById (productId, languageId);

            if (product == null)
            {
                return BadRequest("Cannot find the product");
            }

            return Ok(product);
        }

        [HttpGet("public-paging")]
        public async Task<IActionResult> Get([FromQuery]GetPublicProductPagingRequest request)
        {
            var products = await _publicProductService.GetAllByCategoryId(request);

            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            var productId = await _manageProductService.Create(request);

            if (productId == 0)
            {
                return BadRequest();
            }

            var product = await _manageProductService.GetProductById(productId, request.LanguageId);

            return CreatedAtAction(nameof(GetById), new {id = productId}, productId);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            var affectedResults = await _manageProductService.Update(request);

            if (affectedResults == 0)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResults = await _manageProductService.Delete(productId);

            if (affectedResults == 0) return BadRequest();

            return Ok();
        }

        [HttpPut("price/{newPrice}")]
        public async Task<IActionResult> UpdatePrice([FromQuery] int id, decimal newPrice)
        {
            var isSuccessful = await _manageProductService.UpdatePrice(id, newPrice);

            if (!isSuccessful)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}
