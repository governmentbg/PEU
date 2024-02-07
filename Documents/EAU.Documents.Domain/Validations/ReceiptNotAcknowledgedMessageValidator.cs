using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ReceiptNotAcknowledgedMessageValidator : EAUValidator<ReceiptNotAcknowledgedMessage>
    {
        public ReceiptNotAcknowledgedMessageValidator()
        {
            EAURuleFor(m => m.MessageURI).EAUInjectValidator();
            EAURuleFor(m => m.ElectronicServiceProvider).EAUInjectValidator();
            EAURuleFor(m => m.Applicant).EAUInjectValidator();
            EAURuleFor(m => m.DocumentTypeURI).EAUInjectValidator();
            EAURuleFor(m => m.DocumentTypeName).MinLengthValidatior(1);

            //Нес се добавя за XMLDigitalSignature, защото генератора беше изгенерирал празен валидатор.
        }
    }
}