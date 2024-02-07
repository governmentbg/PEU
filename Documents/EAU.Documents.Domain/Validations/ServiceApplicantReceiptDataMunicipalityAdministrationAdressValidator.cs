using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ServiceApplicantReceiptDataMunicipalityAdministrationAdressValidator : EAUValidator<ServiceApplicantReceiptDataMunicipalityAdministrationAdress>
    {
        public ServiceApplicantReceiptDataMunicipalityAdministrationAdressValidator()
        {
            EAURuleFor(m => m.DistrictName).RequiredFieldFromSection();
            EAURuleFor(m => m.MunicipalityName).RequiredFieldFromSection();
            EAURuleFor(m => m.Mayoralty).RequiredFieldFromSection();
            EAURuleFor(m => m.AreaName).RequiredFieldFromSection().When(m => !string.IsNullOrEmpty(m.MayoraltyCode));
        }
    }
}