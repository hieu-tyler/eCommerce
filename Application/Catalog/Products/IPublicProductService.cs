using ECommerce.ViewModels.Catalog.Products;
using ECommerce.ViewModels.Catalog.Products.Public;
using ECommerce.ViewModels.Common;

namespace ECommerce.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        public Task<PageResults<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request, string languageId);

        //public Task<List<ProductViewModel>> GetAll(string languageId);

    }
}
