using ECommerce.ECommerce.Data.Entities;

namespace ECommerce.ECommerce.ViewModels.Catalog.ProductImages
{
    public class ProductImageUpdateRequest
    {
        public string ImagePath { get; set; }
        public string Caption { get; set; }
        public bool IsDefault { get; set; }
        public int SortOrder { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
