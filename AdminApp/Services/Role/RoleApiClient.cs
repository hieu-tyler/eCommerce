using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using ViewModels.Common;
using ViewModels.System.Roles;
using ViewModels.System.Users;

namespace AdminApp.Services.Role
{
    public class RoleApiClient : BaseApiClient, IRoleApiClient
    {
        public RoleApiClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
            : base(httpClient, httpContextAccessor, configuration)
        {
        }

        public async Task<ApiResult<List<RoleViewModel>>> GetAll()
        {
           return await GetAsync<ApiResult<List<RoleViewModel>>>("/api/roles");
        }
    }
}
