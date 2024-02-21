using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace P2FixAnAppDotNetCode.Models.ViewModels
{
    public class LanguageViewModel
    {
        public List<SelectListItem> LanguageOptions { get; set; }
        public string Language { get; set; }
    }
}
