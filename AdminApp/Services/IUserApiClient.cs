using ViewModels.Common;
using ViewModels.System.Users;

namespace AdminApp.Services
{
    public interface IUserApiClient
    {
        Task<ApiResult<string>> Authenticate(LoginRequest request);

        Task<ApiResult<PageResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request);

        Task<ApiResult<bool>> RegisterUser(RegisterRequest registerRequest);

        Task<ApiResult<bool>> UpdateUser(Guid id, UpdateRequest request);

        Task<ApiResult<UserViewModel>> GetById(Guid id);
    }
}