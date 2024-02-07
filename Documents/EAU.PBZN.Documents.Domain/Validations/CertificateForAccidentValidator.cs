using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.PBZN.Documents.Domain.Models.Forms;
using System;

namespace EAU.PBZN.Documents.Domain.Validations
{
    public class CertificateForAccidentValidator : EAUValidator<CertificateForAccident>
    {
        public CertificateForAccidentValidator()
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
            EAURuleFor(m => m.CertificateForAccidentData).RequiredField();
            EAURuleFor(m => m.CertificateForAccidentData).MinLengthValidatior(1);
            EAURuleFor(m => m.CertificateForAccidentHeader).RequiredField();
            EAURuleFor(m => m.IssuingPoliceDepartment).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.DocumentMustServeTo).RequiredField().EAUInjectValidator();
            EAURuleFor(m => m.AdministrativeBodyName).RequiredField();

            //TODO: LessThanValidation не приема nullable date
            //EAURuleFor(m => m.AccidentRegistrationDate).RequiredField().LessThanValidation(DateTime.Now);
        }
    }
}
