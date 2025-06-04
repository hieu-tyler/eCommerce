using Application.Catalog.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ViewModels.Catalog.ProductImages;
using ViewModels.Catalog.Products;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ProductsController : Controller
    {
        public readonly IPublicProductService _publicProductService;
        public readonly IManageProductService _manageProductService;

        public ProductsController(IPublicProductService publicProductService, IManageProductService manageProductService)
        {
            _publicProductService = publicProductService;
            _manageProductService = manageProductService;
        }

        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var product = await _manageProductService.GetProductById(productId, languageId);

            if (product == null)
            {
                return BadRequest("Cannot find the product");
            }

            return Ok(product);
        }

        // lhocalstart:5000/api/products/public-paging?categoryId=1&pageIndex
        [HttpGet("{languageId}")]
        public async Task<IActionResult> GetAllPaging([FromQuery]GetPublicProductPagingRequest request, string languageId)
        {
            var products = await _publicProductService.GetAllByCategoryId(request, languageId);

            return Ok(products);
        }

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var imageId = await _manageProductService.AddImage(productId, request);

            if (imageId == 0) return BadRequest();

            var searchedImage = await _manageProductService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, searchedImage);
        }

        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (imageId == 0) return BadRequest();

            var searchedImage = await _manageProductService.GetImageById(imageId);

            return Ok();
        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            if (imageId == 0) return BadRequest();

            var searchedImage = await _manageProductService.GetImageById(imageId);

            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var productId = await _manageProductService.Create(request);

            if (productId == 0) return BadRequest();

            var product = await _manageProductService.GetProductById(productId, request.LanguageId);

            return CreatedAtAction(nameof(GetById), new { id = productId }, productId);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            var affectedResults = await _manageProductService.Update(request);

            if (affectedResults == 0) return BadRequest();

            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResults = await _manageProductService.Delete(productId);

            if (affectedResults == 0) return BadRequest();

            return Ok();
        }

        [HttpPatch("{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var isSuccessful = await _manageProductService.UpdatePrice(productId, newPrice);

            if (!isSuccessful) return BadRequest();

            return Ok();
        }


        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int productId, int imageId, [FromForm] ProductImageCreateRequest request)
        {

            var product = await _manageProductService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetById), new { id = productId }, productId);
        }
    }
}
