using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.PBZN.Documents.Domain.Models;
using FluentValidation;
using FluentValidation.Results;

namespace EAU.PBZN.Documents.Domain.Validations
{
    public class ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataValidator : EAUValidator<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData>
    {
        public ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataValidator()
        {
            EAURuleFor(m => m.EntityManagementAddress)
                .RequiredField()
                .EAUInjectValidator();
            EAURuleFor(m => m.CorrespondingAddress)
                .RequiredField()
                .EAUInjectValidator();
            EAURuleFor(m => m.DeclaredScopeOfCertification).RequiredField();
            EAURuleFor(m => m.AvailableCertifiedPersonnel).RequiredField();
            EAURuleForEach(m => m.AvailableCertifiedPersonnel).EAUInjectValidator();
        }
        //public override ValidationResult Validate(ValidationContext<ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityData> context)
        //{
        //    ValidationResult validationRes = base.Validate(context);
        //    var instance = context.InstanceToValidate;

        //    if (instance.AvailableCertifiedPersonnel == null || instance.AvailableCertifiedPersonnel.Count < 2)
        //    {
        //        AddValidationFailure(validationRes, "DOC_PBZN_AvailableCertifiedPersonnel_Min2_E");
        //    }

        //    return validationRes;
        //}
    }
}
