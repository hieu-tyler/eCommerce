using Microsoft.AspNetCore.Mvc.RazorPages;
using ViewModels.Catalog.Products;
using ViewModels.Common;

namespace AdminApp.Services.Product
{
    public interface IProductApiClient
    {
        Task<PageResult<ProductViewModel>> GetPaging(GetProductPagingRequest request);
    }
}
