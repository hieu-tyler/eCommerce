using ViewModels.Common;
using ViewModels.System.Languages;

namespace AdminApp.Services.Language
{
    public interface ILanguageApiClient
    {
        Task<ApiResult<List<LanguageViewModel>>> GetAll();
    }
}
