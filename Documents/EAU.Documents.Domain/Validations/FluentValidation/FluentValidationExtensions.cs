using FluentValidation;
using FluentValidation.Validators;
using System;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;

namespace EAU.Documents.Domain.Validations.FluentValidation
{
    public static class FluentValidationExtensions
    {
        #region Infrastructure Extensions

        public static IRuleBuilderInitial<T, TProperty> EAUConfigureRule<T, TProperty>(this IRuleBuilderInitial<T, TProperty> ruleBuilder)
        {
            ruleBuilder.Configure(pr =>
            {
                pr.MessageBuilder = (context) =>
                {
                    StaticStringSourceWithParameters stringSourceWithParameters = context.PropertyValidator.Options.ErrorCodeSource as StaticStringSourceWithParameters;

                    string labelCode = GetObjectPropertyResourseCode(typeof(T), context.Rule.PropertyName);
                    string sectionCode = GetObjectResourseCode(typeof(T));

                    context.MessageFormatter.AppendArgument("Field", ValidatorOptions.Global.LanguageManager.GetString(labelCode));
                    context.MessageFormatter.AppendArgument("Section", ValidatorOptions.Global.LanguageManager.GetString(sectionCode));

                    if (stringSourceWithParameters?.AdditionalParameters != null)
                    {
                        for (int i = 0; i < stringSourceWithParameters?.AdditionalParameters.Length; i++)
                        {
                            string paramName = string.Format("Param{0}", i + 1);
                            context.MessageFormatter.AppendArgument(paramName, stringSourceWithParameters.AdditionalParameters[i]);
                        }
                    }

                    return context.GetDefaultMessage();
                };
            });

            return ruleBuilder;
        }

        public static IRuleBuilderInitialCollection<T, TElement> EAUConfigureCollectionRule<T, TElement>(this IRuleBuilderInitialCollection<T, TElement> ruleBuilder)
        {
            ruleBuilder.Configure(pr =>
            {
                pr.MessageBuilder = (context) =>
                {
                    StaticStringSourceWithParameters stringSourceWithParameters = context.PropertyValidator.Options.ErrorCodeSource as StaticStringSourceWithParameters;

                    string labelCode = GetObjectPropertyResourseCode(typeof(T), context.Rule.PropertyName);
                    string sectionCode = GetObjectResourseCode(typeof(T));

                    context.MessageFormatter.AppendArgument("Field", ValidatorOptions.Global.LanguageManager.GetString(labelCode));
                    context.MessageFormatter.AppendArgument("Section", ValidatorOptions.Global.LanguageManager.GetString(sectionCode));

                    if (stringSourceWithParameters?.AdditionalParameters != null)
                    {
                        for (int i = 0; i < stringSourceWithParameters?.AdditionalParameters.Length; i++)
                        {
                            string paramName = string.Format("Param{0}", i + 1);
                            context.MessageFormatter.AppendArgument(paramName, stringSourceWithParameters.AdditionalParameters[i]);
                        }
                    }

                    return context.GetDefaultMessage();
                };
            });
            return ruleBuilder;
        }

        public static IRuleBuilderOptions<T, TProperty> WithEAUErrorCode<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, string errorMessageCode, params string[] parameters)
        {
            rule.Configure(pr =>
            {
                pr.CurrentValidator.Options.ErrorCodeSource = new StaticStringSourceWithParameters(errorMessageCode, parameters);
            });

            return rule;
        }

        /// <summary>
        /// Разширени за инжектиране на валидатор, пренаписващ основното поведение за работа с ruleSets.
        /// Има ли подаден/и ruleSets разрешаваме зачитането на rule-ве, които не участват в ruleSets ("default"). 
        /// https://docs.fluentvalidation.net/en/latest/rulesets.html
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <param name="ruleBuilder"></param>
        /// <param name="ruleSets"></param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TProperty> EAUInjectValidator<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, params string[] ruleSets)
        {
            return ruleBuilder.InjectValidator(ruleSets?.Union(new string[] { "default" }).ToArray());
        }

        //public static IRuleBuilderOptions<T, TProperty> EAUInjectPropertyValidator<T, TProperty, TPropertyValidator>(this IRuleBuilder<T, TProperty> ruleBuilder) where TPropertyValidator : IPropertyValidator
        //{
        //    return ruleBuilder.SetValidator(Activator.CreateInstance<TPropertyValidator>());
        //}

        /// <summary>
        /// Разширени за добавяне на наследствен валидатор, променящо основното поведение за работа с ruleSets.
        /// Има ли подаден/и ruleSets разрешаваме зачитането на rule-ве, които не участват в ruleSets ("default"). 
        /// https://docs.fluentvalidation.net/en/latest/rulesets.html
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TProperty"></typeparam>
        /// <typeparam name="TDerived"></typeparam>
        /// <param name="polymorphicValidator"></param>
        /// <param name="derivedValidator"></param>
        /// <param name="ruleSets"></param>
        /// <returns></returns>
        public static PolymorphicValidator<T, TProperty> EAUAdd<T, TProperty, TDerived>(
            this PolymorphicValidator<T, TProperty> polymorphicValidator
            , IValidator<TDerived> derivedValidator
            , params string[] ruleSets) where TDerived : TProperty
        {
            return polymorphicValidator.Add<TDerived>(derivedValidator, ruleSets?.Union(new string[] { "default" }).ToArray());
        }

        #endregion

        public static IRuleBuilderOptions<T, string> EgnValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must((val) => 
            {
                if (string.IsNullOrEmpty(val))
                {
                    return true;
                }
                else
                {
                    CnsysValidatorBase v = new CnsysValidatorBase();
                    return v.ValidateEGN(val);
                }
                
            }).WithEAUErrorCode(ErrorMessagesConstants.InvalidEGN);
            //return ruleBuilder.EAUInjectPropertyValidator<T, TProperty, EGNValidator>().WithEAUErrorCode(ErrorMessagesConstants.InvalidEGN);
        }

        public static IRuleBuilderOptions<T, string> LnchValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must((val) =>
            {
                if (string.IsNullOrEmpty(val))
                {
                    return true;
                }
                else
                {
                    CnsysValidatorBase v = new CnsysValidatorBase();
                    return v.ValidateLNCH(val);
                }
            }).WithEAUErrorCode(ErrorMessagesConstants.InvalidLNCh);
            //return ruleBuilder.EAUInjectPropertyValidator<T, TProperty, LNCHValidator>().WithEAUErrorCode(ErrorMessagesConstants.InvalidLNCh);
        }

        public static IRuleBuilderOptions<T, string> EgnLnchClientValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must((val) =>
            {
                if (string.IsNullOrEmpty(val))
                {
                    return true;
                }
                else
                {
                    CnsysValidatorBase v = new CnsysValidatorBase();
                    return v.ValidateEGN(val) || v.ValidateLNCH(val);
                }
            }).WithEAUErrorCode(ErrorMessagesConstants.InvalidEGNLNCh);
            //return ruleBuilder.EAUInjectPropertyValidator<T, TProperty, EgnLNCHClientValidator>().WithEAUErrorCode(ErrorMessagesConstants.InvalidEGNLNCh);
        }

        public static IRuleBuilderOptions<T, TProperty> GreaterThanOrEqualToValidation<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, TProperty val) where TProperty : IComparable<TProperty>, IComparable
        {
            return ruleBuilder.GreaterThanOrEqualTo(val).WithEAUErrorCode(ErrorMessagesConstants.FiledGreatherThanOrEqual, GetTPropertyAsString(val));
        }

        public static IRuleBuilderOptions<T, TProperty?> GreaterThanOrEqualToValidation<T, TProperty>(this IRuleBuilder<T, TProperty?> ruleBuilder, TProperty val) where TProperty : struct, IComparable<TProperty>, IComparable
        {
            return ruleBuilder.GreaterThanOrEqualTo(val).WithEAUErrorCode(ErrorMessagesConstants.FiledGreatherThanOrEqual, GetTPropertyAsString(val));
        }

        public static IRuleBuilderOptions<T, TProperty> LessThanOrEqualToValidation<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, TProperty val) where TProperty : IComparable<TProperty>, IComparable
        {
            return ruleBuilder.LessThanOrEqualTo(val).WithEAUErrorCode(ErrorMessagesConstants.FiledLessThenOrEqual, GetTPropertyAsString(val));
        }

        public static IRuleBuilderOptions<T, TProperty?> LessThanOrEqualToValidation<T, TProperty>(this IRuleBuilder<T, TProperty?> ruleBuilder, TProperty val) where TProperty : struct, IComparable<TProperty>, IComparable
        {
            return ruleBuilder.LessThanOrEqualTo(val).WithEAUErrorCode(ErrorMessagesConstants.FiledLessThenOrEqual, GetTPropertyAsString(val));
        }

        public static IRuleBuilderOptions<T, TProperty> LessThanValidation<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, TProperty val) where TProperty : IComparable<TProperty>, IComparable
        {
            return ruleBuilder.LessThan(val).WithEAUErrorCode(ErrorMessagesConstants.FiledLessThen, GetTPropertyAsString(val));
        }

        public static IRuleBuilderOptions<T, TProperty?> GreaterThanValidation<T, TProperty>(this IRuleBuilder<T, TProperty?> ruleBuilder, Expression<Func<T, TProperty?>> expression) where TProperty : struct, IComparable<TProperty>, IComparable
        {
            string expresionPropName = ((MemberExpression)expression.Body).Member.Name;
            string expresionPropLabel = GetObjectPropertyResourseCode(typeof(T), expresionPropName);
            string expresionPropNameTranslated = ValidatorOptions.Global.LanguageManager.GetString(expresionPropLabel);;

            return ruleBuilder.GreaterThan(expression)
                .WithEAUErrorCode(ErrorMessagesConstants.FiledGreatherThan, string.Format("\"{0}\"", expresionPropNameTranslated));
        }

        public static IRuleBuilderOptions<T, TProperty> GreaterThanValidation<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Expression<Func<T, TProperty>> expression) where TProperty : struct, IComparable<TProperty>, IComparable
        {
            string expresionPropName = ((MemberExpression)expression.Body).Member.Name;
            string expresionPropLabel = GetObjectPropertyResourseCode(typeof(T), expresionPropName);
            string expresionPropNameTranslated = ValidatorOptions.Global.LanguageManager.GetString(expresionPropLabel); ;

            return ruleBuilder.GreaterThan(expression)
                .WithEAUErrorCode(ErrorMessagesConstants.FiledGreatherThan, string.Format("\"{0}\"", expresionPropNameTranslated));
        }

        public static IRuleBuilderOptions<T, TProperty?> LessThanValidation<T, TProperty>(this IRuleBuilder<T, TProperty?> ruleBuilder, Expression<Func<T, TProperty?>> expression) where TProperty : struct, IComparable<TProperty>, IComparable
        {
            string expresionPropName = ((MemberExpression)expression.Body).Member.Name;
            string expresionPropLabel = GetObjectPropertyResourseCode(typeof(T), expresionPropName);
            string expresionPropNameTranslated = ValidatorOptions.Global.LanguageManager.GetString(expresionPropLabel);

            return ruleBuilder.LessThan(expression)
                .WithEAUErrorCode(ErrorMessagesConstants.FiledLessThen, string.Format("\"{0}\"", expresionPropNameTranslated));
        }

        public static IRuleBuilderOptions<T, string> CountryISO3166TwoLetterCodeValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must((val) =>
            {
                if (string.IsNullOrEmpty(val))
                {
                    return true;
                }
                else
                {
                    return CountryISO3166TwoLetterCodeValidator._codes.Contains(val);
                }
            }).WithEAUErrorCode(ErrorMessagesConstants.InvalidISO3166CountryCode);
        }

        public static IRuleBuilderOptions<T, string> UICBulstatValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must((val) =>
            {
                if (string.IsNullOrEmpty(val))
                {
                    return true;
                }
                else
                {
                    CnsysValidatorBase v = new CnsysValidatorBase();
                    return v.ValidateUICBulstat(val);
                }
            }).WithEAUErrorCode(ErrorMessagesConstants.InvalidBULSTATAndEIK);
            //return ruleBuilder.EAUInjectPropertyValidator<T, TProperty, UICBulstatValidator>().WithEAUErrorCode(ErrorMessagesConstants.InvalidBULSTATAndEIK);
        }

        public static IRuleBuilderOptions<T, string> EgnUICBulstatClientValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must((val) =>
            {
                if (string.IsNullOrEmpty(val))
                {
                    return true;
                }
                else
                {
                    CnsysValidatorBase v = new CnsysValidatorBase();
                    return v.ValidateEGN(val) || v.ValidateUICBulstat(val);
                }
            }).WithEAUErrorCode(ErrorMessagesConstants.InvalidEGNLNCh);
            //return ruleBuilder.EAUInjectPropertyValidator<T, TProperty, EgnLNCHClientValidator>().WithEAUErrorCode(ErrorMessagesConstants.InvalidEGNLNCh);
        }

        public static IRuleBuilderOptions<T, TProperty> NumericDataTypeValidation<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.Must((val) => CnsysValidatorBase.IsNumericType(val))
                .WithEAUErrorCode(ErrorMessagesConstants.DefaultNonNumericErrorText);
            //return ruleBuilder.EAUInjectPropertyValidator<T, TProperty, NumericDataTypeValidator>().WithEAUErrorCode(ErrorMessagesConstants.DefaultNonNumericErrorText);
        }

        public static IRuleBuilderOptions<T, TProperty> SetErrorLevelStrict<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule)
        {
            return rule.WithSeverity(Severity.Error);
        }

        public static IRuleBuilderOptions<T, TProperty> SetErrorLevelInformation<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule)
        {
            return rule.WithSeverity(Severity.Info);
        }

        public static IRuleBuilderOptions<T, string> PhoneValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must((val) =>
            {
                if (string.IsNullOrEmpty(val))
                {
                    return true;
                }
                else
                {
                    CnsysValidatorBase v = new CnsysValidatorBase();
                    return v.PhoneValidation(val);
                }
            }).WithEAUErrorCode(ErrorMessagesConstants.InvalidPhone);
            //return ruleBuilder.EAUInjectPropertyValidator<T, TProperty, PhoneValidator>().WithEAUErrorCode(ErrorMessagesConstants.InvalidPhone);
        }

        public static IRuleBuilderOptions<T, string> EmailValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.EmailAddress().WithEAUErrorCode(ErrorMessagesConstants.FieldMustContainsValidEmailAddress);
        }

        public static IRuleBuilderOptions<T, string> LengthValidatior<T>(this IRuleBuilder<T, string> ruleBuilder, int length)
        {
            return ruleBuilder.Length(length).WithEAUErrorCode(ErrorMessagesConstants.FieldMustContainsExaclyNumSymbols, length.ToString());
        }

        public static IRuleBuilderOptions<T, string> RangeLengthValidatior<T>(this IRuleBuilder<T, string> ruleBuilder, int min, int max)
        {
            return ruleBuilder.Length(min, max).WithEAUErrorCode(ErrorMessagesConstants.FieldCanNotContainsLessThanOrMoreThanSymbols, min.ToString(), max.ToString());
        }

        public static IRuleBuilderOptions<T, string> MinLengthValidatior<T>(this IRuleBuilder<T, string> ruleBuilder, int min)
        {
            return ruleBuilder.Length(min, 2147483647).WithEAUErrorCode(ErrorMessagesConstants.DefaultBottomRangeLengthErrorMessage, min.ToString());
        }

        public static IRuleBuilderOptions<T, string> MaxLengthValidatior<T>(this IRuleBuilder<T, string> ruleBuilder, int max)
        {
            return ruleBuilder.Length(0, max).WithEAUErrorCode(ErrorMessagesConstants.DefaultMaxLengthErrorMessage, max.ToString());
        }

        public static IRuleBuilderOptions<T, string> RequiredLengthValidatior<T>(this IRuleBuilder<T, string> ruleBuilder, int lenght)
        {
            return ruleBuilder.Length(lenght).WithEAUErrorCode(ErrorMessagesConstants.FieldMustContainsExaclyNumSymbols, lenght.ToString());
        }

        public static IRuleBuilderOptions<T, string> MatchesValidatior<T>(this IRuleBuilder<T, string> ruleBuilder, string expression, string errorCode = null, params string[] parameters)
        {
            return ruleBuilder.Matches(expression).WithEAUErrorCode(string.IsNullOrEmpty(errorCode) ? ErrorMessagesConstants.DefaultRegexErrorMessage : errorCode, parameters);
        }

        public static IRuleBuilderOptions<T, string> CyrillicNameValidatior<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.MatchesValidatior(@"^[а-яА-Я]+([а-яА-Я '\-]+[а-яА-Я]){0,1}$", ErrorMessagesConstants.FieldCyrillicNameMustContainsSymbols);
        }

        public static IRuleBuilderOptions<T, string> LatinNameValidatior<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.MatchesValidatior(@"^[A-Za-z]+(([\' A-Za-z\-]+)*[A-Za-z]{1}){0,1}$", ErrorMessagesConstants.FieldLatinNameMustContainsSymbols);
        }

        public static IRuleBuilderOptions<T, TProperty> RequiredSection<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            Type propertyType = typeof(TProperty);

            return GetRequierdField(ruleBuilder, propertyType).WithEAUErrorCode(ErrorMessagesConstants.RequierFillSection);
        }

        public static IRuleBuilderOptions<T, TProperty> RequiredFieldFromSection<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            Type propertyType = typeof(TProperty);

            return GetRequierdField(ruleBuilder, propertyType)
                .WithEAUErrorCode(ErrorMessagesConstants.RequierFillFieldFromSection);
        }

        public static IRuleBuilderOptions<T, TProperty> RequiredField<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            Type propertyType = typeof(TProperty);

            return GetRequierdField(ruleBuilder, propertyType)
                .WithEAUErrorCode(ErrorMessagesConstants.DefaultNotEmptyErrorMessage);
        }

        public static IRuleBuilderOptions<T, TProperty> RequiredXmlElement<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder.NotNull().WithEAUErrorCode(ErrorMessagesConstants.InvalidXmlElement);
        }

        public static IRuleBuilderOptions<T, TProperty> CollectionWithOneElement<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder) where TProperty : ICollection
        {
            return ruleBuilder.Must((obj) =>
            {
                return obj != null && obj.Count == 1;
            }).WithEAUErrorCode(ErrorMessagesConstants.RequierdOnlyOneElementInCollection);
        }

        public static IRuleBuilderOptions<T, TProperty> CollectionWithAtLeastOneElement<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder) where TProperty : ICollection
        {
            return ruleBuilder.Must((obj) =>
            {
                return obj != null && obj.Count >= 1;
            }).WithEAUErrorCode(ErrorMessagesConstants.RequierdAtleastOneElementInCollection);
        }

        public static IRuleBuilderOptions<T, TProperty> RequiredAtLeastOneOfTwoField<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Expression<Func<T, TProperty>> secondFildExpression)
        {
            string secondFieldName = ((MemberExpression)secondFildExpression.Body).Member.Name;
            string secondFieldLabel = GetObjectPropertyResourseCode(typeof(T), secondFieldName);
            string secondFieldNameTranslated = ValidatorOptions.Global.LanguageManager.GetString(secondFieldLabel);

            return ruleBuilder.Must((obj, propVal) =>
            {
                bool firstIsEmpty = CnsysValidatorBase.IsObjectEmpty<TProperty>(propVal);

                var secondFieldFunc = secondFildExpression.Compile();
                var secondFieldValue = secondFieldFunc(obj);

                bool secondIsEmpty = CnsysValidatorBase.IsObjectEmpty<TProperty>(secondFieldValue);

                return !firstIsEmpty || !secondIsEmpty;
            }).WithEAUErrorCode(ErrorMessagesConstants.RequiredAtLeastOneOfTwoFields, secondFieldNameTranslated);
        }

        public static IRuleBuilderOptions<T, string> BirthDateValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must((val) =>
            {
                if (string.IsNullOrEmpty(val))
                {
                    return true;
                }
                else
                {
                    CnsysValidatorBase v = new CnsysValidatorBase();
                    return v.isValidBirthDate(val);
                }

            }).WithEAUErrorCode(ErrorMessagesConstants.InvalidBirthDate);
            //return ruleBuilder.EAUInjectPropertyValidator<T, TProperty, EGNValidator>().WithEAUErrorCode(ErrorMessagesConstants.InvalidEGN);
        }

        #region Helper

        private static string GetResourcePrefixCode(Type objType)
        {
            var nameSpace = objType.AssemblyQualifiedName;
            var nameSpaceSplited = nameSpace.Split('.');

            if (nameSpaceSplited.Contains("PBZN"))
                return "DOC_PBZN_";
            else if (nameSpaceSplited.Contains("Migr"))
                return "DOC_MIGR_";
            else if (nameSpaceSplited.Contains("KAT"))
                return "DOC_KAT_";
            else if (nameSpaceSplited.Contains("COD"))
                return "DOC_COD_";
            else if (nameSpaceSplited.Contains("BDS"))
                return "DOC_BDS_";
            else
                return "DOC_GL_";
        }

        public static string GetObjectResourseCode(Type objType)
        {
            string prefix = GetResourcePrefixCode(objType);
            string objTypeName = objType.Name;

            return string.Format("{0}{1}_L", prefix, objTypeName);
        }

        public static string GetObjectPropertyResourseCode(Type objType, string propName)
        {
            string prefix = GetResourcePrefixCode(objType);
            string objTypeName = objType.Name;

            return string.Format("{0}{1}_{2}_L", prefix, objTypeName, propName);
        }

        private static IRuleBuilderOptions<T, TProperty> GetRequierdField<T, TProperty>(IRuleBuilder<T, TProperty> ruleBuilder, Type fieldType)
        {
            if (Nullable.GetUnderlyingType(fieldType) != null)
            {
                return ruleBuilder.NotNull();
            }
            else if (fieldType.IsEnum)
            {
                return ruleBuilder.IsInEnum();
            }
            else if (fieldType.IsValueType || fieldType == typeof(string))
            {
                return ruleBuilder.NotEmpty();
            }
            else
            {
                return ruleBuilder.NotNull();
            }
        }

        private static string GetTPropertyAsString<Tproperty>(Tproperty tproperty)
        {
            if (tproperty is DateTime dt)
            {
                return dt.ToShortDateString();
            }
            else 
            {
                return tproperty.ToString();
            }
        }

        #endregion
    }
}
