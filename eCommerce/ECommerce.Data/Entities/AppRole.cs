using Microsoft.AspNetCore.Identity;

namespace ECommerce.ECommerce.Data.Entities
{
    public class AppRole : IdentityRole<Guid>
    {
        public string Description { get; set; }
    }
}
