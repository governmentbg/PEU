using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class IssuedBulgarianIdentityDocumentsInPeriodValidator : EAUValidator<IssuedBulgarianIdentityDocumentsInPeriod>
    {
        public IssuedBulgarianIdentityDocumentsInPeriodValidator()
        {
            EAURuleFor(x => x.IdentitityIssueDate)
                .RequiredField();

            EAURuleFor(x => x.IdentitityExpireDate)
                .RequiredField();
        }

        public override ValidationResult Validate(ValidationContext<IssuedBulgarianIdentityDocumentsInPeriod> context)
        {
            ValidationResult validationRes = base.Validate(context);
            var instance = context.InstanceToValidate;

            if(validationRes.IsValid) //за бързодействие.
            {
                DateTime toDayEndOfDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                DateTime startIssueDate = new DateTime(2000, 01, 01, 00, 00, 00);

                if (instance.IdentitityIssueDate > toDayEndOfDay)
                {
                    EAUValidator<IssuedBulgarianIdentityDocumentsInPeriod>.PlaceHolder[] parameters = new EAUValidator<IssuedBulgarianIdentityDocumentsInPeriod>.PlaceHolder[]
                    {
                        new EAUValidator<IssuedBulgarianIdentityDocumentsInPeriod>.PlaceHolder("Field",FluentValidationExtensions.GetObjectPropertyResourseCode(instance.GetType(), "IdentitityIssueDate")),
                        new EAUValidator<IssuedBulgarianIdentityDocumentsInPeriod>.PlaceHolder("Param1", toDayEndOfDay.ToShortDateString())
                    }; 

                    AddValidationFailureWithoutParamsTranslation(validationRes, ErrorMessagesConstants.FiledLessThenOrEqual, parameters);
                }

                if (instance.IdentitityIssueDate < startIssueDate)
                {
                    EAUValidator<IssuedBulgarianIdentityDocumentsInPeriod>.PlaceHolder[] parameters = new EAUValidator<IssuedBulgarianIdentityDocumentsInPeriod>.PlaceHolder[]
                    {
                        new EAUValidator<IssuedBulgarianIdentityDocumentsInPeriod>.PlaceHolder("Field",FluentValidationExtensions.GetObjectPropertyResourseCode(instance.GetType(), "IdentitityIssueDate")),
                        new EAUValidator<IssuedBulgarianIdentityDocumentsInPeriod>.PlaceHolder("Param1", startIssueDate.ToShortDateString())
                    };

                    AddValidationFailureWithoutParamsTranslation(validationRes,ErrorMessagesConstants.FiledGreatherThanOrEqual, parameters);
                }

                if (instance.IdentitityIssueDate > instance.IdentitityExpireDate)
                    AddValidationFailure(validationRes, "DOC_BDS_PERIOD_START_DATE_MUST_LESS_E");

                if (instance.IdentitityExpireDate > toDayEndOfDay)
                {
                    EAUValidator<IssuedBulgarianIdentityDocumentsInPeriod>.PlaceHolder[] parameters = new EAUValidator<IssuedBulgarianIdentityDocumentsInPeriod>.PlaceHolder[]
                    {
                        new EAUValidator<IssuedBulgarianIdentityDocumentsInPeriod>.PlaceHolder("Field",FluentValidationExtensions.GetObjectPropertyResourseCode(instance.GetType(), "IdentitityExpireDate")),
                        new EAUValidator<IssuedBulgarianIdentityDocumentsInPeriod>.PlaceHolder("Param1", toDayEndOfDay.ToShortDateString())
                    };

                    AddValidationFailureWithoutParamsTranslation(validationRes, ErrorMessagesConstants.FiledLessThenOrEqual, parameters);
                }
            }

            return validationRes;
        }
    }
}