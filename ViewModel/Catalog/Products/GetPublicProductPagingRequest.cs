using ViewModels.Common;

namespace ViewModels.Catalog.Products
{
    public class GetPublicProductPagingRequest : PagingRequestBase
    {
        //public string LanguageId { get; set; } = "vi";
        public int? CategoryId { get; set; }

    }
}
