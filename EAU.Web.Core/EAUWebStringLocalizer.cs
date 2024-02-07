using EAU.Nomenclatures;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace EAU.Web
{
    /// <summary>
    /// Реализация на IStringLocalizer
    /// </summary>
    public class EAUWebStringLocalizer : IStringLocalizer
    {
        private readonly string _currentLang;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILabelLocalizations _labels;
        private readonly Regex _regex = null;

        #region Constructors

        public EAUWebStringLocalizer(IHttpContextAccessor httpContextAccessor, ILabelLocalizations labels)
        {
            _httpContextAccessor = httpContextAccessor;

            _labels = labels;

            _regex = new Regex("{[A-Za-z0-9_]*}");
        }

        protected EAUWebStringLocalizer(CultureInfo culture, ILabelLocalizations labels)
        {
            _currentLang = culture.Name.Substring(0, 2);

            _httpContextAccessor = null;

            _labels = labels;
        }

        #endregion

        #region IStringLocalizer

        public LocalizedString this[string name]
        {
            get
            {
                LocalizedString result;
                var resultString = _labels.Get(CurrentLanguage, name);

                if (string.IsNullOrEmpty(resultString))
                {
                    result = null;
                }
                else
                {
                    result = new LocalizedString(name, resultString);
                }

                return result;
            }
        }

        public LocalizedString this[string name, params object[] arguments]
        {
            get
            {
                LocalizedString result;
                var resultString = _labels.Get(CurrentLanguage, name);

                if (string.IsNullOrEmpty(resultString))
                {
                    result = new LocalizedString(name, "MISSING_Label_" + name, true);
                }
                else
                {
                    var argumentsKeys = new List<string>();

                    var match = _regex.Match(resultString);

                    while (match != null && match.Success)
                    {
                        argumentsKeys.Add(match.Groups[0].Value);

                        match = match.NextMatch();
                    }

                    for (int i = 0; i < argumentsKeys.Count; i++)
                    {
                        if (arguments.Count() > i)
                        {
                            resultString = resultString.Replace(argumentsKeys[i], (string)arguments[i]);
                        }
                    }

                    result = new LocalizedString(name, resultString);
                }

                return result;
            }
        }

        public IEnumerable<LocalizedString> GetAllStrings(bool includeParentCultures)
        {
            var labels = _labels.Search(CurrentLanguage, out DateTime? lastModifiedDate);

            if (labels != null && labels.Count > 0)
            {
                foreach (var labelPair in labels)
                {
                    yield return new LocalizedString(labelPair.Key, labelPair.Value);
                }
            }
        }

        public IStringLocalizer WithCulture(CultureInfo culture) => new EAUWebStringLocalizer(culture, _labels);

        #endregion

        #region Helpers

        private string CurrentLanguage
        {
            get => _currentLang ?? _httpContextAccessor?.HttpContext?.GetLanguage();
        }

        #endregion
    }
}
