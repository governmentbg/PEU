using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class VehicleOwnerAddressValidator : EAUValidator<VehicleOwnerAddress>
    {
        public VehicleOwnerAddressValidator()
        {
            EAURuleFor(m => m.DistrictCode)
                .LengthValidatior(2)
                .MatchesValidatior("\\d{2}", ErrorMessagesConstants.OnlyDigitsAllowed);
            EAURuleFor(m => m.DistrictName).RangeLengthValidatior(1, 25);
            EAURuleFor(m => m.MunicipalityCode)
               .LengthValidatior(2)
               .MatchesValidatior("\\d{2}", ErrorMessagesConstants.OnlyDigitsAllowed);
            EAURuleFor(m => m.MunicipalityName).RangeLengthValidatior(1, 25);
            EAURuleFor(m => m.ResidenceCode)
               .LengthValidatior(5)
               .MatchesValidatior("\\d{5}", ErrorMessagesConstants.OnlyDigitsAllowed);
            EAURuleFor(m => m.ResidenceName).RangeLengthValidatior(1, 25);
            EAURuleFor(m => m.AddressSupplement).MinLengthValidatior(1);
        }
    }
}