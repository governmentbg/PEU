using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class ApplicationForIssuingDocumentDataValidator : EAUValidator<ApplicationForIssuingDocumentData>
    {
        public ApplicationForIssuingDocumentDataValidator()
        {
            EAURuleFor(m => m.PersonalInformation).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.DocumentToBeIssuedFor).RequiredSection().EAUInjectValidator();
        }
    }
}