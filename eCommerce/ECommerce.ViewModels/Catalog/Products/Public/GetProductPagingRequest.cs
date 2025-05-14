using ECommerce.ViewModels.Common;

namespace ECommerce.ViewModels.Catalog.Products.Public
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }

    }
}
