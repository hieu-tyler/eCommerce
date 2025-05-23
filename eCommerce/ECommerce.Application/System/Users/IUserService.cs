using ECommerce.ECommerce.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity.Data;

namespace ECommerce.ECommerce.Application.System.Users
{
    public interface IUserService
    {
        Task<string> Authenticate(ViewModels.System.Users.LoginRequest request);
        
        Task<bool> Register(ViewModels.System.Users.RegisterRequest request);

        //Task<>
    }
}
