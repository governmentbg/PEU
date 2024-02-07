using EAU.Documents.Domain.Models;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.Documents.Domain.Validations
{
    public class RemovingIrregularitiesInstructionsIrregularitiesValidator : EAUValidator<RemovingIrregularitiesInstructionsIrregularities>
    {
        public RemovingIrregularitiesInstructionsIrregularitiesValidator()
        {
            EAURuleFor(m => m.IrregularityType).RequiredField();
            EAURuleFor(m => m.AdditionalInformationSpecifyingIrregularity).MinLengthValidatior(1);
        }
    }
}