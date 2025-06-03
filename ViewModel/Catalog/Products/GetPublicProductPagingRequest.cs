using ECommerce.ViewModels.Common;

namespace ECommerce.ViewModels.Catalog.Products.Public
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        //public string LanguageId { get; set; } = "vi";
        public int? CategoryId { get; set; }

    }
}
