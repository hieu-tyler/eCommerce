using ViewModels.System.Languages;

namespace AdminApp.Models
{
    public class NavigationViewModel
    {
        public ICollection<LanguageViewModel> Languages { get; set; }

        public string CurrentLanguageId { get; set; }
    }
}
