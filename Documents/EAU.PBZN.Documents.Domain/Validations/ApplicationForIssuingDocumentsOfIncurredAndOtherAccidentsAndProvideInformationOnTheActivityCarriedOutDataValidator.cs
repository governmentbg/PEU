using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.PBZN.Documents.Domain.Models;

namespace EAU.PBZN.Documents.Domain.Validations
{
    public class ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataValidator : EAUValidator<ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData>
    {
        public ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutDataValidator()
        {
            EAURuleFor(m => m.EntityManagementAddress).EAUInjectValidator();
            EAURuleFor(m => m.CorespondingAddress).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.DocumentCertifyingTheAccidentOccurredOrOtherInformation).RequiredField();
            EAURuleFor(m => m.DocumentMustServeTo).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.PhoneNumber).PhoneValidation();
        }
    }
}
