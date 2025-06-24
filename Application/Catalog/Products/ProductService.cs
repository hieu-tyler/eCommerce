using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Http;
using Application.Common;
using Data.EFContext;
using Data.Entities;
using ViewModels.Catalog.Products;
using ViewModels.Catalog.ProductImages;
using ViewModels.Common;
using Utilities.Exceptions;

namespace Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly ECommerceDbContext _context;
        private readonly IStorageService _storageService;

        public ProductService(ECommerceDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }
        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name =  request.Name,
                        Description = request.Description,
                        Details = request.Details,
                        SeoDescription = request.SeoDescription,
                        SeoAlias = request.SeoAlias,
                        SeoTitle = request.SeoTitle,
                        LanguageId = request.LanguageId
                    }
                }
            };
            // Save image
            if (request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption = "Thumbnail image",
                        DateCreated = DateTime.Now,
                        FileSize = request.ThumbnailImage.Length,
                        ImagePath = await SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder = 1
                    }
                };
            }
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return product.Id;
        }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new ECommerceException($"Cannot find a product: {productId}");

            var images = _context.ProductImages.Where(i => i.ProductId == productId);
            foreach (var image in images)
            {
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Products.Remove(product);

            return await _context.SaveChangesAsync();
        }

        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            // Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        //join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        //join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId == request.LanguageId
                        select new { p, pt };

            // Filter
            if (!string.IsNullOrEmpty(request.Keyword))
                query = query.Where(x => x.pt.Name.Contains(request.Keyword));

            //if (request.CategoryId != null && request.CategoryId.Value > 0)
            //    query = query.Where(x => request.CategoryId == x.pic.CategoryId);

            // Paging
            int totalRow = await query.CountAsync();
            int skipPage = (request.PageIndex - 1) * request.PageSize;
            var data = await query
                    .Skip(skipPage)
                    .Take(request.PageSize)
                    .Select(x => new ProductViewModel()
                    {
                        //Id = x.p.Id,
                        Name = x.pt.Name,
                        DateCreated = x.p.DateCreated,
                        Description = x.pt.Description,
                        LanguageId = x.pt.LanguageId,
                        OriginalPrice = x.p.OriginalPrice,
                        Price = x.p.Price,
                        SeoAlias = x.pt.SeoAlias,
                        SeoDescription = x.pt.SeoDescription,
                        SeoTitle = x.pt.SeoTitle,
                        Stock = x.p.Stock,
                        ViewCount = x.p.ViewCount,
                    }).ToListAsync();

            // Select and projection
            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data,
            };

            return pageResult;
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTranslations = await _context.ProductTranslations.FirstOrDefaultAsync(x => x.ProductId == request.Id && x.LanguageId == request.LanguageId);

            if (product == null || productTranslations == null)
                throw new ECommerceException($"Didn't find any product with productId: {request.Id}");

            productTranslations.Name = request.Name;
            productTranslations.Description = request.Description;
            productTranslations.SeoDescription = request.SeoDescription;
            productTranslations.SeoTitle = request.SeoTitle;
            productTranslations.Details = request.Details;

            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new ECommerceException($"Didn't find any product with productId: {productId}");
            product.Price = newPrice;

            return await _context.SaveChangesAsync() != 0;
        }

        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
                throw new ECommerceException($"Didn't find any product with productId: {productId}");
            product.Stock += addedQuantity;

            return await _context.SaveChangesAsync() != 0;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task<ProductViewModel> GetProductById(int productId, string languageId)
        {
            var product = await _context.Products.FindAsync(productId);
            var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(
                x => x.ProductId == productId && x.LanguageId == languageId);

            if (productTranslation == null) throw new ECommerceException("No product translation");

            var productViewModel = new ProductViewModel()
            {
                Id = product.Id,
                DateCreated = product.DateCreated,
                Description = productTranslation != null ? productTranslation.Description : null,
                LanguageId = productTranslation.LanguageId,
                Details = productTranslation != null ? productTranslation.Details : null,
                Name = productTranslation != null ? productTranslation.Name : null,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
                SeoAlias = productTranslation != null ? productTranslation.SeoAlias : null,
                SeoDescription = productTranslation != null ? productTranslation.SeoDescription : null,
                SeoTitle = productTranslation != null ? productTranslation.SeoTitle : null,
                Stock = product.Stock,
                ViewCount = product.ViewCount
            };

            return productViewModel;
        }

        public async Task<int> AddImage(int productId, ProductImageCreateRequest request)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new ECommerceException($"Cannot find a product to add image: {productId}");

            var image = new ProductImage()
            {
                Caption = request.Caption,
                IsDefault = request.IsDefault,
                SortOrder = request.SortOrder,
                DateCreated = DateTime.Now,
                ProductId = productId
            };

            if (request.ImageFile != null)
            {
                image.ImagePath = await SaveFile(request.ImageFile);
                image.FileSize = request.ImageFile.Length;
            }

            _context.ProductImages.Add(image);
            await _context.SaveChangesAsync();
            return image.Id; 
        }

        public async Task<int> RemoveImages(int imageId)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);

            if (productImage == null) throw new ECommerceException($"Cannot find and delete product image with id: {imageId}");

            _context.ProductImages.Remove(productImage);

            return await _context.SaveChangesAsync();

        }

        public async Task<int> UpdateImages(int imageId, ProductImageUpdateRequest request)
        {
            var productImage = await _context.ProductImages.FindAsync(imageId);
            if (productImage == null)
                throw new ECommerceException($"Cannot find a product image: {imageId}");
            else
            {
                productImage.ImagePath = await SaveFile(request.ImageFile);
                productImage.FileSize = request.ImageFile.Length;
            }

            _context.ProductImages.Update(productImage);

            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductImageViewModel>> GetListImages(int productId)
        {
            return await _context.ProductImages
                .Where(x => x.ProductId == productId)
                .Select(x => new ProductImageViewModel()
                {
                    Caption = x.Caption,
                    DateCreated = x.DateCreated,
                    FileSize = x.FileSize,
                    ImagePath = x.ImagePath,
                    IsDefault = x.IsDefault,
                    ProductId = x.ProductId,
                    SortOrddr = x.SortOrder
                }).ToListAsync();
        }

        public async Task<ProductImageViewModel> GetImageById(int imageId)
        {
           var searchedImage = await _context.ProductImages
                .Where(x => x.Id == imageId)
                .Select(x => new ProductImageViewModel()
                {
                    Caption = x.Caption,
                    DateCreated = x.DateCreated,
                    FileSize = x.FileSize,
                    ImagePath = x.ImagePath,
                    IsDefault = x.IsDefault,
                    ProductId = x.ProductId,
                    SortOrddr = x.SortOrder
                }).FirstOrDefaultAsync();

            if (searchedImage == null) throw new ECommerceException($"Cannot find product image with id: {imageId}");
            else return searchedImage;
        }

        public async Task<PageResult<ProductViewModel>> GetAllByCategoryId(GetProductPagingRequest request, string languageId)
        {
            // Select join
            var query = from p in _context.Products
                        join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                        join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                        join c in _context.Categories on pic.CategoryId equals c.Id
                        where pt.LanguageId == languageId
                        select new { p, pt, pic };

            // Filter
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
                query = query.Where(x => x.pic.CategoryId == request.CategoryId);

            // Paging
            int totalRow = await query.CountAsync();
            int skipPage = (request.PageIndex - 1) * request.PageSize;
            var data = await query
                    .Skip(skipPage)
                    .Take(request.PageSize)
                    .Select(x => new ProductViewModel()
                    {
                        Id = x.p.Id,
                        Name = x.pt.Name,
                        DateCreated = x.p.DateCreated,
                        Description = x.pt.Description,
                        LanguageId = x.pt.LanguageId,
                        OriginalPrice = x.p.OriginalPrice,
                        Price = x.p.Price,
                        SeoAlias = x.pt.SeoAlias,
                        SeoDescription = x.pt.SeoDescription,
                        SeoTitle = x.pt.SeoTitle,
                        Stock = x.p.Stock,
                        ViewCount = x.p.ViewCount,
                    }).ToListAsync();

            // Select and projection
            var pageResult = new PageResult<ProductViewModel>()
            {
                TotalRecords = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                Items = data,
            };

            return pageResult;
        }
    }
}
