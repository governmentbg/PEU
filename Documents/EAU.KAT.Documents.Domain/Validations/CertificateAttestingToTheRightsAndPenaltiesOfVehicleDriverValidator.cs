using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models.Forms;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverValidator : EAUValidator<CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriver>
    {
        public CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverValidator()
        {
            EAURuleFor(m => m.ElectronicServiceProviderBasicData)
               .EAUInjectValidator(); //ElectronicServiceProviderBasicDataValidator
            //EAURuleFor(m => m.ElectronicServiceApplicant)
            //    .EAUInjectValidator(); //ElectronicServiceApplicantValidator
            EAURuleFor(m => m.DocumentURI)
                .EAUInjectValidator(); //DocumentURIValidator
            EAURuleFor(m => m.DocumentTypeURI)
                .EAUInjectValidator(); //DocumentTypeURIValidator
            EAURuleFor(m => m.DocumentTypeName).RequiredField();
            EAURuleFor(m => m.AISCaseURI).RequiredXmlElement();
            //EAURuleFor(m => m.CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverHeader).RequiredField();
            EAURuleFor(m => m.CertificateAttestingToTheRightsAndPenaltiesOfVehicleDriverHeader).MinLengthValidatior(1);
            EAURuleFor(m => m.CertificateData).MinLengthValidatior(1);
            EAURuleFor(m => m.CertificateData).RequiredField();
            //EAURuleFor(m => m.CertificateData1).MinLengthValidatior(1);
            EAURuleFor(m => m.CertificateNumber).MinLengthValidatior(1);
            EAURuleFor(m => m.IssuingPoliceDepartment).EAUInjectValidator();
            EAURuleFor(m => m.AdministrativeBodyName).MinLengthValidatior(1);
            EAURuleFor(m => m.ReportDate).RequiredField();
        }
    }
}