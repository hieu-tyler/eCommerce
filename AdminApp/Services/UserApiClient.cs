using Microsoft.AspNetCore.Http;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
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

        public async Task<string> Authenticate(LoginRequest request)
        {
            // Serialize the request object to JSON and create an HttpContent object
            var jsonContent = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");

            // Contact the backend API to authenticate the user port 7215
            var response = await _httpClient.PostAsync("api/user/authenticate", httpContent);
            var token = await response.Content.ReadAsStringAsync();
            return token;
        }

        public async Task<PageResult<UserViewModel>> GetUserPaging(GetUserPagingRequest request)
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

            var users = JsonSerializer.Deserialize<PageResult<UserViewModel>>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
                Converters = { new JsonStringEnumConverter() } // Handle enum serialization if needed
            });

            return users; // Ensure the deserialized object is returned
        }

        public Task<bool> RegisterUser(RegisterRequest request)
        {
            // Serialize the request object to JSON and create an HttpContent object
            var jsonContent = JsonSerializer.Serialize(request);
            var httpContent = new StringContent(jsonContent, System.Text.Encoding.UTF8, "application/json");
            // Contact the backend API to register the user
            var response = _httpClient.PostAsync("api/user/register", httpContent).Result;

            if (response.IsSuccessStatusCode)
            {
                return Task.FromResult(true);
            }
            else
            {
                return Task.FromResult(false);
            }
        }
    }
}
