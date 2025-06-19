using AdminApp.Models;
using AdminApp.Services.Language;
using Microsoft.AspNetCore.Mvc;
using NuGet.Versioning;
using Utilities.SystemConstants.cs;

namespace AdminApp.Controllers.Components
{
    public class NavigationViewComponent : ViewComponent
    {
        private readonly ILanguageApiClient _languageApiClient;

        public NavigationViewComponent(ILanguageApiClient languageApiClient)
        {
            _languageApiClient = languageApiClient;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var languages = await _languageApiClient.GetAll();
            var navigationViewModel = new NavigationViewModel()
            {
                CurrentLanguageId = HttpContext.Session.GetString(SystemConstants.AppSettings.DefaultLanguageId),
                Languages = languages.ResultObject
            };

            return View("Default", navigationViewModel);
        }
    }
}
