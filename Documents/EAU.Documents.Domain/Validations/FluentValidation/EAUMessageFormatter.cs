using FluentValidation.Internal;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EAU.Documents.Domain.Validations.FluentValidation
{
    public class EAUMessageFormatter : MessageFormatter
    {
        private static readonly Regex _keyRegex2 = new Regex("<([^<>:]+)(?::([^<>]+))?>", RegexOptions.Compiled);

        protected override string ReplacePlaceholdersWithValues(string template, IDictionary<string, object> values)
        {
            //copied from https://github.com/FluentValidation/FluentValidation/blob/master/src/FluentValidation/Internal/MessageFormatter.cs
            string ret = base.ReplacePlaceholdersWithValues(template, values);

            return _keyRegex2.Replace(ret, m =>
            {
                var key = m.Groups[1].Value;

                if (!values.TryGetValue(key, out var value))
                    return m.Value; // No placeholder / value

                var format = m.Groups[2].Success // Format specified?
                    ? $"{{0:{m.Groups[2].Value}}}"
                    : null;

                return format == null
                    ? value?.ToString()
                    : string.Format(format, value);
            });
        }
    }
}
