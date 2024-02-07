using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;
using FluentValidation;
using FluentValidation.Results;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class ApplicationForIssuingDriverLicenseValidator : EAUValidator<ApplicationForIssuingDriverLicense>
    {
        public ApplicationForIssuingDriverLicenseValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            EAURuleFor(m => m.IdentificationPhotoAndSignature).EAUInjectValidator();
            EAURuleFor(m => m.ApplicationForIssuingDriverLicenseData).EAUInjectValidator();
            EAURuleFor(m => m.IssuingPoliceDepartment).EAUInjectValidator();
            EAURuleForEach(m => m.AttachedDocuments).EAUInjectValidator();
        }

        public override ValidationResult Validate(ValidationContext<ApplicationForIssuingDriverLicense> context)
        {
            ValidationResult validationRes = base.Validate(context);
            var instance = context.InstanceToValidate;

            if (instance.ApplicationForIssuingDriverLicenseData != null 
                && instance.ApplicationForIssuingDriverLicenseData.HasDocumentForDisabilities.GetValueOrDefault()
                && instance.AttachedDocuments != null
                && instance.AttachedDocuments.Count <= 0)
            {
                AddValidationFailure(validationRes, ErrorMessagesConstants.RequireDocumentForDisabilities);
            }

            return validationRes;
        }
    }
}