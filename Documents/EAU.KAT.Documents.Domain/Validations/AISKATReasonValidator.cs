using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;
using EAU.KAT.Documents.Domain.Models;

namespace EAU.KAT.Documents.Domain.Validations
{
    public class AISKATReasonValidator : EAUValidator<AISKATReason>
    {
        public AISKATReasonValidator()
        {
            EAURuleFor(m => m.Code).RequiredFieldFromSection();
            EAURuleFor(m => m.Name).RequiredFieldFromSection();
        }
    }
}
