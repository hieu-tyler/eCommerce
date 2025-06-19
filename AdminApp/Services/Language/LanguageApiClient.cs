using ViewModels.Common;
using ViewModels.System.Languages;

namespace AdminApp.Services.Language
{
    public class LanguageApiClient : BaseApiClient, ILanguageApiClient
    {
        public LanguageApiClient(HttpClient httpClient, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) 
            : base(httpClient, httpContextAccessor, configuration)
        {
        }

        public async Task<ApiResult<List<LanguageViewModel>>> GetAll()
        {
            var languages = await GetAsync<List<LanguageViewModel>>("/api/languages");

            if (languages == null)
            {
                return new ApiErrorResult<List<LanguageViewModel>>("Failed to retrieve languages.");
            }

            return new ApiSuccessResult<List<LanguageViewModel>>(languages);
        }
    }
}
