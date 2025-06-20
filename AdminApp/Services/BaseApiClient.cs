using System.Net.Http.Headers;
using System.Text.Json;
using Utilities.SystemConstants.cs;
using ViewModels.Common;
using ViewModels.System.Users;

namespace AdminApp.Services
{
    public class BaseApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BaseApiClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
            _httpClient.BaseAddress = new Uri(_configuration[SystemConstants.AppSettings.BaseAddress]);
        }

        protected async Task<TResponse> GetAsync<TResponse>(string url)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session == null || !session.TryGetValue("Token", out var tokenBytes))
            {
                throw new InvalidOperationException("Session is not available or BearerToken is not set.");
            }
            var token = session.GetString("Token");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _httpClient.GetAsync(url);
            var body = await response.Content.ReadAsStringAsync();
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            if (response.IsSuccessStatusCode)
            {

                TResponse myDeserializedObjList = JsonSerializer.Deserialize<TResponse>(body, options);
                
                return myDeserializedObjList;
            }
            return JsonSerializer.Deserialize<TResponse>(body, options);
        }
    }
}
