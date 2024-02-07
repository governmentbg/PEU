using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ForeignCitizenNamesValidator : EAUValidator<ForeignCitizenNames>
    {
        public ForeignCitizenNamesValidator()
        {
            //Cyrillic Names
            EAURuleFor(m => m.FirstCyrillic)
                .CyrillicNameValidatior()
                .RequiredAtLeastOneOfTwoField(m => m.FirstLatin);
            EAURuleFor(m => m.LastCyrillic).CyrillicNameValidatior();
            EAURuleFor(m => m.OtherCyrillic)
                .CyrillicNameValidatior()
                .RequiredAtLeastOneOfTwoField(m => m.LastCyrillic).When(m => !string.IsNullOrEmpty(m.FirstCyrillic));
            EAURuleFor(m => m.PseudonimCyrillic).CyrillicNameValidatior();

            //Latin names
            EAURuleFor(m => m.FirstLatin).LatinNameValidatior();
            EAURuleFor(m => m.LastLatin).LatinNameValidatior();
            EAURuleFor(m => m.OtherLatin)
                .LatinNameValidatior()
                .RequiredAtLeastOneOfTwoField(m => m.LastLatin).When(m => !string.IsNullOrEmpty(m.FirstLatin));
            EAURuleFor(m => m.PseudonimLatin).LatinNameValidatior();
        }
    }
}
