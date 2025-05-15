namespace ECommerce.ECommerce.Application.Catalog.Products
{
    public class ProductImageViewModel
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public bool IsDefault { get; set; }
        public long fileSize { get; set; }
    }
}