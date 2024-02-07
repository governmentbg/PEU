using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class DocumentsWillBeIssuedMessageValidator : EAUValidator<DocumentsWillBeIssuedMessage>
    {
        public DocumentsWillBeIssuedMessageValidator()
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
            EAURuleFor(m => m.IdentityDocumentsWillBeIssuedMessage)
                .MinLengthValidatior(1)
                .RequiredField();
            EAURuleFor(m => m.PoliceDepartment)
                .RequiredField()
                .EAUInjectValidator(); //PoliceDepartmentValidator
            EAURuleFor(m => m.AdministrativeBodyName).RequiredField();
            EAURuleFor(m => m.Signature).RequiredField();
        }
    }
}