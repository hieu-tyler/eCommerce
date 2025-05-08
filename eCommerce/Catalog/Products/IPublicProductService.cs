using ECommerce.Catalog.Products.DTOs;
using ECommerce.Catalog.Products.DTOs.Public;
using ECommerce.DTOs;

namespace ECommerce.Catalog.Products
{
    public interface IPublicProductService
    {
        public Task<PageResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request);

    }
}
