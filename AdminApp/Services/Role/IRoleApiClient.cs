using ViewModels.Common;
using ViewModels.System.Roles;

namespace AdminApp.Services.Role
{
    public interface IRoleApiClient
    {
        public Task<ApiResult<List<RoleViewModel>>> GetAll();
    }
}
