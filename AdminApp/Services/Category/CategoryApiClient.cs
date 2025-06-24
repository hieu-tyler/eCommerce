using ViewModels.Catalog.Categories;
using ViewModels.Common;

namespace AdminApp.Services.Category
{
    public class CategoryApiClient : BaseApiClient, ICategoryApiClient
    {
        public CategoryApiClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
            : base(httpClient, httpContextAccessor, configuration)
        {
        }

        public async Task<ApiResult<List<CategoryViewModel>>> GetAll(string languageId)
        {
            var categories = await GetListAsync<CategoryViewModel>($"/api/categories/{languageId}");

            if (categories == null)
            {
                return new ApiErrorResult<List<CategoryViewModel>>("Failed to retrieve categories.");
            }

            return new ApiSuccessResult<List<CategoryViewModel>>(categories);
        }
    }
}
