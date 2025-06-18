using ViewModels.Common;
using ViewModels.System.Roles;

namespace AdminApp.Services
{
    public interface IRoleApiClient
    {
        public Task<ApiResult<List<RoleViewModel>>> GetAll();
    }
}
