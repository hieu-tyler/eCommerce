using ECommerce.DTOs;

namespace ECommerce.Catalog.Products.DTOs.Public
{
    public class GetProductPagingRequest : PagingRequestBase
    {
        public int? CategoryId { get; set; }

    }
}
