using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.System.Languages;
using ViewModels.Common;

namespace Application.System.Languages
{
    public interface ILanguageService
    {
        public Task<ApiResult<List<LanguageViewModel>>> GetAll();
    }
}
