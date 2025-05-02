using Microsoft.AspNetCore.Identity;

namespace ECommerce.Models
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
