

namespace ECommerce.Application.System.Users
{
    public interface IUserService
    {
        Task<string> Authenticate(ViewModels.System.Users.LoginRequest request);
        
        Task<bool> Register(ViewModels.System.Users.RegisterRequest request);

        //Task<>
    }
}
