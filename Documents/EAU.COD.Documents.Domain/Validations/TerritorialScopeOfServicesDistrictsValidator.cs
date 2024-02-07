using EAU.COD.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.COD.Documents.Domain.Validations
{
    public class TerritorialScopeOfServicesDistrictsValidator : EAUValidator<TerritorialScopeOfServicesDistricts>
    {
        public TerritorialScopeOfServicesDistrictsValidator()
        {
            EAURuleFor(m => m.DistrictGRAOCode).RequiredField().MatchesValidatior("\\d{2}", ErrorMessagesConstants.OnlyDigitsAllowed);
            EAURuleFor(m => m.DistrictGRAOName).RequiredField().RangeLengthValidatior(1, 25);
        }
    }
}
