using ViewModels.Common;

namespace ViewModels.Catalog.Products
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        public int? CategoryId { get; set; }
    }
}
