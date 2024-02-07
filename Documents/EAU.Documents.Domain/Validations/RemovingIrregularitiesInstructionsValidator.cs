using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class RemovingIrregularitiesInstructionsValidator : EAUValidator<RemovingIrregularitiesInstructions>
    {
        public RemovingIrregularitiesInstructionsValidator()
        {
            EAURuleFor(m => m.ElectronicServiceProviderBasicData).RequiredField().EAUInjectValidator(); //ElectronicServiceProviderBasicDataValidator
            EAURuleFor(m => m.ElectronicServiceApplicant).RequiredField().EAUInjectValidator(); //ElectronicServiceApplicantValidator
            EAURuleFor(m => m.DocumentTypeURI).RequiredField().EAUInjectValidator(); //DocumentTypeURIValidator
            EAURuleFor(m => m.DocumentTypeName).RequiredField();
            EAURuleFor(m => m.RemovingIrregularitiesInstructionsHeader).RequiredField();
            EAURuleFor(m => m.ApplicationDocumentURI).RequiredField().EAUInjectValidator();  //DocumentURIValidator
            EAURuleFor(m => m.AISCaseURI).RequiredField();
            EAURuleFor(m => m.Irregularities).RequiredField();
            EAURuleFor(m => m.Irregularities).CollectionWithAtLeastOneElement();
            EAURuleForEach(m => m.Irregularities).EAUInjectValidator();
            EAURuleFor(m => m.DeadlineCorrectionIrregularities).RequiredField();
            EAURuleFor(m => m.AdministrativeBodyName).RequiredField();
        }
    }
}