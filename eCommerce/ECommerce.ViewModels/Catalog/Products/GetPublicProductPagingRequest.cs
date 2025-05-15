using ECommerce.ViewModels.Common;

namespace ECommerce.ViewModels.Catalog.Products.Public
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }

    }
}
