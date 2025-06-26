using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewModels.Catalog.Categories;
using ViewModels.Catalog.Products;
using ViewModels.Common;
using ViewModels.System.Users;

namespace AdminApp.Services.Product
{
    public interface IProductApiClient
    {
        Task<PageResult<ProductViewModel>> GetPaging(GetProductPagingRequest request);

        Task<bool> CreateProduct(ProductCreateRequest request);

        Task<ApiResult<ProductViewModel>> GetById(int id, string languageId);

        Task<ApiResult<bool>> CategoryAssign(int id, CategoryAssignRequest request);
    }
}
