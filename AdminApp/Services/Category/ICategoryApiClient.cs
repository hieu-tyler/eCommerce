using ViewModels.Catalog.Categories;
using ViewModels.Common;

namespace AdminApp.Services.Category
{
    public interface ICategoryApiClient
    {
        Task<ApiResult<List<CategoryViewModel>>> GetAll(string langauageId);

    }
}
