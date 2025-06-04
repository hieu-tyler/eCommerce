using ViewModels.Common;

namespace Application.System.Users
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
        //public string RoleId { get; set; } // Assuming RoleId is a string, adjust as necessary
        public bool? IsActive { get; set; } // Nullable to allow filtering by active status
        //public string SortBy { get; set; } // Property to specify sorting criteria
        //public bool SortDescending { get; set; } // Property to specify sort order (ascending/descending)
    }
}