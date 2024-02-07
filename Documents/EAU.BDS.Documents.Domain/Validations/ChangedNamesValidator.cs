using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.Documents.Domain.Validations;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class ChangedNamesValidator : EAUValidator<ChangedNames>
    {
        public ChangedNamesValidator()
        {
            EAURuleFor(m => m.Names).EAUInjectValidator();
            EAURuleFor(m => m.NamesLatin).EAUInjectValidator();
        }
    }
}