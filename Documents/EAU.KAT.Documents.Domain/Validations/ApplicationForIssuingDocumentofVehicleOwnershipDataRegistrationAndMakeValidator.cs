using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForIssuingDocumentofVehicleOwnershipDataRegistrationAndMakeValidator : EAUValidator<ApplicationForIssuingDocumentofVehicleOwnershipDataRegistrationAndMake>
    {
        public ApplicationForIssuingDocumentofVehicleOwnershipDataRegistrationAndMakeValidator()
        {
            EAURuleFor(m => m.RegistrationNumber).RequiredField();
            When(m => !string.IsNullOrEmpty(m.RegistrationNumber), () =>
            {
                EAURuleFor(m => m.RegistrationNumber)
                    .MatchesValidatior("^[А-Я0-9]+$", ErrorMessagesConstants.FieldCanContainsOnly, "главни букви на кирилица и цифри");
            });

            EAURuleFor(m => m.MakeModel).RequiredField();
            When(m => !string.IsNullOrEmpty(m.MakeModel), () =>
            {
                EAURuleFor(m => m.MakeModel)
                    .MatchesValidatior("^[А-Яа-яa-zA-Z0-9\\s\\.]+$", ErrorMessagesConstants.FieldCanContainsOnly, "букви на кирилица, букви на латиница, интервал, арабски цифри и точка");
            });
        }
    }
}