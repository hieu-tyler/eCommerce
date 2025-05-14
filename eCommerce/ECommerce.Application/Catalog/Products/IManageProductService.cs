using ECommerce.ViewModels.Catalog.Products;
using ECommerce.ViewModels.Catalog.Products.Manage;
using ECommerce.ViewModels.Common;

namespace ECommerce.ECommerce.Application.Catalog.Products
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<int> Delete(int productId);

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task<bool> UpdateStock(int productId, int addedQuantity);
        Task AddViewCount(int productId);
        Task<PageResults<ProductViewModel>> GetAllPaging(GetProductPagingRequest request);
    }
}
