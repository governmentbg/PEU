using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class VehicleRegistrationDataValidator : EAUValidator<VehicleRegistrationData>
    {
        public VehicleRegistrationDataValidator()
        {
            RuleSet("Full", () => {
                EAURuleFor(m => m.RegistrationNumber)
                .RequiredFieldFromSection()
                .MatchesValidatior("^[А-Я0-9]+$", ErrorMessagesConstants.FieldCanContainsUpperCyrillicLettersAndDigits);

                EAURuleFor(m => m.IdentificationNumber)
                .RequiredField()
                .MinLengthValidatior(1)
                .MatchesValidatior("^[A-HJ-NPR-Z0-9]+$", ErrorMessagesConstants.FieldCanContainsOnly, "главни букви на латиница без I,O и Q и цифри");

                EAURuleFor(m => m.RegistrationCertificateType)
                    .RequiredFieldFromSection();

                EAURuleFor(m => m.RegistrationCertificateNumber).RequiredField();

                EAURuleFor(m => m.RegistrationCertificateNumber)
                    .MatchesValidatior(@"^[0-9]+$", ErrorMessagesConstants.OnlyDigitsAllowed)
                    .RequiredLengthValidatior(9)
                    .When(m => m.RegistrationCertificateType == RegistrationCertificateTypeNomenclature.RegistrationDocument);
                EAURuleFor(m => m.RegistrationCertificateNumber)
                    .MatchesValidatior("^[А-Я0-9]+$", ErrorMessagesConstants.FieldCanContainsUpperCyrillicLettersAndDigits)
                    .RangeLengthValidatior(1, 12)
                    .When(m => m.RegistrationCertificateType == RegistrationCertificateTypeNomenclature.RegistrationCertificate);

                EAURuleFor(m => m.RegAddress).EAUInjectValidator();
                EAURuleFor(m => m.PoliceDepartment).EAUInjectValidator();
            });

            RuleSet("RoadVehicleRegistrationData", () => {
                EAURuleFor(m => m.RegistrationNumber)
                .RequiredFieldFromSection()
                .MatchesValidatior("^[А-Я0-9]+$", ErrorMessagesConstants.FieldCanContainsUpperCyrillicLettersAndDigits);

                EAURuleFor(m => m.RegistrationCertificateType)
                    .RequiredFieldFromSection();

                EAURuleFor(m => m.RegistrationCertificateNumber).RequiredField();

                EAURuleFor(m => m.RegistrationCertificateNumber)
                    .MatchesValidatior(@"^[0-9]+$", ErrorMessagesConstants.OnlyDigitsAllowed)
                    .RequiredLengthValidatior(9)
                    .When(m => m.RegistrationCertificateType == RegistrationCertificateTypeNomenclature.RegistrationDocument);
                EAURuleFor(m => m.RegistrationCertificateNumber)
                    .MatchesValidatior("^[А-Я0-9]+$", ErrorMessagesConstants.FieldCanContainsUpperCyrillicLettersAndDigits)
                    .RangeLengthValidatior(1, 12)
                    .When(m => m.RegistrationCertificateType == RegistrationCertificateTypeNomenclature.RegistrationCertificate);
            });

            RuleSet("RoadVehicleRegistrationDataWithoutType", () => {
                EAURuleFor(m => m.RegistrationNumber)
                    .RequiredFieldFromSection()
                    .MatchesValidatior("^[А-Я0-9]+$", ErrorMessagesConstants.FieldCanContainsUpperCyrillicLettersAndDigits);

                EAURuleFor(m => m.RegistrationCertificateNumber)
                    .RequiredField()
                    .MatchesValidatior(@"^[0-9]+$", ErrorMessagesConstants.OnlyDigitsAllowed)
                    .RequiredLengthValidatior(9);
            });
        }
    }
}