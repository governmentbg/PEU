using EAU.BDS.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using FluentValidation.Results;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class ApplicationForIssuingDocumentValidator : EAUValidator<ApplicationForIssuingDocument>
    {
        public ApplicationForIssuingDocumentValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            EAURuleFor(m => m.ApplicationForIssuingDocumentData).EAUInjectValidator();
            EAURuleForEach(m => m.Declarations).EAUInjectValidator();
            EAURuleForEach(m => m.AttachedDocuments).EAUInjectValidator();
        }

        public override ValidationResult Validate(ValidationContext<ApplicationForIssuingDocument> context)
        {
            ValidationResult validationRes = base.Validate(context);
            var instance = context.InstanceToValidate;

            //REQ_N1_PEAU_0020 Автоматично определяне на начина на получаване на удостоверение, когато заявител е избрал, че ще го използва в чужбина.
            if (instance.ApplicationForIssuingDocumentData?.DocumentToBeIssuedFor?.DocumentMustServeTo?.ItemElementName != null
                && instance.ApplicationForIssuingDocumentData.DocumentToBeIssuedFor.DocumentMustServeTo.ItemElementName == EAU.Documents.Domain.Models.ItemChoiceType1.AbroadDocumentMustServeTo
                && (instance.ServiceApplicantReceiptData == null 
                    || instance.ServiceApplicantReceiptData.ServiceResultReceiptMethod == null
                    || instance.ServiceApplicantReceiptData.ServiceResultReceiptMethod.Value != EAU.Documents.Domain.Models.ServiceResultReceiptMethods.UnitInAdministration))
            {
                AddValidationFailure(validationRes, "DOC_BDS_CERTIFICATE_CAN_BE OBTAINED_ONLY_ON_COUNTER_E");
            }

            return validationRes;
        }
    }
}
