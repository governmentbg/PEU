using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ServiceApplicantReceiptDataUnitInAdministrationValidator : EAUValidator<ServiceApplicantReceiptDataUnitInAdministration>
    {
        public ServiceApplicantReceiptDataUnitInAdministrationValidator()
        {
            EAURuleFor(m => m.EntityBasicData).EAUInjectValidator(); //EntityBasicDataValidator
            EAURuleFor(m => m.AdministrativeDepartmentCode).RequiredField(); 
        }
    }
}
