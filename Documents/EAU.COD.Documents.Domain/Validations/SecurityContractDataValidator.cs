using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using FluentValidation.Results;
using System;

namespace EAU.COD.Documents.Domain.Validations
{
    public class SecurityContractDataValidator : EAUValidator<SecurityContractData>
    {
        public SecurityContractDataValidator()
        {
            EAURuleFor(m => m.ContractDate).RequiredField();

            EAURuleFor(m => m.ContractType).MaxLengthValidatior(50);
        }
        public override ValidationResult Validate(ValidationContext<SecurityContractData> context)
        {
            ValidationResult validationRes = base.Validate(context);
            var instance = context.InstanceToValidate;

            if (validationRes.IsValid)
            {
                DateTime toDayEndOfDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 23, 59, 59);
                if (instance.ContractDate > toDayEndOfDay)
                {
                    EAUValidator<SecurityContractData>.PlaceHolder[] parameters = new EAUValidator<SecurityContractData>.PlaceHolder[]
                   {
                        new EAUValidator<SecurityContractData>.PlaceHolder("Field",FluentValidationExtensions.GetObjectPropertyResourseCode(instance.GetType(), "ContractDate")),
                        new EAUValidator<SecurityContractData>.PlaceHolder("Param1", toDayEndOfDay.ToShortDateString())
                   };

                    AddValidationFailureWithoutParamsTranslation(validationRes, ErrorMessagesConstants.FiledLessThenOrEqual, parameters);
                }
            }

            return validationRes;
        }
    }
}