using System.Net.Http;
using System.Text.Json;
using ViewModels.System.Users;

namespace AdminApp.Services
{
    public class UserApiClient : IUserApiClient
    {
        private readonly HttpClient _httpClient;
        private const string BackendUrl = "https://localhost:7215";
        public UserApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BackendUrl);
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
    }
}
