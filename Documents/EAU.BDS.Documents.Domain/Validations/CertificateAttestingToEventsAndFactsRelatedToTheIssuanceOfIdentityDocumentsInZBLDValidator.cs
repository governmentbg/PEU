using EAU.BDS.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDValidator : EAUValidator<CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLD>
    {
        public CertificateAttestingToEventsAndFactsRelatedToTheIssuanceOfIdentityDocumentsInZBLDValidator()
        {
            EAURuleFor(m => m.ElectronicServiceProviderBasicData)
               .RequiredField()
               .EAUInjectValidator(); //ElectronicServiceProviderBasicDataValidator
            //EAURuleFor(m => m.ElectronicServiceApplicant)
            //    .RequiredField()
            //    .EAUInjectValidator(); //ElectronicServiceApplicantValidator
            EAURuleFor(m => m.DocumentURI)
                .EAUInjectValidator(); //DocumentURIValidator
            //EAURuleFor(m => m.DocumentTypeURI)
            //    .EAUInjectValidator(); //DocumentTypeURIValidator има this.ClearRules(m => m.DocumentURI);
            EAURuleFor(m => m.DocumentTypeName).RequiredField();
            EAURuleFor(m => m.AISCaseURI).RequiredXmlElement();
            EAURuleFor(m => m.IssuingPoliceDepartment)
                .RequiredField()
                .EAUInjectValidator();

            When(m => m.DocumentMustServeTo == null, () =>
            {
                EAURuleFor(m => m.DocumentMustServeTo).RequiredSection();
            })
            .Otherwise(() =>
            {
                EAURuleFor(m => m.DocumentMustServeTo).EAUInjectValidator();
            });

            EAURuleFor(m => m.ReportDate).RequiredField();
        }
    }
}