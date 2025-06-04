namespace ViewModels.Common
{
    public class PageResults<T>
    {
        public List<T> Items { get; set; }
        public int TotalRecords { get; set; }
    }
}
