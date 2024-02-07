using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.PBZN.Documents.Domain.Models.Forms;
using FluentValidation;

namespace EAU.PBZN.Documents.Domain.Validations
{
    public class CertificateToWorkWithFluorinatedGreenhouseGassesValidator : EAUValidator<CertificateToWorkWithFluorinatedGreenhouseGasses>
    {
        public CertificateToWorkWithFluorinatedGreenhouseGassesValidator()
        {
            EAURuleFor(m => m.ElectronicServiceProviderBasicData)
               .EAUInjectValidator(); //ElectronicServiceProviderBasicDataValidator
            //EAURuleFor(m => m.ElectronicServiceApplicant)
            //    .EAUInjectValidator(); //ElectronicServiceApplicantValidator
            //EAURuleFor(m => m.DocumentURI)
            //    .EAUInjectValidator(); //DocumentURIValidator има ClearRules
            EAURuleFor(m => m.DocumentTypeURI)
                .EAUInjectValidator(); //DocumentTypeURIValidator
            EAURuleFor(m => m.DocumentTypeName).RequiredField();
            EAURuleFor(m => m.AISCaseURI).RequiredXmlElement();
            EAURuleFor(m => m.CertificateToWorkWithFluorinatedGreenhouseGassesHeader).RequiredField();
            EAURuleFor(m => m.CertificateValidity).RequiredField();
            EAURuleFor(m => m.CertificateToWorkWithFluorinatedGreenhouseGassesGround).RequiredField();
            EAURuleFor(m => m.CertificateToWorkWithFluorinatedGreenhouseGassesActivities).RequiredField();
            //EAURuleFor(m => m.DocumentReceiptOrSigningDate).RequiredField();
            EAURuleFor(m => m.AdministrativeBodyName).RequiredField();

            EAURuleFor(m => m.Item).SetInheritanceValidator(v =>
            {
                v.EAUAdd(new ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesEntityDataValidator());
                v.EAUAdd(new ApplicationForIssuingOrRenewingCertificateToWorkWithFluorinatedGreenhouseGassesPersonDataValidator());
            });

            ////TODO: Да се провери.
            //EAURuleFor(m => m.IdentificationPhoto).RequiredField()
            //    .When(m => m.ElectronicServiceApplicant.RecipientGroups[0].Recipients[0].ItemName == EAU.Documents.Domain.Models.RecipientChoiceType.Person);
        }
    }
}
