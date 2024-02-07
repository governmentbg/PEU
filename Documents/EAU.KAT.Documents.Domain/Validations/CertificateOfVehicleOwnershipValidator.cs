using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class CertificateOfVehicleOwnershipValidator : EAUValidator<CertificateOfVehicleOwnership>
    {
        public CertificateOfVehicleOwnershipValidator()
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
            EAURuleFor(m => m.CertificateNumber).MinLengthValidatior(1);
            EAURuleFor(m => m.IssuingPoliceDepartment).RequiredFieldFromSection().EAUInjectValidator();
            EAURuleFor(m => m.CertificateOfVehicleOwnershipHeader).MinLengthValidatior(1);
            EAURuleFor(m => m.PermanentAddress).EAUInjectValidator();
            EAURuleFor(m => m.IssuingPoliceDepartment)
                .RequiredFieldFromSection()
                .EAUInjectValidator();
            //EAURuleFor(m => m.VehicleData).EAUInjectValidator(); валидатора е празен
            EAURuleFor(m => m.VehicleOwnerInformationCollection).EAUInjectValidator();
            EAURuleFor(m => m.AdministrativeBodyName).MinLengthValidatior(1);
        }
    }
}