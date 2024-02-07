using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class IndividualAdministrativeActRefusalOfficialValidator : EAUValidator<IndividualAdministrativeActRefusalOfficial>
    {
        public IndividualAdministrativeActRefusalOfficialValidator()
        {
            EAURuleFor(m => m.ElectronicDocumentAuthorQuality).RequiredField();
            EAURuleFor(m => m.XMLDigitalSignature).RequiredField();
        }
    }
}