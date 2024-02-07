using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{    
    public class OutstandingConditionsForWithdrawServiceMessageValidator : EAUValidator<OutstandingConditionsForWithdrawServiceMessage>
    {
        public OutstandingConditionsForWithdrawServiceMessageValidator()
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
            EAURuleFor(m => m.DocumentTypeURI)
                .RequiredField()
                .EAUInjectValidator(); //DocumentTypeURIValidator
            EAURuleFor(m => m.DocumentTypeName).RequiredField();
            EAURuleFor(m => m.AISCaseURI).RequiredXmlElement();
            //EAURuleFor(m => m.OutstandingConditionsForStartOfServiceMessageHeader).RequiredField();
            EAURuleFor(m => m.Grounds).RequiredField();
            EAURuleForEach(m => m.Grounds).EAUInjectValidator();
            // EAURuleFor(m => m.AdministrativeBodyName).RequiredField();
            EAURuleFor(m => m.Signature).RequiredField();
        }
    }
}
