using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace EAU.Documents.Domain.Validations
{
    public abstract class EAUValidator<T> : AbstractValidator<T>
    {
        #region Types

        protected struct PlaceHolder
        {
            public PlaceHolder(string name, string resourceCode)
            {
                Name = name;
                ResourceCode = resourceCode;
            }

            public string Name { get; set; }
            public string ResourceCode { get; set; }
        }

        #endregion

        public IRuleBuilderInitial<T, TProperty> EAURuleFor<TProperty>(Expression<Func<T, TProperty>> expression)
        {
            return RuleFor(expression).EAUConfigureRule();
        }

        public IRuleBuilderInitialCollection<T, TElement> EAURuleForEach<TElement>(Expression<Func<T, IEnumerable<TElement>>> expression)
        {
            return RuleForEach(expression).EAUConfigureCollectionRule();
        }

        protected string Localized(string code)
        {
            return ValidatorOptions.Global.LanguageManager.GetString(code);
        }

        protected void AddValidationFailure(ValidationResult validationResult, string errorCode, params PlaceHolder[] arguments)
        {
            var messageFormatter = ValidatorOptions.Global.MessageFormatterFactory();
            
            if(arguments != null)
            {
                foreach(var argument in arguments)
                {
                    messageFormatter.AppendArgument(argument.Name, Localized(argument.ResourceCode));
                }
            }

            var error = new ValidationFailure("", messageFormatter.BuildMessage(Localized(errorCode)));

            validationResult.Errors.Add(error);
        }

        protected void AddValidationFailureWithoutParamsTranslation(ValidationResult validationResult, string errorCode, params PlaceHolder[] arguments)
        {
            var messageFormatter = ValidatorOptions.Global.MessageFormatterFactory();

            if (arguments != null)
            {
                foreach (var argument in arguments)
                {
                    if(string.Compare(argument.Name, "Field", true) == 0)
                    {
                        messageFormatter.AppendArgument(argument.Name, Localized(argument.ResourceCode));
                    }
                    else
                    {
                        messageFormatter.AppendArgument(argument.Name, argument.ResourceCode);
                    }
                }
            }

            var error = new ValidationFailure("", messageFormatter.BuildMessage(Localized(errorCode)));

            validationResult.Errors.Add(error);
        }
    }
}
