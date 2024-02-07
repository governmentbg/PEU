using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.PBZN.Documents.Domain.Models.Forms;
using FluentValidation;
using FluentValidation.Results;

namespace EAU.PBZN.Documents.Domain.Validations
{
    public class ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutValidator : EAUValidator<ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOut>
    {
        public ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutValidator()
        {
            EAURuleFor(m => m.ElectronicAdministrativeServiceHeader).EAUInjectValidator();
            EAURuleFor(m => m.ServiceApplicantReceiptData).EAUInjectValidator();
            EAURuleFor(m => m.IssuingPoliceDepartment).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData).RequiredSection().EAUInjectValidator();
            EAURuleForEach(m => m.Declarations).EAUInjectValidator();
            EAURuleForEach(m => m.AttachedDocuments).EAUInjectValidator();
        }
        public override ValidationResult Validate(ValidationContext<ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOut> context)
        {
            ValidationResult validationRes = base.Validate(context);
            var instance = context.InstanceToValidate;

            if (instance.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant.RecipientGroups != null
                && instance.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant.RecipientGroups[0].Recipients.Count > 0
                )
            {
                if (instance.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant.RecipientGroups[0].Recipients[0].Item != null
                    && instance.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant.RecipientGroups[0].Recipients[0].Item is EntityBasicData)
                {
                    if (instance.ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData.EntityManagementAddress == null)
                    {
                        AddValidationFailure(validationRes, "DOC_PBZN_ApplicationForIssuingDocumentsOfIncurredAndOtherAccidentsAndProvideInformationOnTheActivityCarriedOutData_entityManagementAddress_E");
                    }
                }
            }

            return validationRes;
        }

    }

}
