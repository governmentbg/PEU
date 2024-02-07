using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class PersonDataValidator : EAUValidator<EAU.Documents.Domain.Models.PersonData>
    {
        public PersonDataValidator()
        {
            EAURuleFor(m => m.PersonIdentification).RequiredFieldFromSection().EAUInjectValidator();

            EAURuleFor(m => m.Height)
                   .ExclusiveBetween(30, 300).WithEAUErrorCode(ErrorMessagesConstants.InsertValidHeight);

            EAURuleFor(m => m.Item).RequiredFieldFromSection().SetInheritanceValidator(v =>
            {
                v.EAUAdd(new PlaceOfBirthValidator());
                v.EAUAdd(new PlaceOfBirthAbroadValidator());
            });
        }
    }
}