using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Migr.Documents.Domain.Models.Forms;

namespace EAU.Migr.Documents.Domain.Validations
{
    public class ApplicationForIssuingDocumentForForeignersValidator : EAUValidator<ApplicationForIssuingDocumentForForeigners>
    {
        public ApplicationForIssuingDocumentForForeignersValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();           
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            EAURuleFor(m => m.ApplicationForIssuingDocumentForForeignersData).RequiredSection().EAUInjectValidator();
            EAURuleForEach(m => m.Declarations).EAUInjectValidator();
            EAURuleForEach(m => m.AttachedDocuments).EAUInjectValidator();
        }
    }
}
