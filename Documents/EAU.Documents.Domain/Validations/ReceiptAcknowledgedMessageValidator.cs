using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ReceiptAcknowledgedMessageValidator : EAUValidator<ReceiptAcknowledgedMessage>
    {
        public ReceiptAcknowledgedMessageValidator()
        {
            EAURuleFor(m => m.ElectronicServiceProvider).RequiredFieldFromSection().EAUInjectValidator();
            EAURuleFor(m => m.DocumentURI).RequiredFieldFromSection().EAUInjectValidator();
            EAURuleFor(m => m.RegisteredBy).RequiredFieldFromSection();
            EAURuleFor(m => m.CaseAccessIdentifier).RequiredFieldFromSection().MinLengthValidatior(1);
            EAURuleFor(m => m.Applicant).RequiredFieldFromSection().EAUInjectValidator();
            EAURuleFor(m => m.DocumentTypeURI).EAUInjectValidator();
            EAURuleFor(m => m.DocumentTypeName).MinLengthValidatior(1);
            EAURuleFor(m => m.TransportType).RequiredFieldFromSection();
            EAURuleFor(m => m.Signature).RequiredFieldFromSection();
        }
    }
}
