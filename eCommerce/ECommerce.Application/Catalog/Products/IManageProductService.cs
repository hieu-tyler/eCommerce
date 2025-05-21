using ECommerce.ECommerce.ViewModels.Catalog.Products;
using ECommerce.ViewModels.Catalog.Products;
using ECommerce.ViewModels.Common;

namespace ECommerce.ECommerce.Application.Catalog.Products.Manage
{
    public interface IManageProductService
    {
        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<int> Delete(int productId);

        Task<ProductViewModel> GetProductById(int productId, string languageId);

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task<bool> UpdateStock(int productId, int addedQuantity);
        
        Task AddViewCount(int productId);
        
        Task<PageResults<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);

        Task<int> AddImages(int productId, List<IFormFile> files);

        Task<int> RemoveImages(int imageId);
        
        Task<int> UpdateImages(int imageId, string caption, bool isDefault);
        
        Task<List<ProductImageViewModel>> GetListImage(int productId);
        
    }
}
