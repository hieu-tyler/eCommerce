using Azure;
using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Utilities.SystemConstants.cs;
using ViewModels.Catalog.Categories;
using ViewModels.Catalog.Products;
using ViewModels.Common;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace AdminApp.Services.Product
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        public ProductApiClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
            : base(httpClient, httpContextAccessor, configuration)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
        }

        public async Task<bool> CreateProduct(ProductCreateRequest request)
        {
            var tokens = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens);

            var requestContent = new MultipartFormDataContent();

            if (request.ThumbnailImage != null)
            {
                byte[] data;
                using (var br = new BinaryReader(request.ThumbnailImage.OpenReadStream()))
                {
                    data = br.ReadBytes((int)request.ThumbnailImage.OpenReadStream().Length);
                }
                ByteArrayContent bytes = new ByteArrayContent(data);
                requestContent.Add(bytes, "thumbnailImage", request.ThumbnailImage.FileName);
            }

            requestContent.Add(new StringContent(request.Price.ToString()), "price");
            requestContent.Add(new StringContent(request.OriginalPrice.ToString()), "originalPrice");
            requestContent.Add(new StringContent(request.Stock.ToString()), "stock");
            requestContent.Add(new StringContent(request.Name.ToString()), "name");
            requestContent.Add(new StringContent(request.Description.ToString()), "description");

            requestContent.Add(new StringContent(request.Details.ToString()), "details");
            requestContent.Add(new StringContent(request.SeoDescription.ToString()), "seoDescription");
            requestContent.Add(new StringContent(request.SeoTitle.ToString()), "seoTitle");
            requestContent.Add(new StringContent(request.SeoAlias.ToString()), "seoAlias");
            requestContent.Add(new StringContent(request.LanguageId != null ? request.LanguageId : languageId), "languageId");

            var response = await _httpClient.PostAsync($"/api/products/", requestContent);
            return response.IsSuccessStatusCode;
        }

        public async Task<ApiResult<ProductViewModel>> GetById(int id, string languageId)
        {
            var result = await GetAsync<ProductViewModel>($"/api/products/{id}/{languageId}");

            if (result == null)
                return new ApiErrorResult<ProductViewModel>($"Couldn't find the product with id {id}");
            else
                return new ApiSuccessResult<ProductViewModel>(result);
        }

        public async Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request)
        {
            var tokens = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.Token);
            var languageId = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokens);

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var jsonObject = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(jsonObject, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/products/{id}/categories", httpContent);

            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonSerializer.Deserialize<ApiSuccessResult<bool>>(body, options);

            return JsonSerializer.Deserialize<ApiErrorResult<bool>>(body, options);
        }

        public async Task<PageResult<ProductViewModel>> GetPaging(GetProductPagingRequest request)
        {
            var data = await GetAsync<PageResult<ProductViewModel>>(
                $"/api/products/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}&categoryId={request.CategoryId}" +
                $"&keyword={request.Keyword}&languageId={request.LanguageId}");

            return data;
        }
    }
}
