using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class PersonalInformationValidator : EAUValidator<PersonalInformation>
    {
        public PersonalInformationValidator()
        {
            EAURuleFor(m => m.MobilePhone)
                .MinLengthValidatior(1)
                .PhoneValidation();
            EAURuleFor(m => m.WorkPhone)
                .MinLengthValidatior(1)
                .PhoneValidation();
            EAURuleFor(m => m.PersonAddress)
                .RequiredField()
                .EAUInjectValidator(); //PersonAddressValidator
        }
    }
}
