using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KOS.Documents.Domain.Models;

namespace EAU.KOS.Documents.Domain.Validations
{
    public class TechnicalSpecificationsOfWeaponsValidator : EAUValidator<TechnicalSpecificationOfWeapon>
    {
        public TechnicalSpecificationsOfWeaponsValidator()
        {
            EAURuleFor(m => m.Make).RequiredField();
            EAURuleFor(m => m.Caliber).RequiredField();
            EAURuleFor(m => m.WeaponNumber).RequiredField();
            EAURuleFor(m => m.WeaponNumber).MatchesValidatior(@"^[^@?]+$").WithEAUErrorCode(ErrorMessagesConstants.NotAllowedQuestionmarksAndAT);
        }
    }
}