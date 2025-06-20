using ViewModels.Catalog.Products;
using ViewModels.Common;

namespace AdminApp.Services.Product
{
    public class ProductApiClient : BaseApiClient, IProductApiClient
    {
        public ProductApiClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) 
            : base(httpClient, httpContextAccessor, configuration)
        {
        }

        public async Task<PageResult<ProductViewModel>> GetPaging(GetProductPagingRequest request)
        {
            var data = await GetAsync<PageResult<ProductViewModel>>(
                $"/api/products/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}&languageId={request.LanguageId}");

            return data;
        }
    }
}
