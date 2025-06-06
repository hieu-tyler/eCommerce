using ViewModels.Common;
using ViewModels.System.Users;

namespace AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<string> Authenticate(LoginRequest request);
        
        Task<bool> RegisterUser(RegisterRequest request);
        //Task<bool> CreateUser(CreateUserRequest request);

        Task<PageResult<UserViewModel>> GetUserPaging(GetUserPagingRequest request);

    }
}