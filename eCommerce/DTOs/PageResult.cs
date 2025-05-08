namespace ECommerce.DTOs
{
    public class PageResult<T>
    {
        public List<T> Items { set; get; }
        public int TotalRecords { get; set; }
    
    }
}
