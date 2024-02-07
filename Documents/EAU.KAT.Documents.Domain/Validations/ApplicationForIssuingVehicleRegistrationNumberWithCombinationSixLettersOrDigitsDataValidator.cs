using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using FluentValidation;
using FluentValidation.Results;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsDataValidator : EAUValidator<ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData>
    {
        public ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsDataValidator()
        {
            EAURuleFor(m => m.IssuingPoliceDepartment).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.PlatesTypeCode).RequiredField();
            EAURuleFor(m => m.PlatesTypeName).RequiredField();
            EAURuleFor(m => m.PlatesContentType).RequiredField();
            EAURuleFor(m => m.AISKATVehicleTypeCode).RequiredField();
            EAURuleFor(m => m.WishedRegistrationNumber).RequiredField();
        }

        public override ValidationResult Validate(ValidationContext<ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData> context)
        {
            ValidationResult res = base.Validate(context);
            ApplicationForIssuingVehicleRegistrationNumberWithCombinationSixLettersOrDigitsData obj = context.InstanceToValidate;

            if (obj.RectangularPlatesCount.HasValue && obj.SquarePlatesCount.HasValue)
            {
                if (obj.AISKATVehicleTypeCode == "8404" && (obj.RectangularPlatesCount + obj.SquarePlatesCount) != 1)
                    AddValidationFailure(res, "Общия брой на табелите от Тип1 и Тип2 трябва да бъде 1.");
                else
                    if (obj.AISKATVehicleTypeCode != "8404" && (obj.RectangularPlatesCount + obj.SquarePlatesCount) != 2)
                    AddValidationFailure(res, "Общия брой на табелите от Тип1 и Тип2 трябва да бъде 2.");
            }

            return res;
        }
    }
}