using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace EAU.Documents.Domain.Validations
{
    public class IdentityDocumentForeignCitizenBasicDataValidator : EAUValidator<IdentityDocumentForeignCitizenBasicData>
    {
        public IdentityDocumentForeignCitizenBasicDataValidator()
        {
            EAURuleFor(m => m.IdentityNumber).RequiredFieldFromSection();
            EAURuleFor(m => m.IdentityIssuer).RequiredFieldFromSection();
            EAURuleFor(m => m.IdentitityIssueDate).RequiredFieldFromSection();
   
            EAURuleFor(m => m.IdentitityExpireDate)
                .RequiredFieldFromSection()
                .GreaterThanValidation(m => m.IdentitityIssueDate);
        }

        public override ValidationResult Validate(ValidationContext<IdentityDocumentForeignCitizenBasicData> context)
        {
            ValidationResult validationRes = base.Validate(context);
            var instance = context.InstanceToValidate;

            DateTime toDayEndOfDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);

            if (instance.IdentitityIssueDate != null && instance.IdentitityIssueDate > toDayEndOfDay)
            {
                AddValidationFailureWithoutParamsTranslation(validationRes
                        , ErrorMessagesConstants.FiledLessThenOrEqual
                        , new EAUValidator<IdentityDocumentForeignCitizenBasicData>.PlaceHolder("Param1", toDayEndOfDay.ToShortDateString()));
            }

            return validationRes;
        }
    }
}