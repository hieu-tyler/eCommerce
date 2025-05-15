using ECommerce.ViewModels.Catalog.Products;
using ECommerce.ViewModels.Catalog.Products.Public;
using ECommerce.ViewModels.Common;

namespace ECommerce.ECommerce.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        public Task<PageResults<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request);

        public Task<List<ProductViewModel>> GetAll();

    }
}
