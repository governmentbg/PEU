using FluentValidation.Resources;
using Microsoft.Extensions.Localization;
using System.Globalization;

namespace EAU.Documents.Domain.Validations.FluentValidation
{
    public class LanguageManagerBridge : ILanguageManager
    {
        private readonly IStringLocalizer _localizer;

        public LanguageManagerBridge(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        // here ignore culture arg, _localizer has already a culture
        public string GetString(string key, CultureInfo culture = null)
        {
            LocalizedString str = _localizer[key];

            return str != null ? str.Value : string.Format("KEY NOT FOUND for {0}", key);
        }

        public bool Enabled { get; set; }
        public CultureInfo Culture { get; set; }
    }
}
