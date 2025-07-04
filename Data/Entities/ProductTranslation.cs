﻿namespace Data.Entities
{
    public class ProductTranslation
    {
        public int Id { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public string Details { set; get; }
        public string SeoDescription { set; get; }
        public string SeoTitle { set; get; }
        public string SeoAlias { get; set; }

        public Product Product { get; set; }
        public int ProductId { set; get; }
        public Language Language { get; set; }
        public string LanguageId { set; get; }
    }
}
