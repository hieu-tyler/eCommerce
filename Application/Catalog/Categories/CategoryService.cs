using Data.EFContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Catalog.Categories;

namespace Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly ECommerceDbContext _context;

        public CategoryService(ECommerceDbContext context)
        {
            _context = context;
        }
        public async Task<List<CategoryViewModel>> GetAll(string languageId)
        {
            var query = from c in _context.Categories
                        join ct in _context.CategoryTranslations on c.Id equals ct.CategoryId
                        where ct.LanguageId == languageId
                        select new { c, ct };
                        ;

            return await query.Select(result => new CategoryViewModel()
            {
                Id = result.c.Id,
                Name = result.ct.Name,
            }).ToListAsync();
        }
    }
}
