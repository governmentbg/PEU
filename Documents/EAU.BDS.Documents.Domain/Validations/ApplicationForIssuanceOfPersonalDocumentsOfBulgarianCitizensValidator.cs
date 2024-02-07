using EAU.BDS.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using FluentValidation.Results;
using EAU.Documents.Domain.Validations;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensValidator : EAUValidator<ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizens>
    {
        public ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            EAURuleFor(m => m.IdentificationPhotoAndSignature).EAUInjectValidator();
            EAURuleFor(m => m.ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData).EAUInjectValidator();
            EAURuleFor(m => m.IssuingPoliceDepartment).EAUInjectValidator();
            EAURuleForEach(m => m.AttachedDocuments).EAUInjectValidator();
        }

        public override ValidationResult Validate(ValidationContext<ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizens> context)
        {
            ValidationResult validationRes = base.Validate(context);
            var instance = context.InstanceToValidate;

            if (instance.ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData != null 
                && instance.ApplicationForIssuanceOfPersonalDocumentsOfBulgarianCitizensData.HasDocumentForDisabilities.GetValueOrDefault()
                && instance.AttachedDocuments != null
                && instance.AttachedDocuments.Count <= 0)
            {
                AddValidationFailure(validationRes, ErrorMessagesConstants.RequireDocumentForDisabilities);
            }

            return validationRes;
        }
    }
}