using ECommerce.ECommerce.Data.EF;
using ECommerce.ViewModels.Catalog.Products;
using ECommerce.ViewModels.Catalog.Products.Public;
using Microsoft.EntityFrameworkCore;
using ECommerce.ViewModels.Common;
using Azure.Core;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace ECommerce.ECommerce.Application.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly ECommerceDbContext _context;
        public int categoryId { get; set; }

        public PublicProductService(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<PageResults<ProductViewModel>> GetAllByCategoryId(GetPublicProductPagingRequest request, string languageId)
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
            var pageResult = new PageResults<ProductViewModel>()
            {
                TotalRecords = totalRow,
                Items = data,
            };

            return pageResult;
        }

        //public async Task<List<ProductViewModel>> GetAll(string languageId)
        //{
        //    // Query
        //    var query = from p in _context.Products
        //                join pt in _context.ProductTranslations on p.Id equals pt.ProductId
        //                join pic in _context.ProductInCategories on p.Id equals pic.ProductId
        //                join c in _context.Categories on pic.CategoryId equals c.Id
        //                where pt.LanguageId == languageId
        //                select new { p, pt, pic };

        //    // 
        //    var data = await query
        //            .Select(x => new ProductViewModel()
        //            {
        //                Id = x.p.Id,
        //                Name = x.pt.Name,
        //                DateCreated = x.p.DateCreated,
        //                Description = x.pt.Description,
        //                LanguageId = x.pt.LanguageId,
        //                OriginalPrice = x.p.OriginalPrice,
        //                Price = x.p.Price,
        //                SeoAlias = x.pt.SeoAlias,
        //                SeoDescription = x.pt.SeoDescription,
        //                SeoTitle = x.pt.SeoTitle,
        //                Stock = x.p.Stock,
        //                ViewCount = x.p.ViewCount,
        //            }).ToListAsync();

        //    return data;
        //}
    }
}
