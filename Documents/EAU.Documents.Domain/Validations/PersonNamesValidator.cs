using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class PersonNamesValidator : EAUValidator<PersonNames>
    {
        public PersonNamesValidator()
        {
            EAURuleFor(m => m.First)
                .RequiredField()
                .RangeLengthValidatior(1, 60)
                .CyrillicNameValidatior();

            EAURuleFor(m => m.Middle)
                .RangeLengthValidatior(1, 60)
                .CyrillicNameValidatior();

            EAURuleFor(m => m.Last)
                .RequiredField()
                .RangeLengthValidatior(1, 60)
                .CyrillicNameValidatior();

            EAURuleFor(m => m.Pseudonim).CyrillicNameValidatior();
        }
    }
}
