using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;
using FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class ElectronicServiceRecipientValidator : EAUValidator<ElectronicServiceRecipient>
    {
        public ElectronicServiceRecipientValidator()
        {
            EAURuleFor(m => m.ItemPersonBasicData).EAUInjectValidator("All")
                .When(m => m.ItemName == RecipientChoiceType.Person);

            EAURuleFor(m => m.ItemForeignCitizenBasicData).EAUInjectValidator()
                .When(m => m.ItemName == RecipientChoiceType.ForeignPerson);

            EAURuleFor(m => m.ItemEntityBasicData).EAUInjectValidator()
                .When(m => m.ItemName == RecipientChoiceType.Entity);

            EAURuleFor(m => m.ItemForeignEntityBasicData).EAUInjectValidator()
                .When(m => m.ItemName == RecipientChoiceType.ForeignEntity);
        }
    }
}
