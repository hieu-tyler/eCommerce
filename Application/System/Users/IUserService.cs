using ViewModels.Common;
using ViewModels.System.Users;

namespace Application.System.Users
{
    public interface IUserService
    {
        Task<string> Authenticate(LoginRequest request);
        
        Task<bool> Register(RegisterRequest request);

        Task<PageResult<UserViewModel>> GetUsersPaging(GetUserPagingRequest request);
    }
}
