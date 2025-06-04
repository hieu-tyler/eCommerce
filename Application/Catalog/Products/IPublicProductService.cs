using ViewModels.Catalog.Products;
using ViewModels.Common;

namespace Application.Catalog.Products
{
    public interface IPublicProductService
    {
        public Task<PageResults<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request, string languageId);

        //public Task<List<ProductViewModel>> GetAll(string languageId);

    }
}
