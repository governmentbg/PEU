using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models.Forms;
using EAU.Documents.Domain.Validations;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class NotificationForTemporaryRegistrationPlatesValidator : EAUValidator<NotificationForTemporaryRegistrationPlates>
    {
        public NotificationForTemporaryRegistrationPlatesValidator()
        {
            EAURuleFor(m => m.ElectronicServiceProviderBasicData).EAUInjectValidator(); 
            //EAURuleFor(m => m.ElectronicServiceApplicant).EAUInjectValidator(); 
            //EAURuleFor(m => m.DocumentURI)
            //    .EAUInjectValidator(); //DocumentURIValidator има ClearRules
            EAURuleFor(m => m.DocumentTypeURI)
                .EAUInjectValidator(); //DocumentTypeURIValidator
            EAURuleFor(m => m.DocumentTypeName).RequiredField();
            EAURuleFor(m => m.AISCaseURI).RequiredXmlElement();
            EAURuleFor(m => m.CountOfSetsOfTemporaryPlates).RequiredField();
            EAURuleFor(m => m.CountOfSetsOfTemporaryPlates).LessThanOrEqualToValidation(99).GreaterThanOrEqualToValidation(1);
            EAURuleFor(m => m.CountOfSetsOfTemporaryPlatesText).RequiredField();
            EAURuleFor(m => m.RegistrationNumbersForEachSet).RequiredField();
            EAURuleFor(m => m.AdministrativeBodyName).MinLengthValidatior(1);
        }
    }
}