﻿using ViewModels.Catalog.ProductImages;
using ViewModels.Catalog.Products;
using ViewModels.Common;

namespace Application.Catalog.Products
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

        Task<int> AddImage(int productId, ProductImageCreateRequest request);

        Task<int> RemoveImages(int imageId);
        
        Task<int> UpdateImages(int imageId, ProductImageUpdateRequest request);

        //Task<ProductImageViewModel> GetProductImageById(int imageId, int productId);
        Task<ProductImageViewModel> GetImageById(int imageId);

        Task<List<ProductImageViewModel>> GetListImages(int productId);
        
    }
}
