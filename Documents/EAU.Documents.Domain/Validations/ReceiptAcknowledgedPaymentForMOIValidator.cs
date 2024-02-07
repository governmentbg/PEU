using EAU.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ReceiptAcknowledgedPaymentForMOIValidator : EAUValidator<ReceiptAcknowledgedPaymentForMOI>
    {
        public ReceiptAcknowledgedPaymentForMOIValidator()
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
            EAURuleFor(m => m.BankName)
                .RequiredField()
                .RangeLengthValidatior(1, 100);
            EAURuleFor(m => m.BIC)
                .RequiredField()
                .RangeLengthValidatior(8, 11);
            EAURuleFor(m => m.IBAN)
                .RequiredField()
                .RangeLengthValidatior(15, 34);
            EAURuleFor(m => m.PaymentReason)
                .RequiredField()
                .MinLengthValidatior(1);

            When(x => x.Amount.HasValue, () =>
            {
                EAURuleFor(m => m.Amount)
                .GreaterThanOrEqualToValidation(0);
            });

            EAURuleFor(m => m.AmountCurrency)
                .RequiredField()
                .MatchesValidatior("[A-Z]{3}");
            EAURuleFor(m => m.ReceiptAcknowledgedPaymentMessage)
                .RequiredField()
                .MinimumLength(1);
            EAURuleFor(m => m.AdministrativeBodyName)
                .RequiredField()
                .MinimumLength(1);
            EAURuleFor(m => m.Signature)
                .RequiredField();
        }
    }
}