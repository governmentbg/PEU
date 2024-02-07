using FluentValidation;
using FluentValidation.Resources;

namespace EAU.Documents.Domain.Validations.FluentValidation
{
    public class StaticStringSourceWithParameters : IStringSource
    {
        private string _code;

        public StaticStringSourceWithParameters(string code, params string[] additionalParameters)
        {
            _code = code;
            AdditionalParameters = additionalParameters;
        }

        public string[] AdditionalParameters { get; private set; }

        public string GetString(ICommonContext context)
        {
            return _code;
        }
    }
}
