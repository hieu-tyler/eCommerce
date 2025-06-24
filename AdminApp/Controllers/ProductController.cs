using AdminApp.Services.Category;
using AdminApp.Services.Product;
using Microsoft.AspNetCore.Mvc;
using Utilities.SystemConstants.cs;
using ViewModels.Catalog.Products;

namespace AdminApp.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductApiClient _productApiClient;
        private readonly ICategoryApiClient _categoryApiClient;
        private readonly IConfiguration _configuration;

        public ProductController(IProductApiClient productApiClient, ICategoryApiClient categoryApiClient, IConfiguration configuration)
        {
            _categoryApiClient = categoryApiClient;
            _configuration = configuration;
            _productApiClient = productApiClient;
        }

        public async Task<IActionResult> Index(string keyword, int? categoryId, int pageIndex = 1, int pageSize = 10)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);

            var request = new GetProductPagingRequest()
            {
                Keyword = keyword,
                PageIndex = pageIndex,
                PageSize = pageSize,
                LanguageId = languageId,
                CategoryId = categoryId.HasValue ? categoryId.Value : 0
            };
            var categories = await _categoryApiClient.GetAll(languageId);

            var data = await _productApiClient.GetPaging(request);
            ViewBag.Keyword = keyword;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(data);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _productApiClient.CreateProduct(request);
            if (result)
            {
                TempData["result"] = "Add product successfully";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", "Failed to add product");
            return View(request);
        }
    }
}
