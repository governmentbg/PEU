using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class RefusalWithoutConsideringTerminationProceedingsValidator : EAUValidator<RefusalWithoutConsideringTerminationProceedings>
    {
        public RefusalWithoutConsideringTerminationProceedingsValidator()
        {
            EAURuleFor(m => m.ElectronicServiceProviderBasicData)
               .RequiredField()
               .RequiredXmlElement()
               .EAUInjectValidator(); //ElectronicServiceProviderBasicDataValidator
            EAURuleFor(m => m.ElectronicServiceApplicant)
                .RequiredField()
                .RequiredXmlElement()
                .EAUInjectValidator(); //ElectronicServiceApplicantValidator
            EAURuleFor(m => m.DocumentURI)
                .RequiredField()
                .RequiredXmlElement()
                .EAUInjectValidator(); //DocumentURIValidator
            EAURuleFor(m => m.DocumentTypeURI).RequiredField().EAUInjectValidator(); //DocumentTypeURIValidator
            EAURuleFor(m => m.DocumentTypeName).RequiredField();
            EAURuleFor(m => m.AISCaseURI).RequiredXmlElement();
            EAURuleFor(m => m.DocumentReceiptOrSigningDate).RequiredFieldFromSection();
            EAURuleFor(m => m.IndividualAdministrativeActRefusalHeader).RequiredSection();
            EAURuleFor(m => m.IndividualAdministrativeActRefusalFactualGround).RequiredSection();
            EAURuleFor(m => m.IndividualAdministrativeActRefusalAppealTerm).RequiredSection();
            EAURuleFor(m => m.IndividualAdministrativeActRefusalAppealAuthority).RequiredSection();
            EAURuleFor(m => m.AdministrativeBodyName).RequiredSection();
            EAURuleFor(m => m.Official).RequiredField();
            EAURuleForEach(m => m.Official).EAUInjectValidator();
        }
    }
}