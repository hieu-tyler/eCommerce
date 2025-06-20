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
        public readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{productId}/{languageId}")]
        public async Task<IActionResult> GetById(int productId, string languageId)
        {
            var product = await _productService.GetProductById(productId, languageId);

            if (product == null)
            {
                return BadRequest("Cannot find the product");
            }

            return Ok(product);
        }

        // localhost:5000/api/products/paging?categoryId=1&pageIndex
        [HttpGet("paging")]
        public async Task<IActionResult> GetAllPaging([FromQuery] GetProductPagingRequest request)
        {
            var products = await _productService.GetAllPaging(request);

            return Ok(products);
        }

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> CreateImage(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var imageId = await _productService.AddImage(productId, request);

            if (imageId == 0) return BadRequest();

            var searchedImage = await _productService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, searchedImage);
        }

        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (imageId == 0) return BadRequest();

            var searchedImage = await _productService.GetImageById(imageId);

            return Ok();
        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            if (imageId == 0) return BadRequest();

            var searchedImage = await _productService.GetImageById(imageId);

            return Ok();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var productId = await _productService.Create(request);

            if (productId == 0) return BadRequest();

            var product = await _productService.GetProductById(productId, request.LanguageId);

            return CreatedAtAction(nameof(GetById), new { id = productId }, productId);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] ProductUpdateRequest request)
        {
            var affectedResults = await _productService.Update(request);

            if (affectedResults == 0) return BadRequest();

            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            var affectedResults = await _productService.Delete(productId);

            if (affectedResults == 0) return BadRequest();

            return Ok();
        }

        [HttpPatch("{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            var isSuccessful = await _productService.UpdatePrice(productId, newPrice);

            if (!isSuccessful) return BadRequest();

            return Ok();
        }


        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int productId, int imageId, [FromForm] ProductImageCreateRequest request)
        {

            var product = await _productService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetById), new { id = productId }, productId);
        }
    }
}
