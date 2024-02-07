using EAU.BDS.Documents.Domain.Models;
using EAU.Documents.Domain.Validations;
using EAU.Documents.Domain.Validations.FluentValidation;

namespace EAU.BDS.Documents.Domain.Validations
{
    public class IssuingBgPersonalDocumentReasonDataValidator : EAUValidator<IssuingBgPersonalDocumentReasonData>
    {
        public IssuingBgPersonalDocumentReasonDataValidator()
        {
            EAURuleFor(m => m.Reason).RequiredField();
            EAURuleFor(m => m.FactsAndCircumstances).RequiredField();
        }
    }
}
