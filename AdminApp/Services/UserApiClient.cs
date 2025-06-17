using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using ViewModels.Common;
using ViewModels.System.Users;

namespace AdminApp.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserApiClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration["BaseAddress"]);
        }

        public async Task<ApiResult<string>> Authenticate(LoginRequest request)
        {
            // Serialize the request object to JSON and create an HttpContent object
            var jsonContent = System.Text.Json.JsonSerializer.Serialize(request);
            var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            // Contact the backend API to authenticate the user port 7215
            var response = await _httpClient.PostAsync("api/user/authenticate", httpContent);
            var token = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return new ApiSuccessResult<string>(token);
            else
                return new ApiErrorResult<string>("Failed to authenticate user");

        }

        public async Task<ApiResult<bool>> DeleteUser(Guid id)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null || !session.TryGetValue("Token", out var tokenBytes))
            {
                throw new InvalidOperationException("Session is not available or BearerToken is not set.");
            }
            var token = session.GetString("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var result = await _httpClient.DeleteAsync($"/api/user/{id}");
            var body = await result.Content.ReadAsStringAsync();
            if (result.IsSuccessStatusCode)
                return new ApiSuccessResult<bool>(result.IsSuccessStatusCode);
            else
                return new ApiErrorResult<bool>("Failed to authenticate user");
        }

        public async Task<ApiResult<UserViewModel>> GetById(Guid id)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null || !session.TryGetValue("Token", out var tokenBytes))
            {
                throw new InvalidOperationException("Session is not available or BearerToken is not set.");
            }
            var token = session.GetString("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.GetAsync($"/api/user/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ApiResult<UserViewModel>>(body)!;
            }


            return new ApiErrorResult<UserViewModel>("Failed to get by Id");
        }

        public async Task<ApiResult<PageResult<UserViewModel>>> GetUserPaging(GetUserPagingRequest request)
        {
            // Preprare _httpClient for the request
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null || !session.TryGetValue("Token", out var tokenBytes))
            {
                throw new InvalidOperationException("Session is not available or BearerToken is not set.");
            }
            var token = session.GetString("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Contact the backend API to authenticate the user port 
            var response = await _httpClient.GetAsync($"api/user/paging?pageIndex={request.PageIndex}" +
                $"&pageSize={request.PageSize}" +
                $"&keyword={request.Keyword}");

            var body = await response.Content.ReadAsStringAsync();

            var users = JsonConvert.DeserializeObject<PageResult<UserViewModel>>(body);

            return new ApiSuccessResult<PageResult<UserViewModel>>(users);
        }

        public async Task<ApiResult<bool>> RegisterUser(RegisterRequest request)
        {
            // Serialize the request object to JSON and create an HttpContent object
            var jsonContent = System.Text.Json.JsonSerializer.Serialize(request);
            var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            // Contact the backend API to register the user
            var response = _httpClient.PostAsync("api/user/register", httpContent).Result;

            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return new ApiSuccessResult<bool>(true);

            return new ApiErrorResult<bool>("Failed to register user");
        }

        public async Task<ApiResult<bool>> RoleAssign(Guid id, RoleAssignRequest request)
        {
            // Preprare _httpClient for the request
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null || !session.TryGetValue("Token", out var tokenBytes))
            {
                throw new InvalidOperationException("Session is not available or BearerToken is not set.");
            }
            var token = session.GetString("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/user/{id}/roles", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return new ApiSuccessResult<bool>(true);

            return new ApiErrorResult<bool>("Failed to assign roles");
        }

        public async Task<ApiResult<bool>> UpdateUser(Guid id, UpdateRequest request)
        {
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"/api/user/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return new ApiSuccessResult<bool>(true);

            return new ApiErrorResult<bool>("Failed to update user");
        }
    }
}
