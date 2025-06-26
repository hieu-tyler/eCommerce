using AdminApp.Services.Category;
using AdminApp.Services.Product;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Utilities.SystemConstants.cs;
using ViewModels.Catalog.Categories;
using ViewModels.Catalog.Products;
using ViewModels.Common;

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
            var selectedCategories = categories.ResultObject.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.Name,
                Selected = categoryId.HasValue && categoryId.Value == x.Id
            });
            ViewBag.Categories = selectedCategories;
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMessage = TempData["result"];
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

        [HttpGet]
        public async Task<IActionResult> CategoryAssign(int id)
        {
            var roleAssignRequest = await this.GetCategoryAssignRequest(id);
            return View(roleAssignRequest);
        }

        [HttpPost]
        public async Task<IActionResult> CategoryAssign(CategoryAssignRequest request)
        {
            if (!ModelState.IsValid)
                return View(request);

            var result = await _productApiClient.CategoryAssign(request.Id, request);
            if (result.IsSuccess)
            {
                TempData["result"] = "Assign roles successfully!";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }

        private async Task<CategoryAssignRequest> GetCategoryAssignRequest(int id)
        {
            var languageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            var productObj = await _productApiClient.GetById(id, languageId);
            var categoryInProduct = productObj.ResultObject.Categories;
            var categoryList = await _categoryApiClient.GetAll(languageId);
            var categoryAssignRequest = new CategoryAssignRequest();
            foreach (var category in categoryList.ResultObject)
            {

                categoryAssignRequest.Categories.Add(new SelectItem()
                {
                    Id = category.Id.ToString(),
                    Name = category.Name,
                    Selected = categoryInProduct.Contains(category.Name)
                });
            }
            return categoryAssignRequest;
        }
    }
}
