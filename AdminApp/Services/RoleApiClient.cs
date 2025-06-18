using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using ViewModels.Common;
using ViewModels.System.Roles;
using ViewModels.System.Users;

namespace AdminApp.Services
{
    public class RoleApiClient : IRoleApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public RoleApiClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
        }
        public async Task<ApiResult<List<RoleViewModel>>> GetAll()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null || !session.TryGetValue("Token", out var tokenBytes))
            {
                throw new InvalidOperationException("Session is not available or BearerToken is not set.");
            }
            var token = session.GetString("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"/api/roles");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<RoleViewModel> objList = (List<RoleViewModel>)JsonConvert.DeserializeObject(body, typeof(List<RoleViewModel>));
                return new ApiSuccessResult<List<RoleViewModel>>(objList);
            }

            return new ApiErrorResult<List<RoleViewModel>>("Failed to get all roles");

        }
    }
}
