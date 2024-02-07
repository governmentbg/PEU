using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;
using System.Linq;

namespace EAU.COD.Documents.Domain.Validations
{
    public class TerritorialScopeOfServicesValidator : EAUValidator<TerritorialScopeOfServices>
    {
        public TerritorialScopeOfServicesValidator()
        {
            EAURuleFor(m => m.ScopeOfCertification).RequiredField();
            EAURuleForEach(m => m.Districts).EAUInjectValidator().When(m => m.ScopeOfCertification == ScopeOfCertification.SelectedDistricts);
            EAURuleFor(m => m.Districts).Must((d, v) =>
            {
                var duplicateItems = v.Where(x => !string.IsNullOrEmpty(x.DistrictGRAOCode))
                .GroupBy(x => x.DistrictGRAOCode)
                .Where(x => x.Count() > 1).Select(x => x.Key);

                if (duplicateItems != null && duplicateItems.Any())
                    return false;
                else
                    return true;
            }).WithEAUErrorCode(ErrorMessagesConstants.DuplicateElementsInCollection)
            .When(m => m.ScopeOfCertification == ScopeOfCertification.SelectedDistricts && m.Districts != null && m.Districts.Count > 1);

            EAURuleFor(m => m.Districts).RequiredField().When(m => m.ScopeOfCertification == ScopeOfCertification.SelectedDistricts);
        }
    }
}