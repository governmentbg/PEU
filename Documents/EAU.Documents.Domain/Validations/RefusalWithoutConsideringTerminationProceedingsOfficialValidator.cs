using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class RefusalWithoutConsideringTerminationProceedingsOfficialValidator : EAUValidator<RefusalWithoutConsideringTerminationProceedingsOfficial>
    {
        public RefusalWithoutConsideringTerminationProceedingsOfficialValidator()
        {
            EAURuleFor(m => m.ElectronicDocumentAuthorQuality).RequiredField();
            EAURuleFor(m => m.XMLDigitalSignature).RequiredField();
        }
    }
}